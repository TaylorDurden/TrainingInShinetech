app.controller("profileController", function($scope, $location, $cookieStore, timerService, userService) {
    checkCookie($cookieStore, $location);

    timerService.cancelTimer();

    $scope.user = {};
    $scope.userTemp = {};

    userService.getUserById($cookieStore.get("Cookie_User_Id"))
        .success(function (returnObject) {
            $scope.user.Username = returnObject.UserName;
            $scope.user.Email = returnObject.Email;
            $scope.user.Image = returnObject.Image;
        })
        .error(function(errorMessage) { console.log(errorMessage); });

    $scope.changeImage = function() { $scope.user.Image = $scope.image; };

    $scope.UpdateProfile = function() {
        userService.login($scope.user.Username, $scope.oldpassword)
            .success(function(returnObject1) {
                if (returnObject1.Status) {
                    $scope.userTemp = returnObject1;
                    $scope.user.Password = $scope.newpassword;
                    userService.updateUser($scope.user)
                        .success(function(returnObject2) {
                            if (returnObject2.Message) {
                                $scope.updateErrorTips = returnObject2.Message;
                                return;
                            }

                            $location.path("/dashboard");
                        })
                        .error(function(errorMessage) { console.log(errorMessage); });
                } else {
                    $scope.updateErrorTips = returnObject1.Message;
                }
            });
    };
});