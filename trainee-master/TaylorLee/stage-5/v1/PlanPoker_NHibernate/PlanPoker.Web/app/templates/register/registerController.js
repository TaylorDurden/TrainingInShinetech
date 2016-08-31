var registerModule = angular.module('RegisterModule', []);

registerModule.controller('registerController', function ($scope, $state, $stateParams, webApiService) {

    $scope.userRegisterModel = {
        userName: '',
        password: '',
        confirmPassword: '',
        email: '',
        image: ''
    };

    $scope.confirmFailed = false;
    $scope.rejected = false;

    $scope.$watch('userRegisterModel.confirmPassword', function () {
        $scope.confirmFailed = false;
    });

    $scope.userNameExisted = false;

    $scope.checkUser = function () {
        webApiService.checkUser($scope.userRegisterModel.userName)
        .success(function (data) {
            if (data) {
                $scope.userNameExisted = true;
            } else {
                $scope.userNameExisted = false;
            }
        });
    }

    var validatePassword;
    $scope.userRegister = function (valid) {
        if (valid) {
            if (!validatePassword($scope.userRegisterModel.password, $scope.userRegisterModel.confirmPassword)) {
                $scope.confirmFailed = true;
                return;
            }
            webApiService
            .register($scope.userRegisterModel)
            .success(function (data, status, headers, config) {
                $state.go('login');
            }).error(function (data, status, headers, config) {
                $scope.rejected = true;
                $scope.rejectedMsg = data;
            });
        }
    };
    validatePassword = function (password, confirm) {
        if (password === confirm) {
            return true;
        }
        return false;
    };
});

