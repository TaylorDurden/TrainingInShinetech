function apiPath() {
    var path = "http://localhost:8888";
    return path;
};

var app = angular.module("PlanPokerApp", ["ngRoute", "ngCookies"]);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/register', {
            templateUrl: 'partials/register.html',
            controller: 'registerController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/login', {
            templateUrl: 'partials/login.html',
            controller: 'loginController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/profile', {
            templateUrl: 'partials/profile.html',
            controller: 'profileController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/project', {
            templateUrl: 'partials/project.html',
            controller: 'projectController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/dashboard', {
            templateUrl: 'partials/dashboard.html',
            controller: 'dashboardController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/estimate/:presentation/', {
            templateUrl: 'partials/estimate.html',
            controller: 'estimateController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/estimate/:presentation/:projectId/', {
            templateUrl: 'partials/estimate.html',
            controller: 'estimateController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .otherwise({
            redirectTo: '/dashboard'
        });
    //$locationProvider.html5Mode(true);
});

app.service("globalService", function ($rootScope, $interval) {
    $rootScope.timerStop = false;
    this.timerCancel = function () {
        $rootScope.timerStop = true;
        $interval.cancel($rootScope.timer1);
        $interval.cancel($rootScope.timer2);
    };
});

app.controller("indexController", function ($scope, $location, $route, $cookies, $cookieStore, globalService, projectService, estimateService) {
    $scope.$on("$routeChangeSuccess", function () {
        if ($location.path().indexOf("login") > 0 || $location.path().indexOf("register") > 0) {
            $scope.newProjectMenu = false;
            $scope.dropdownMenu = false;
        }
        if ($location.path().indexOf("dashboard") > 0 || $location.path().indexOf("profile") > 0 || $location.path().indexOf("project") > 0) {
            $scope.newProjectMenu = true;
            $scope.dropdownMenu = true;
        }
        if ($location.path().indexOf("estimate") > 0) {
            if ($location.path().indexOf("true") > 0) {
                $scope.newProjectMenu = false;
                $scope.dropdownMenu = false;
            } else {
                $scope.newProjectMenu = true;
                $scope.dropdownMenu = true;
            }
        }
    });

    $scope.project = { Id: 0, Name: "", ProjectGuid: "" };
    $scope.createProject = function () {
        projectService.createProject($scope.project).then(function () {
            console.log("Create project success!");
            angular.element("#modalCreateProject").modal("hide");
            $location.path('/dashboard');
        }, function () {
            $scope.createProjectErrorTips = "project name already exists!";
        });
    }
    $scope.changeProjectName = function () {
        $scope.project.Name = $scope.projectName;
    };

    $scope.toLogin = function () {
        estimateService.deleteEstimate($cookieStore.get("Cookie_Project_Id"));
        $cookieStore.remove("Cookie_User_Id");
        $cookieStore.remove("Cookie_User_Name");
        $cookieStore.remove("Cookie_Project_Id");
        $cookieStore.remove("Cookie_Poker_Id");

        globalService.timerCancel();

        $location.path("/login");
    };
});

app.directive('passwordCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('passwordmatch', v);
                });
            });
        }
    }
}]);

app.directive('fileReader', function ($q) {
    var slice = Array.prototype.slice;
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;

            ngModel.$render = function () { };

            element.bind('change', function (e) {
                var element = e.target;

                $q.all(slice.call(element.files, 0).map(readFile))
                    .then(function (values) {
                        if (element.multiple) ngModel.$setViewValue(values);
                        else ngModel.$setViewValue(values.length ? values[0] : null);
                    });

                function readFile(file) {
                    var deferred = $q.defer();

                    var reader = new FileReader();
                    reader.onload = function (e) {
                        deferred.resolve(e.target.result);
                    };
                    reader.onerror = function (e) {
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
    if (!$cookieStore.get('Cookie_User_Id')) {
        $location.path("/login");
    }
};