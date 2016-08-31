app.controller("loginController", function($scope, $rootScope, $location, $cookieStore, userService) {
    $scope.login = function() {
        userService.login($scope.user.username, $scope.user.password)
            .success(function(returnObject1) {
                if (!returnObject1.Message) {
                    userService.getUserByName($scope.user.username)
                        .success(function(returnObject2) {
                            console.log("Get User Info Success");
                            $rootScope.stopTimer = false;
                            $cookieStore.put("Cookie_User_Id", returnObject2.UserId);
                            $cookieStore.put("Cookie_User_Name", returnObject2.UserName);
                            $location.path("/dashboard");
                            console.log("Login Success!");
                        })
                        .error(function(errorMessage) { console.log(errorMessage) });
                } else {
                    $scope.signinErrorTips = returnObject1.Message;
                }
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };
});