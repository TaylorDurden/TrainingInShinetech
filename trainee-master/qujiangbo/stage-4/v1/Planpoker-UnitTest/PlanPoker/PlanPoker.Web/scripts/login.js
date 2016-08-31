planPokerApp().controller('signinCtrl', function ($scope, $http) {
    $scope.signinSubmit = function () {
        $http.get(
            apiPath() + "/api/user?username=" + $scope.username + "&password=" + $scope.password).success(function (response) {
                console.log(response);
                if (response > 0)
                {
                    Cookies(User_Name_Cookie, $scope.username, { expires: 1 });
                    Cookies(User_Id_Cookie, response, { expires: 1 });
                    location.href = 'dashboard.html';
                } else {
                    $scope.message = response;
                    $scope.iserror = true;
                }
            });
    };
})


