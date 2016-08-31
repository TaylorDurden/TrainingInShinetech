app
    .controller("loginController", function($scope, loginService) {
        $scope.login = function() {
            var promiseLogin = loginService.get($scope.username, $scope.password);
            promiseLogin.then(
                function() { alert("OK"); },
                function() { alert("Err"); }
            );
        };
    });