app.controller("profileController", function($scope, $location, $cookieStore, userService) {
    checkCookie($cookieStore, $location);

    $scope.user = {
        userid: 0,
        username: "",
        password: "",
        email: "",
        image: ""
    };

    var originalPassword = "";

    userService.getUserById($cookieStore.get("Cookie_User_Id")).then(function (ret) {
        console.log("Success!");

        $scope.user.username = ret.data.UserName;
        $scope.user.email = ret.data.Email;
        $scope.user.image = ret.data.Image;

        originalPassword = ret.data.Password;
    });

    $scope.changeImage = function() {
        $scope.user.image = $scope.image;
    };

    $scope.UpdateProfile = function() {
        if ($scope.oldpassword === originalPassword) {
            $scope.user.userid = $cookieStore.get("Cookie_User_Id");
            $scope.user.password = $scope.newpassword;
            $scope.user.image = $scope.image;
            userService.updateUser($scope.user).then(function () {
                console.log("Update profile success!");
                $location.path('/dashboard');
            });
        } else {
            $scope.updateErrorTips = "the old password is wrong, please check it and input again.";
        }
    };
});