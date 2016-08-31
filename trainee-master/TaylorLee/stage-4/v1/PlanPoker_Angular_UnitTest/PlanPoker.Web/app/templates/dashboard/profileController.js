var profileModule = angular.module('ProfileModule', []);

profileModule.controller('modalProfileController', function ($scope, $uibModal) {
    $scope.open = function (size) {
        $uibModal.open({
            templateUrl: 'ModalProfile.html',
            controller: 'modalProfileInstanceController',
            size: size
        });
    }
});

profileModule.controller('modalProfileInstanceController', function ($rootScope, $scope, $cookieStore, $uibModalInstance, webApiService) {

    $scope.user = {};
    webApiService.getUserById($cookieStore.get(User_Id_Cookie))
    .success(function (data) {
        $scope.user = data;
    });

    var editUser;

    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        webApiService.validatePassword($scope.user.UserName, $scope.confirmPassword)
            .success(function (data) {
                if (data) {
                    $uibModalInstance.close();
                    editUser();
                } else {
                    $scope.confirmFailed = true;
                    return;
                }
            });
    };

    editUser = function () {
        webApiService.editUser($scope.user);
    };

    $scope.$watch('confirmPassword', function () {
        $scope.confirmFailed = false;
    });

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
});
