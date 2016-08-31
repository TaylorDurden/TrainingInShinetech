apiPathService.factory('webApiService', ['$http', function ($http) {
    return {
        login: function (userName, password) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/user?username=' + userName + '&password=' + password
            });
        }
    };
}]);

