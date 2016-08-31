angular.module("mainApp").controller("registerController", ["$rootScope", "$scope", "$http", "userService", "$cookieStore", function ($rootScope, $scope, $http, userService, $cookieStore) {
    $rootScope.isShowNewProject = false;
    $rootScope.isShowSettings = false;
    $scope.user = {
        isExist: false,
        Image: "upload/user.png"
    };

    $scope.onUserCreate = function () {
        userService.createUser($scope.user).then(function (data) {
            $cookieStore.put(User_Name_Cookie, $scope.user.UserName);
            $cookieStore.put(User_Id_Cookie, data);
            redirectToDashboard();
        });
    }

    $scope.onUserNameChange = function () {
        userService.checkUserNameExist($scope.user.UserName).then(function (data) {
            $scope.user.isExist = (data.length > 0);
        });
    }

}]);

