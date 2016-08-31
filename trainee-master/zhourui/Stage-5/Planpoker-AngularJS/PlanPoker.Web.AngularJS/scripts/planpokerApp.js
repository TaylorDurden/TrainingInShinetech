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

app.service("timerService", function($rootScope, $interval) {
    $rootScope.stopTimer = false;
    this.cancelTimer = function() {
        $rootScope.stopTimer = true;
        $interval.cancel($rootScope.timer1);
        $interval.cancel($rootScope.timer2);
    };
});

app.controller("indexController", function($scope, $location, $route, $cookieStore, timerService, projectService, estimateService) {
    $scope.$on("$routeChangeSuccess", function() {
        if ($location.path().indexOf("login") > 0 || $location.path().indexOf("register") > 0) {
            $scope.menuNewProject = false;
            $scope.menuDropDown = false;
        }
        if ($location.path().indexOf("dashboard") > 0 || $location.path().indexOf("profile") > 0 || $location.path().indexOf("project") > 0) {
            $scope.menuNewProject = true;
            $scope.menuDropDown = true;
        }
        if ($location.path().indexOf("estimate") > 0) {
            if ($location.path().indexOf("true") > 0) {
                $scope.menuNewProject = false;
                $scope.menuDropDown = false;
            } else {
                $scope.menuNewProject = true;
                $scope.menuDropDown = true;
            }
        }
    });

    $scope.project = {};
    $scope.createProject = function() {
        projectService.createProject($scope.project)
            .success(function(returnObject) {
                if (returnObject.Message) {
                    $scope.createProjectErrorTips = "project name already exists!";
                    return;
                }

                angular.element("#modalCreateProject").modal("hide");
                $location.path("/dashboard");
                console.log("Create project success!");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };
    $scope.changeProjectName = function() { $scope.project.Name = $scope.projectName; };

    $scope.toLogin = function() {
        estimateService.deleteEstimate($cookieStore.get("Cookie_Project_Id"));
        $cookieStore.remove("Cookie_User_Id");
        $cookieStore.remove("Cookie_User_Name");
        $cookieStore.remove("Cookie_Project_Id");
        $cookieStore.remove("Cookie_Poker_Id");

        timerService.cancelTimer();

        $location.path("/login");
    };
});

app.directive("passwordCheck", [
    function() {
        return {
            require: "ngModel",
            link: function(scope, elem, attrs, ctrl) {
                var firstPassword = "#" + attrs.passwordCheck;
                elem.add(firstPassword).on("keyup", function() {
                    scope.$apply(function() {
                        var v = elem.val() === $(firstPassword).val();
                        ctrl.$setValidity("passwordmatch", v);
                    });
                });
            }
        };
    }
]);

app.directive("fileReader", function($q) {
    var slice = Array.prototype.slice;
    return {
        restrict: "A",
        require: "ngModel",
        link: function(scope, element, attrs, ngModel) {
            if (!ngModel) return;

            ngModel.$render = function() {};

            element.bind("change", function(e) {
                var element = e.target;
                $q.all(slice.call(element.files, 0).map(readFile))
                    .then(function(values) {
                        if (element.multiple) ngModel.$setViewValue(values);
                        else ngModel.$setViewValue(values.length ? values[0] : null);
                    });

                function readFile(file) {
                    var deferred = $q.defer();
                    var reader = new FileReader();
                    reader.onload = function(e) {
                        deferred.resolve(e.target.result);
                    };
                    reader.onerror = function(e) {
                        deferred.reject(e);
                    };
                    reader.readAsDataURL(file);

                    return deferred.promise;
                }
            });
        }
    };
});

function checkCookie($cookieStore, $location) {
    if (!$cookieStore.get("Cookie_User_Id")) {
        $location.path("/login");
    }
};