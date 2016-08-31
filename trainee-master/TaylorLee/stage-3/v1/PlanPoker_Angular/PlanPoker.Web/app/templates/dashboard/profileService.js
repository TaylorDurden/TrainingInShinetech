apiPathService.factory('webApiService', ['$http', function ($http) {
    return {
        getUserById: function (userId) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/user/' + userId
            });
        },
        editUser: function (user) {
            return $http({
                method: 'PUT',
                data: user,
                url: apiPath + '/api/user/'
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

