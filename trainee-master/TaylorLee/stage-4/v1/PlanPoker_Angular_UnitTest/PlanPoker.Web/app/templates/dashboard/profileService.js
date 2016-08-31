angular.module('ProfileModule', [])
    .factory('webApiService', ['$http', function ($http, webApiService) {
    return {
        getUserById: function (userId) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/user/' + userId
            });
        },
        editUser: function (user) {
            return $http({
                method: 'PUT',
                data: user,
                url: webApiService.apiPath + '/api/user/'
            });
        },
        checkUser: function (userName) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/userCheck?userName=' + userName
            });
        },
        validatePassword: function (userName, password) {
            return $http({
                method: 'POST',
                url: webApiService.apiPath + '/api/validatePassword?userName=' + userName + '&password=' + password
            });
        }
    };
}]);

