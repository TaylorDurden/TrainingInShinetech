appModule.controller("loginController", function ($scope, $location, $cookieStore, loginService) {
    $scope.login = function () {
        loginService.login($scope.username, $scope.password).then(function (response) {
            if (response.data.Status) {
                $cookieStore.put("User_Id_Cookie", response.data.UserId);
                $cookieStore.put("User_Name_Cookie", $scope.username);
                $location.path('/dashboard');
            } else {
                $scope.message = response.data.Message;
                $scope.isError = true;
            }
        });
    }
});
