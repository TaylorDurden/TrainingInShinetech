angular.module("mainApp").controller("profileController", ["$rootScope","$scope", "$http", "checkUserProvider", "userService", "$cookieStore", function ($rootScope,$scope, $http, checkUserProvider, userService, $cookieStore) {
    checkUserProvider.checkUser();
    $rootScope.isShowNewProject = false;
    $rootScope.isShowSettings = true;
    var userId = $cookieStore.get(User_Id_Cookie);
    $scope.user = {};
    $scope.user.userId = userId;

    $scope.onProfileInit = function () {
        userService.initUserProfile(userId).then(function (data) {
            $scope.user.UserName = data.UserName;
            $scope.user.Email = data.Email;
            $scope.user.Image = data.Image;
        });
    };

    $scope.onModify = function () {
        userService.modifyUser($scope.user).then(function () {
            $("#updateProfileModal").modal({ keyboard: false });
        });
    }

    $scope.redirectToDashboard = function () {
        redirectToDashboard();
    }
}]);