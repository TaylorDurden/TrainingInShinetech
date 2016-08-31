apiPathService.factory('webApiService', ['$http', function ($http) {
    return {
        register: function (registerModel) {
            return $http({
                method: 'POST',
                data: registerModel,
                url: apiPath + '/api/user'
            });
        },
        checkUser: function (userName) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/userCheck?userName=' + userName
            });
        }
    };
}]);

