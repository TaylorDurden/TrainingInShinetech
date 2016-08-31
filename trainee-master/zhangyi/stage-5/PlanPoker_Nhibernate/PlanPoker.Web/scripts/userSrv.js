mainApp.service("userService",['$http', function ($http) {

    this.login = function ($scope) {
        $('#signIn').startSpin();
        var request =
        $http({
            method: 'Get',
            url: apiPath() + '/api/userLogin?username=' + $scope.user.username + '&password=' + $scope.user.password
        }).success(function (data) {
            if (!isNaN(data)) {
                Cookies(User_Name_Cookie, $scope.user.username, { expires: 1 });
                Cookies(User_Id_Cookie, data, { expires: 1 });
                redirectToDashboard();
            } else {
                $scope.user.iserror = true;
                $scope.user.message = data;
            }
            $('#signIn').stopSpin();
        }).error(function (e) {
            $('#signIn').stopSpin();
        });
        return request;
    };

    this.createUser= function($scope) {
        $scope.user.Image = $('#hideImage').val();
        $('#btnSubmit').startSpin();
        $http({
            method: 'POST',
            url: apiPath() + '/api/user',
            data: JSON.stringify($scope.user)
        }).success(function (data) {
            Cookies(User_Name_Cookie, $scope.user.UserName, { expires: 1 });
            Cookies(User_Id_Cookie, data, { expires: 1 });
            redirectToDashboard();
            $('#btnSubmit').stopSpin();
        }).error(function (e) {
            $('#signIn').stopSpin();
        });
    }

    this.userNameChange = function ($scope) {
        $http({
            method: "Get",
            url: apiPath() + "/api/user/search?userName=" + $scope.user.UserName
        }).success(function (data) {
            $scope.user.isExist = (data.length > 0);
        }).error(function (e) {
        });
    }

    this.userProfileInit = function ($scope,userId) {
        $http({
            method: "Get",
            url: apiPath() + "/api/user/" + userId
        }).success(function (data) {
            $scope.user.UserName = data.UserName;
            $scope.user.Email = data.Email;
            $scope.user.Image = data.Image;
        }).error(function (e) {
        });
    }

    this.userModify = function ($scope) {
        $scope.user.Image = $('#hideImage').val();
        $http({
            method: "Put",
            url: apiPath() + '/api/user',
            data: $scope.user
        }).success(function (data) {
            $('#updateProfileModal').modal({ keyboard: false });
            $('#btnSubmit').stopSpin();
        }).error(function () {
            $('#btnSubmit').stopSpin();
        });
    }

}])