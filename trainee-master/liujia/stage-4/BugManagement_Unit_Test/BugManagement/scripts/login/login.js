app.controller("loginController", function ($scope, $rootScope, $location, $http, $cookies, $cookieStore, userService) {

    $scope.onLogin = function () {
        userService.login($scope.email, $scope.password).then(
            function (result) {
                if (result.data != null) {
                    $cookieStore.put(Cookie_UserId, result.data.UserId);
                    $cookieStore.put(Cookie_UserName, result.data.FristName + " " + result.data.LastName);
                    $cookieStore.put(Cookie_Email, result.data.Email);
                    $location.path("/dashboard");
                } else {
                    $scope.loginErrorMessage = "email or password wrong!";
                }
            }, function (result) { $scope.loginErrorMessage = result.data });
    };

    $scope.logout = function () {
        $cookieStore.remove(Cookie_UserId);
        $cookieStore.remove(Cookie_UserName);
        $cookieStore.remove(Cookie_Email);
        $location.path("/login");
    };
});