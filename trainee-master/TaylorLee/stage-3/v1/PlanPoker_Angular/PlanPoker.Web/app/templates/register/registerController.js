var registerModule = angular.module('RegisterModule', []);

registerModule.controller('registerController', function ($scope, $state, $stateParams, webApiService) {
    $scope.userRegisterModel = {
        userName: '',
        password: '',
        confirmPassword: '',
        email: '',
        image: ''
    };

    $scope.confirmFailed = false;
    $scope.rejected = false;

    $scope.$watch('userRegisterModel.confirmPassword', function (newValue, oldValue) {
        $scope.confirmFailed = false;
    });

    $scope.userNameExisted = false;

    $scope.checkUser = function () {
        webApiService.checkUser($scope.userRegisterModel.userName)
        .success(function (data, status, headers, config) {
            if (data) {
                $scope.userNameExisted = true;
            } else {
                $scope.userNameExisted = false;
            }
        }).error(function (data, status, headers, config) {
        });
    }

    $scope.userRegister = function (valid) {
        if (valid) {
            if (!validatePassword($scope.userRegisterModel.password, $scope.userRegisterModel.confirmPassword)) {
                $scope.confirmFailed = true;
                return;
            }
            webApiService
            .register($scope.userRegisterModel)
            .success(function (data, status, headers, config) {
                $state.go('login');
            }).error(function (data, status, headers, config) {
                $scope.rejected = true;
                $scope.rejectedMsg = data;
            });
        }
    };

    var validatePassword = function (password, confirm) {
        if (password === confirm) {
            return true;
        }
        return false;
    }
});

registerModule.directive('fileReader', function ($q) {
    var slice = Array.prototype.slice;

    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;

            ngModel.$render = function () { };

            element.bind('change', function (e) {
                var element = e.target;

                $q.all(slice.call(element.files, 0).map(readFile))
                    .then(function (values) {
                        if (element.multiple) {
                            ngModel.$setViewValue(values);
                        } else {
                            ngModel.$setViewValue(values.length ? values[0] : null);
                        }
                    });

                function readFile(file) {
                    var deferred = $q.defer();

                    var reader = new FileReader();
                    reader.onload = function (e) {
                        deferred.resolve(e.target.result);
                    };
                    reader.onerror = function (e) {
                        deferred.reject(e);
                    };
                    reader.readAsDataURL(file);
                    return deferred.promise;
                }
            });

        }
    };
});
