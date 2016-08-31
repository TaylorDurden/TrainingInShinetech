appModule.controller("mainController", function ($scope, $location, $cookieStore) {
    $scope.removeCookie = function () {
        $cookieStore.remove("User_Id_Cookie");
        $cookieStore.remove("User_Name_Cookie");
        $cookieStore.remove("Project_Id_Cookie");
        $location.path("/login");
    }
})