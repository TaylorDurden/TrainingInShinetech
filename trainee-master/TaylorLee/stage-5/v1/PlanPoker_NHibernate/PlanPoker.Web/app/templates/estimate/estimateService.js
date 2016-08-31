angular.module('EstimateModule', []).factory('webApiService', ['$http', function ($http, webApiService) {
    return {
        newEstimate: function (estimate) {
            return $http({
                method: 'POST',
                data: estimate,
                url: webApiService.apiPath + '/api/estimate'
            });
        },
        queryPokers: function (projectid) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/estimate?projectId=' + projectid
            });
        },
        changeIsShow: function (projectid) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/estimateShowCard?projectId=' + projectid
            });
        },
        checkEstimateCleared: function (projectid) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/estimateIsCleared?projectId=' + projectid
            });
        },
        deleteEstimate: function (projectId) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/estimateDelete?projectId=' + projectId
            });
        }
    };
}]);

