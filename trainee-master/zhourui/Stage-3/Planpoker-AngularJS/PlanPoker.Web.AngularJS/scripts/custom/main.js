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