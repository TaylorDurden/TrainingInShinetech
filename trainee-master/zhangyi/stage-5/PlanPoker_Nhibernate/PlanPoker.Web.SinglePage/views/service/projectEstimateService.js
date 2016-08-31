angular.module("mainApp").service("projectEstimateService", ["$http", "$q", function ($http, $q) {

    this.submitEstimate = function (estimate) {
        var delay = $q.defer();
        if (estimate.ProjectId) {

            $http({
                method: "POST",
                url: apiPath + "/api/estimate",
                data: JSON.stringify(estimate)
            }).success(function () {
                delay.resolve();
            });
        }
        return delay.promise;
    }

    this.initProjectEstimate = function (currentProjectGuid) {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/projectid/" + currentProjectGuid
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.queryAppendPoker = function (projectIdEstimate) {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/estimate?projectId=" + projectIdEstimate
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.startNewEstimate = function (projectIdEstimate) {
        var delay = $q.defer();
        $http({
            method: "Delete",
            url: apiPath + "/api/estimateDelete?projectId=" + projectIdEstimate
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.CheckEstimateIsFinished = function (projectIdEstimate) {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/estimateIsCleared?projectId=" + projectIdEstimate
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

}])