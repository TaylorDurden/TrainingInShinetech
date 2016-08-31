angular.module("mainApp").controller('loginController',['$scope', '$http','userService', function ($scope, $http,userService) {

    $scope.user = {
        iserror: false,
        message:''
    };

    $scope.onlogin = function () {
        userService.login($scope);
    };
}]);