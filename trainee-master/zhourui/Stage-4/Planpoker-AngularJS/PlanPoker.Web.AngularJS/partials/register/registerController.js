app.controller("registerController", function($scope, $location, userService) {
    $scope.user = {};

    $scope.user.image = "images/user.png";

    $scope.register = function() {
        userService.createUser($scope.user)
            .success(function(returnObject) {
                if (returnObject.Message) {
                    $scope.signupErrorTips = returnObject.Message;
                    return;
                }

                $location.path("/login");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };
});