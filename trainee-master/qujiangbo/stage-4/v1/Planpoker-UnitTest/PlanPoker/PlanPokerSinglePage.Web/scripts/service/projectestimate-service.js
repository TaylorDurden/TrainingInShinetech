appModule.service("estimateService", function ($q, $http) {
    this.getProjectByguid = function (projectGuid) {
        return $http({
            method: "Get",
            url: apiPath + "/api/projectguid/" + projectGuid
        }).then(function(response) {
            return $q.when(response);
        });
    }

    this.clearEstimateAndRedirect = function (projectId) {
        return $http({
            method: "GET",
            url: apiPath + "/api/estimateDelete?projectId=" + projectId
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.queryAppendPoker = function (projectId) {
        return $http({
            method: "Get",
            url: apiPath + "/api/estimate?projectId=" + projectId
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.changePokerIsShow = function (projectId) {
        return $http({
            method: "GET",
            url: apiPath + "/api/estimateShowCard?projectId=" + projectId
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.queryFinished = function (projectId) {
        return $http({
            method: "GET",
            url: apiPath + "/api/estimateIsCleared?projectId=" + projectId
        }).then(function (response) {
            return $q.when(response);
        });
    }
});
