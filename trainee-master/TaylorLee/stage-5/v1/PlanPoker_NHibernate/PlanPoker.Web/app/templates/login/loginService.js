angular.module('LoginModule', [])
    .factory('webApiService', ['$http', function ($http, webApiService) {
    return {
        login: function (userName, password) {
            return $http({
                method: 'POST',
                url: webApiService.apiPath + '/api/user?username=' + userName + '&password=' + password
            });
        }
    };
}]);

