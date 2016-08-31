function apiPath() {
    var path = "http://localhost:42356";
    return path;
};

var app = angular.module("BugManagementApp", ["ngRoute", "ngCookies"]);
//app = angular.module("BugManagementApp", ['ngCookies', 'ngResource','ngSanitize','ngRoute','angularFileUpload']);
    
app.config(function ($routeProvider) {
    $routeProvider
        .when('/register', {
            templateUrl: 'Views/register.html',
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
            templateUrl: 'Views/login.html',
            controller: 'loginController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/dashboard', {
            templateUrl: 'Views/dashboard.html',
            controller: 'dashboardController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/bug', {
            templateUrl: 'Views/bug.html',
            controller: 'bugController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/project', {
            templateUrl: 'Views/project.html',
            controller: 'projectController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/developer', {
            templateUrl: 'Views/developer.html',
            controller: 'developerController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/user', {
            templateUrl: 'Views/user.html',
            controller: 'userController',
            resolve: {
                delay: function ($q, $timeout) {
                    var delay = $q.defer();
                    $timeout(delay.resolve, 500);
                    return delay.promise;
                }
            }
        })
        .when('/test', {
            templateUrl: 'Views/testUpoad.html',
            controller: 'UploadCtrl'
        })

        .otherwise({
            redirectTo: '/login'
        });
});

function checkCookie($cookieStore, $location) {
    if (!$cookieStore.get('Cookie_UserId')) {
        $location.path("/login");
    }
};