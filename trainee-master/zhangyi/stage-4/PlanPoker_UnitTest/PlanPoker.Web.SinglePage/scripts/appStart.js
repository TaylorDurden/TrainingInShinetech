var apiPath = "http://localhost:9127";
//var apiPath = "http://192.168.1.105:9127";

angular.module("mainApp", ["ui.router", "ngCookies"]);

angular.module("mainApp").controller("indexController", ["$scope","$location","$cookieStore", function ($scope, $location, $cookieStore) {
    $scope.removeCookie = function () {
        $cookieStore.remove(User_Name_Cookie);
        $cookieStore.remove(User_Id_Cookie);
        $cookieStore.remove(Project_Id_Cookie);
        $cookieStore.remove(Presenter_Cookie);
        $cookieStore.remove(Project_GUID_Cookie);
        $location.path("/main/login");
    }
}]);