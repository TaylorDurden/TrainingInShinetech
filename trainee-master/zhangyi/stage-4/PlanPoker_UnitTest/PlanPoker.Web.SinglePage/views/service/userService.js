angular.module("mainApp").service("userService", ["$http", "$q", function ($http, $q) {

    this.login = function (user) {
        var delay = $q.defer();
        $http({
            method: "Post",
            url: apiPath + "/api/userLogin",
            data: user
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    };

    this.createUser = function (user) {
        var delay = $q.defer();
        $http({
            method: "POST",
            url: apiPath + "/api/user",
            data: JSON.stringify(user)
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.checkUserNameExist = function (userName) {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/user/search?userName=" + userName
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.initUserProfile = function (userId) {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/user/" + userId
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.modifyUser = function (user) {
        var delay = $q.defer();
        $http({
            method: "Put",
            url: apiPath + "/api/user",
            data: user
        }).success(function () {
            delay.resolve();
        });
        return delay.promise;
    }

}])