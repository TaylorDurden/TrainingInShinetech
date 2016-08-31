function apiPath() {
    var path = "http://localhost:8888";
    return path;
}

var appModule = angular.module('plan-poker-app', ['ngRoute']);

angular.module('plan-poker-app').run(["$rootScope", "$location","$routeParams", function ($rootScope, $location, $routeParams) {
    var routeChangeSuccessOff = $rootScope.$on('$routeChangeSuccess', routeChangeSuccess);
    function routeChangeSuccess(event) {
        if ($location.path() == '/login' || $location.path() == '/register') {
            $rootScope.isProjectShow = false;
            $rootScope.isSettingsShow = false;
        }
        else if ($location.path() == '/dashboard') {
            $rootScope.isProjectShow = true;
            $rootScope.isSettingsShow = true;
        }
        else if ($location.path() == '/project' || $location.path() == '/profile') {
            $rootScope.isProjectShow = false;
            $rootScope.isSettingsShow = true; 
        }
        else if ($routeParams.presenter == undefined) {
            $rootScope.isProjectShow = false;
            $rootScope.isSettingsShow = true;
        }
        else if ($routeParams.presenter == 'presenter') {
            $rootScope.isProjectShow = false;
            $rootScope.isSettingsShow = false;
        }
    }
}]);
