angular.module('RegisterModule', [])
    .factory('webApiService', ['$http', function ($http, webApiService) {
    return {
        register: function (registerModel) {
            return $http({
                method: 'POST',
                data: registerModel,
                url: webApiService.apiPath + '/api/user'
            });
        },
        checkUser: function (userName) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/userCheck?userName=' + userName
            });
        }
    };
}]);

