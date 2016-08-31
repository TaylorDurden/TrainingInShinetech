app.service("userService", function ($http) {
    this.createUser = function (user) {
        var request = $http({
            method: "POST",
            url: apiPath() + "/api/user",
            data: user
        });
        return request;
    };

    this.deleteUser = function (userid) {
        var request = $http({
            method: "DELETE",
            url: apiPath() + "/api/user/" + userid
        });
        return request;
    };

    this.updateUser = function (user) {
        var request = $http({
            method: "PUT",
            url: apiPath() + "/api/user",
            data: user
        });
        return request;
    };

    this.login = function (username, password) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/user/login?username=" + username + "&password=" + password
        });
        return request;
    };

    this.getAllUser = function () {
        return $http.get(apiPath() + "/api/user");
    };

    this.getUserById = function (userId) {
        return $http.get(apiPath() + "/api/user/" + userId);
    };

    this.getUserByName = function (userName) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/user?username=" + userName
        });
        return request;
    };
});