// controller for the register page
appModule.controller('registerController', function ($scope, $http, $location) {
    $scope.user = { username: "", password: "", confirmPassword: "", email: "", image: "" };

    $scope.changeImage = function () {
        $scope.user.image = $scope.image;
    };

    $scope.signinupSubmit = function () {
        $http({
            method: 'POST',
            url: apiPath() + "/api/user",
            data: $scope.user,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (response) {
            if (response == '' || response == null) {
                console.log("ok");
                $location.path('/login');
            } else {
                $scope.message = response;
                $scope.isexist = true;
            }
        }).error(function () {
            console.log("error");
        })
    }
})