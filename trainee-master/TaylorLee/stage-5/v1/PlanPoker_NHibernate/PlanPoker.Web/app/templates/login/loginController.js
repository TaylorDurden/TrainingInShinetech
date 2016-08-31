var loginModule = angular.module('myApp', []);

angular.module('myApp', []).controller('loginController', function ($scope, $state, $stateParams, $cookieStore, webApiService) {
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
            .success(function (data) {
                if (!isNaN(data)) {
                    $cookieStore.put(User_Name_Cookie, $scope.userLoginModel.userName);
                    $cookieStore.put(User_Id_Cookie, data);
                    $state.go('dashboard');
                } else {
                    $scope.loginFailed = true;
                    $scope.failedMsg = data;
                }
            });
        }
    };
});