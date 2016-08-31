// controller for the profile page
appModule.controller('profileController', function ($scope, $http, userLogin) {
    userLogin.loginCheck();
    var oldpassword = "";
    $scope.user = { userId: '', username: "", password: "", confirmPassword: "", email: "", image: "" };
    $scope.user.userId = getQueryString(User_Id_Cookie);
    $http.get(apiPath() + "/api/user/" + $scope.user.userId).success(function (response) {
        $scope.user.username = response.UserName;
        $scope.user.email = response.Email;
        if (response.image != null || response.Image != '') {
            $scope.user.image = response.Image;
        }
        else {
            $scope.user.image = '../image/user.png';
        }
        oldpassword = response.Password;
    })

    $scope.changeImage = function () {
        $scope.user.image = $scope.image;
    };

    $scope.updateSubmit = function () {
        if ($scope.user.oldpassword == oldpassword) {
            $http({
                method: 'Put',
                url: apiPath() + "/api/user",
                data: $scope.user
            }).success(function () {
                $scope.message = 'update sucess';
                $scope.isStatus = true;
            }).error(function () {
                console.log("error");
            })
        } else {
            $scope.message = 'old password is wrong';
            $scope.isStatus = false;
        }
    }
})