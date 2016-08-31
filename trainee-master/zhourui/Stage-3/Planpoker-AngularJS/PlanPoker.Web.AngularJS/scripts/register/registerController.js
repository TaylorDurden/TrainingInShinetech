app.controller("registerController", function($scope, $location, userService) {
    $scope.user = {
        userid: 0,
        username: "",
        password: "",
        email: "",
        image: ""
    };

    $scope.user.image = "images/user.png";

    $scope.register = function() {
        var promisePost = userService.createUser($scope.user);
        promisePost.then(function() {
            console.log("Success!");
            $location.path("/login");
        }, function(err) {
            console.log(err);
            $scope.signupErrorTips = err.data;
        });
    }
});