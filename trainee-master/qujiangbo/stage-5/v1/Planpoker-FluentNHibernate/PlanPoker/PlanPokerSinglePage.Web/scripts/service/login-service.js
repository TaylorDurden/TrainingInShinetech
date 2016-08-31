appModule.service("loginService", function ($q, $http, $location, $cookieStore) {
    this.login = function (userName, passWord) {
        return $http({
            method: "POST",
            url: apiPath + "/api/user?username=" + userName + "&password=" + passWord
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.loginCheck = function () {
        return $http({
            method: "Get",
            url: apiPath + "/api/user/" + $cookieStore.get("User_Id_Cookie")
        }).then(function (response) {
            return $q.when(response);
        });
    }
});