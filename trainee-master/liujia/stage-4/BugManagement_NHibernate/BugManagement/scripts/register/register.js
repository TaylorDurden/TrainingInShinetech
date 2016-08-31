app.controller("registerController", function ($scope, $location, $route, $cookies, $cookieStore, userService) {

    $scope.EmailChange = function () {
        if ($scope.user.email !== "") {
            userService.checkUserEmailIsExist($scope.user.email,"").then(
                function (response) {
                    $scope.isExist = response.data;
                });
        }
    }

    $scope.register = function () {
        userService.createUserByRegister($scope.user).then(
            function() {
                userService.login($scope.user.email, $scope.user.password).then(
                    function(result) {
                        if (result.data != null) {
                            $cookieStore.put(Cookie_UserId, result.data.UserId);
                            $cookieStore.put(Cookie_UserName, result.data.FristName + " " + result.data.LastName);
                            $cookieStore.put(Cookie_Email, result.data.Email);
                            $location.path("/dashboard");
                        }
                    }, function(err) { $scope.registerErrorMessage = err.data });
            });
    }
});

app.directive('passwordCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = "#" + attrs.passwordCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('passwordmatch', v);
                });
            });
        }
    }
}]);