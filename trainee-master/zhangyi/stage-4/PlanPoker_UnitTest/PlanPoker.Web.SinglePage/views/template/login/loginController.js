angular.module("mainApp").controller("loginController", ["$rootScope", "$scope", "userService", "$cookieStore", function ($rootScope,$scope, userService, $cookieStore) {
    $rootScope.isShowNewProject = false;
    $rootScope.isShowSettings = false;
    $scope.user = {
        isError: false,
        message: ""
    };

    $scope.onlogin = function () {
        userService.login($scope.user).then(function (data) {
            if (Number(data) > 0) {
                $cookieStore.put(User_Name_Cookie, $scope.user.username);
                $cookieStore.put(User_Id_Cookie, data);
                redirectToDashboard();
            } else {
                $scope.user.isError = true;
                $scope.user.message = "User name or password is invalid";
            }
        });
    };
}]);