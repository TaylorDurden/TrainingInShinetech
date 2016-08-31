appModule.service("profileService", function ($q, $http) {
    this.getUserById = function (userId) {
        return $http({
            method: "Get",
            url: apiPath + "/api/user/" + userId
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.editUser = function (user) {
        return $http({
            method: "Put",
            url: apiPath + "/api/user",
            data: user
        }).then(function (response) {
            return $q.when(response);
        });
    }
});
