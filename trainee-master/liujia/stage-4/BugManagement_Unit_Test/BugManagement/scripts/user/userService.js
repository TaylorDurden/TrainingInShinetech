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

    this.getUserListViewModelByCondition = function (condition,pageindex) {
        var strurl = apiPath() + "/api/user?condition=" + condition + "&strpage=" + pageindex;
        var result = $http.get(strurl);
        return result;
    };

    this.getUserById = function (userId) {
        return $http.get(apiPath() + "/api/user/" + userId);
    };
    
    this.getUsers = function () {
        return $http.get(apiPath() + "/api/user");
    };
    
    this.checkUserEmailIsExist = function(email,userId) {
        return $http.get(apiPath() + "/api/user/checkEmail?email=" + email + "&userId=" + userId);
    }

    this.getUserTypes = function () {
        return $http.get(apiPath() + "/api/user/getUserTypes");
    };

    this.getUserStatuss = function () {
        return $http.get(apiPath() + "/api/user/getUserStatuss");
    };
});
