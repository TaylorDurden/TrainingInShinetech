app.controller("loginController", function ($scope, $rootScope, $location, $cookieStore, userService) {
    $scope.login = function () {
        userService.login($scope.user.username, $scope.user.password).then(
            function () {
                console.log("Login Success!");
                userService.getUserByName($scope.user.username).then(function (ret) {
                    console.log("Get User Info Success");
                    $rootScope.timerStop = false;
                    $cookieStore.put("Cookie_User_Id", ret.data.UserId);
                    $cookieStore.put("Cookie_User_Name", ret.data.UserName);
                    $location.path("/dashboard");
                }, function (err) { console.log(err) });
            }, function () { $scope.signinErrorTips = "user name of password wrong!" });
    };
});