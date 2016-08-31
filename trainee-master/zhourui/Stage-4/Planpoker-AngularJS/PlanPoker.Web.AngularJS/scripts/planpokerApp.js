var app = angular.module("PlanPokerApp", ["ngRoute", "ngCookies"]);

var apiUrl = "http://localhost:8888";

app.config(function($routeProvider) {
    $routeProvider
        .when("/register", {
            templateUrl: "partials/register/register_template.html",
            controller: "registerController"
        })
        .when("/login", {
            templateUrl: "partials/login/login_template.html",
            controller: "loginController"
        })
        .when("/profile", {
            templateUrl: "partials/profile/profile_template.html",
            controller: "profileController"
        })
        .when("/project", {
            templateUrl: "partials/project/project_template.html",
            controller: "projectController"
        })
        .when("/dashboard", {
            templateUrl: "partials/dashboard/dashboard_template.html",
            controller: "dashboardController"
        })
        .when("/estimate/:presentation/", {
            templateUrl: "partials/estimate/estimate_template.html",
            controller: "estimateController"
        })
        .when("/estimate/:presentation/:projectId/", {
            templateUrl: "partials/estimate/estimate_template.html",
            controller: "estimateController"
        })
        .otherwise({
            redirectTo: "/dashboard"
        });
    //$locationProvider.html5Mode(true);
});

function checkCookie($cookieStore, $location) {
    if (!$cookieStore.get("Cookie_User_Id")) {
        $location.path("/login");
    }
};

function logout($location, $cookieStore, timerService, estimateService) {
    estimateService.deleteEstimate($cookieStore.get("Cookie_Project_Id"));
    $cookieStore.remove("Cookie_User_Id");
    $cookieStore.remove("Cookie_User_Name");
    $cookieStore.remove("Cookie_Project_Id");
    $cookieStore.remove("Cookie_Poker_Id");

    timerService.cancelTimer();

    $location.path("/login");
}

function createEstimate($scope, $rootScope, $cookieStore, $location, estimateService, pokerId) {
    $rootScope.isTimerStopped = true;
    $cookieStore.put("Cookie_Poker_Id", pokerId);
    var estimate = {
        ProjectId: $cookieStore.get("Cookie_Project_Id"),
        UserId: $cookieStore.get("Cookie_User_Id"),
        UserName: $cookieStore.get("Cookie_User_Name"),
        SelectedPoker: $cookieStore.get("Cookie_Poker_Id")
    };
    estimateService.createEstimate(estimate)
        .success(function () { $location.path("/estimate/false/" + $scope.selectedProjectId + "/"); })
        .error(function (errorMessage) { console.log(errorMessage); });
}

var estimatesIsShow = false;
function loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) {
    if (!$rootScope.isTimerStopped) {
        estimateService.getEstimates($routeParams.projectId)
            .success(function (returnObject) {
                if (returnObject) {
                    $scope.estimates = returnObject.Estimates;
                    estimatesIsShow = returnObject.IsShow;

                    if (!estimatesIsShow) {
                        angular.forEach($scope.estimates, function (estimate) { estimate.pokerImage = "/images/planpoker.jpg"; });
                    } else {
                        angular.forEach($scope.estimates, function (estimate) { estimate.pokerImage = "/images/" + estimate.SelectedPoker + ".jpg"; });

                        var pokerIds = [];
                        angular.forEach($scope.estimates, function (estimate) { pokerIds.push(estimate.SelectedPoker); });
                        $scope.averageTime = calculateAverageHour(pokerIds);
                        $scope.presentationResult = true;
                    }

                    $scope.getUserImage = function (estimate) {
                        if (estimate) {
                            return estimate[0].UserImage;
                        } else {
                            return "/images/user.png";
                        }
                    };

                    $scope.getUserName = function (estimate) {
                        if (estimate) {
                            return estimate[0].UserName;
                        } else {
                            return "anonymous";
                        }
                    };
                } else {
                    if ($routeParams.presentation === "false") {
                        $location.path("/dashboard");
                    }
                }
            })
            .error(function (errorMessage) { console.log(errorMessage); });
    } else {
        $interval.cancel($rootScope.timer1);
        $interval.cancel($rootScope.timer2);
    }
};

function loopEstimateResult($rootScope, $scope, $routeParams, $location, $interval, estimateService) {
    $scope.presentationShow = false;
    $scope.presentationNext = true;

    estimateService.showEstimatesResult($routeParams.projectId);

    $rootScope.timer2 = $interval(function () { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) }, 1000, 5);
    $rootScope.timer2.then(function () { console.log("timer2 done!"); });

    var pokerIds = [];
    angular.forEach($scope.estimates, function (estimate) { pokerIds.push(estimate.SelectedPoker); });

    $scope.averageTime = calculateAverageHour(pokerIds);
    $scope.presentationResult = true;
}

function calculateAverageHour(selectPokerCardsPoints) {
    var length = selectPokerCardsPoints.length;
    var sum = 0;
    var nanNum = 0;

    for (var i = 0; i < length; i++) {
        if (isNaN(selectPokerCardsPoints[i])) {
            nanNum--;
            continue;
        }
        sum += Number(selectPokerCardsPoints[i]);
    }

    var average = sum / (length + nanNum);
    return isNaN(Math.round(average)) ? 0 : Math.round(average);
}