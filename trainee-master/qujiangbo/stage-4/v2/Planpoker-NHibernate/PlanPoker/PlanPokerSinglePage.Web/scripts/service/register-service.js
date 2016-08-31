appModule.service("registerService", function ($q, $http) {
    this.createUser = function (user) {
        return $http({
            method: "POST",
            url: apiPath + "/api/user",
            data:user
        }).then(function (response) {
            return $q.when(response);
        });
    }
});