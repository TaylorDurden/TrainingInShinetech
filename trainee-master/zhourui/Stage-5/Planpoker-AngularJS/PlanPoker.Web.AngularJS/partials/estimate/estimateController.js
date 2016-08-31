app.controller("estimateController", function($scope, $rootScope, $location, $route, $routeParams, $cookies, $cookieStore, projectService, estimateService, $q, $interval) {
    if ($routeParams.presentation === undefined || $routeParams.presentation === "false") {
        checkCookie($cookieStore, $location);
    };

    $scope.projects = [];
    projectService.getAllProjects()
        .success(function(returnObject) {
            $scope.projects = returnObject;
            console.log("Get all projects names success!");
        })
        .error(function(errorMessage) { console.log(errorMessage); });

    if ($routeParams.projectId !== undefined) {
        $scope.selectedProjectId = $routeParams.projectId;
    }

    $scope.changeProject = function() {
        projectService.getProjectById($scope.selectedProjectId)
            .success(function(returnObject) {
                $location.path("/estimate/true/" + returnObject.Id + "/");
                console.log("Change project success!");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };

    $scope.presentation = $routeParams.presentation !== undefined && $routeParams.presentation === "true";

    $rootScope.stopTimer = false;
    if ($routeParams.projectId !== undefined) {
        $scope.estimates = "";
        $rootScope.timer1 = $interval(function() { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) }, 1000);
        $rootScope.timer1.then(function() { console.log("timer2 done!"); });

        $scope.showEstimatesResult = function () {
            $scope.presentation = false;
            $scope.presentationNext = true;

            estimateService.showEstimatesResult($routeParams.projectId);

            $rootScope.timer2 = $interval(function() { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) }, 1000, 5);
            $rootScope.timer2.then(function() { console.log("timer2 done!"); });

            var pokerIds = [];
            angular.forEach($scope.estimates, function(estimate) { pokerIds.push(estimate.SelectedPoker); });

            $scope.averageTime = calculateAverageHour(pokerIds);
            $scope.presentationResult = true;
        };

        $scope.nextProject = function() {
            $scope.presentation = true;
            $scope.presentationNext = false;

            $rootScope.stopTimer = false;
            estimateService.deleteEstimate($routeParams.projectId);
            $location.path("/estimate/true/");
        };
    }
});

var estimatesIsShow = false;
function loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) {
    if (!$rootScope.stopTimer) {
        estimateService.getEstimates($routeParams.projectId)
            .success(function(returnObject) {
                if (returnObject) {
                    $scope.estimates = returnObject.Estimates;
                    estimatesIsShow = returnObject.IsShow;

                    if (!estimatesIsShow) {
                        angular.forEach($scope.estimates, function(estimate) { estimate.pokerImage = "/images/planpoker.jpg"; });
                    } else {
                        angular.forEach($scope.estimates, function(estimate) { estimate.pokerImage = "/images/" + estimate.SelectedPoker + ".jpg"; });

                        var pokerIds = [];
                        angular.forEach($scope.estimates, function(estimate) { pokerIds.push(estimate.SelectedPoker); });
                        $scope.averageTime = calculateAverageHour(pokerIds);
                        $scope.presentationResult = true;
                    }

                    $scope.getUserImage = function(estimate) {
                        if (estimate) {
                            return estimate[0].UserImage;
                        } else {
                            return "/images/user.png";
                        }
                    };

                    $scope.getUserName = function(estimate) {
                        if (estimate) {
                            return estimate[0].UserName;
                        } else {
                            return "anonymous";
                        }
                    };
                } else {
                    if (!$scope.presentation) {
                        $location.path("/dashboard");
                    }
                }
                console.log("Get Poker ViewModel Success!");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    } else {
        $interval.cancel($rootScope.timer1);
        $interval.cancel($rootScope.timer2);
    }
};

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