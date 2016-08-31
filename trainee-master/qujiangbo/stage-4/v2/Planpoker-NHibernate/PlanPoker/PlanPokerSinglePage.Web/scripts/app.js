var apiPath = "http://localhost:8888";
var appModule = angular.module("plan-poker-app", ["ngRoute", "ngCookies"]);

angular.module("plan-poker-app").run(["$rootScope", "$location","$routeParams", function ($rootScope, $location, $routeParams) {
    $rootScope.$on("$routeChangeSuccess", routeChangeSuccess);
    function routeChangeSuccess() {
        if ($location.path() === "/dashboard" || $location.path() === "/profile") {
            $rootScope.isProjectShow = false;
            $rootScope.isSettingsShow = true;
        }
        else if ($location.path() === "/project") {
            $rootScope.isProjectShow = true;
            $rootScope.isSettingsShow = true;
        }
        else if ($routeParams.presenter == undefined) {
            $rootScope.isProjectShow = false;
            $rootScope.isSettingsShow = true;
        }
        else if ($routeParams.presenter === "presenter") {
            $rootScope.isProjectShow = false;
            $rootScope.isSettingsShow = false;
        }
    }
}]);
