app
    .service("loginService", function($http) {
        //Login
        this.get = function(userName, passWord) {
            var request = $http({
                method: "GET",
                url: "/api/user",
                data: { username: userName, password: passWord }
            });
            return request;
        };
    });