var loginModule = angular.module('LoginModule', []);

loginModule.controller('loginController', function ($scope, $state, $stateParams, webApiService) {
    $scope.userLoginModel = {
        userName: '',
        password: ''
    };
    $scope.loginFailed = false;

    $scope.userLogin = function (valid) {
        if (valid) {
            var userName = $scope.userLoginModel.userName;
            var password = $scope.userLoginModel.password;
            $scope.submitted = false;

            webApiService
            .login(userName, password)
            .success(function (data, status, headers, config) {
                if (!isNaN(data)) {
                    Cookies(User_Name_Cookie, $scope.userLoginModel.userName, { expires: 1 });
                    Cookies(User_Id_Cookie, data, { expires: 1 });
                    $state.go('dashboard');
                } else {
                    $scope.loginFailed = true;
                    $scope.failedMsg = data;
                }
            }).error(function (data, status, headers, config) {

            });
        }
    };
});