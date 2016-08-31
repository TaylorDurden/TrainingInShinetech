appModule.controller("profileController", function ($scope, $cookieStore, loginService, profileService) {
    loginService.loginCheck().then(function (response) {
        if ($cookieStore.get("User_Id_Cookie") !== response.data.UserId) {
            $location.path("/login");
        }
    });
    $scope.user = {};
    $scope.user.userId = $cookieStore.get("User_Id_Cookie");

    $scope.uploadUserPicture = function () {
        $scope.user.image = $scope.image;
    };

    profileService.getUserById($scope.user.userId).then(function (response) {
        $scope.user.username = response.data.UserName;
        $scope.user.email = response.data.Email;
        if (response.data.image != null || response.data.Image !== '') {
            $scope.user.image = response.data.Image;
        }
        else {
            $scope.user.image = "../image/user.png";
        }
    });

    $scope.updateUser = function () {
        loginService.login($scope.user.username, $scope.user.oldpassword).then(function (response) {
            console.log(response.data.Status);
            if (response.data.Status) {
                profileService.editUser($scope.user).then(function () {
                    $scope.message = "update sucess";
                    $scope.isStatus = true;
                });
            } else {
                $scope.message = "old password is wrong";
                $scope.isStatus = false;
            }
        });
    }
})