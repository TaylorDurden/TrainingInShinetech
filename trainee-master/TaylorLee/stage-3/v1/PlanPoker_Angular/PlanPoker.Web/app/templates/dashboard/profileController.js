var profileModule = angular.module('ProfileModule', []);

profileModule.controller('modalProfileController', function ($scope, $uibModal, $log) {
    $scope.open = function (size) {
        var modalInstance = $uibModal.open({
            templateUrl: 'ModalProfile.html',
            controller: 'modalProfileInstanceController',
            size: size
        });
        modalInstance.result.then(function (selectedItem) {
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    }
});

profileModule.controller('modalProfileInstanceController', function ($rootScope, $scope, $uibModalInstance, webApiService) {

    $scope.user = {};
    webApiService.getUserById(Cookies(User_Id_Cookie))
    .success(function (data) {
        $scope.user = data;
    })
    .error(function () { });

    $scope.userNameExisted = false;

    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        if (!validatePassword($scope.user.Password, $scope.confirmPassword)) {
            $scope.confirmFailed = true;
            return;
        }
        $uibModalInstance.close();
        editUser();
    };

    var editUser = function () {
        var isExisted = false;
        webApiService.editUser($scope.user)
        .success(function (data, status, headers, config) {
        }).error(function (data, status, headers, config) {
        });

        return isExisted;
    }

    $scope.$watch('confirmPassword', function (newValue, oldValue) {
        $scope.confirmFailed = false;
    });

    var validatePassword = function (password, confirm) {
        if (password === confirm) {
            return true;
        }
        return false;
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
});
