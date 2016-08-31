app.service("userService", function ($http) {
    this.login = function (email, password) {
        var strurl = apiPath() + "/api/user/login?email=" + email + "&password=" + password;
        var result = $http.get(strurl);
        return result;

    };

    this.createUser = function (userViewModel, callback) {
        var strurl = apiPath() + "/api/user";
        $http.post(strurl, userViewModel).success(callback);
    };

    this.createUserByRegister = function (registerViewModel) {
        var strurl = apiPath() + "/api/user/register";
        return $http.post(strurl, registerViewModel);
    };

    this.deleteUser = function (userid,callback) {
        $http({
            method: "DELETE",
            url: apiPath() + "/api/user/" + userid
        }).success(callback);
    };

    this.updateUser = function (user, callback) {
        $http({
            method: "PUT",
            url: apiPath() + "/api/user",
            data: user
        }).success(callback);
    };

    this.getUserByCondition = function (condition,pageindex,pageSize) {
        var strurl = apiPath() + "/api/user?condition=" + condition + "&strpage=" + pageindex + "&strpagesize=" + pageSize;
        var result = $http.get(strurl);
        return result;
    };

    this.getUserById = function (userId) {
        return $http.get(apiPath() + "/api/user/" + userId);
    };
    
    this.getAllUser = function () {
        return $http.get(apiPath() + "/api/user");
    };
    
    this.checkUserEmailIsExist = function(email) {
        return $http.get(apiPath() + "/api/user/checkEmail?email=" + email);
    }

});
