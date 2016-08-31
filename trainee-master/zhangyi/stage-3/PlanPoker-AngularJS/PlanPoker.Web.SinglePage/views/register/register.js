
angular.module("mainApp").controller('registerController', ['$scope', '$http', 'userService', function ($scope, $http, userService) {

    $scope.user = {
        isExist: false,
        Image: 'upload/user.png'
    };

    $scope.oncreateUser = function () {
        userService.createUser($scope);
    }

    $scope.onUserNamechange = function () {
        userService.userNameChange($scope);
    }

}]);

