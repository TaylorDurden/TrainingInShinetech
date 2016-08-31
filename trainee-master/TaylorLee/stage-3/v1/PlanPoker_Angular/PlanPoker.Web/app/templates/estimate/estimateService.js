apiPathService.factory('webApiService', ['$http', function($http) {
    return {
        newEstimate: function (estimate) {
            return $http({
                method: 'POST',
                data: estimate,
                url: apiPath + '/api/estimate'
            });
        },
        queryPokers: function (projectid) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/estimate?projectId=' + projectid
            });
        },
        changeIsShow: function (projectid) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/estimateShowCard?projectId=' + projectid
            });
        },
        checkEstimateCleared: function (projectid) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/estimateIsCleared?projectId=' + projectid
            });
        },
        deleteEstimate: function (projectId) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/estimateDelete?projectId=' + projectId
            });
        }
    };
}]);

