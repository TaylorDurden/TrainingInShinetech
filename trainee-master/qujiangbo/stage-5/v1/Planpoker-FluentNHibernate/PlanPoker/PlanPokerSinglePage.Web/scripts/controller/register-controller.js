appModule.controller("registerController", function ($scope, $location, registerService) {
    $scope.user = {  };

    $scope.uploadUserPicture = function () {
        $scope.user.image = $scope.image;
    };

    $scope.registerUser = function () {
        registerService.createUser($scope.user).then(function (response) {
            if (response.data === "" || response.data == null) {
                $location.path("/login");
            } else {
                $scope.message = response.data;
                $scope.isExist = true;
            }
        });
    }
})