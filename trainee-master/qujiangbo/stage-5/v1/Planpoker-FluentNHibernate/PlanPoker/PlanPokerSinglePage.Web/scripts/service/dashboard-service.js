appModule.service("dashboardService", function ($q, $http) {
    this.getAllProjects = function () {
        return $http.get(apiPath + "/api/project").then(function (response) {
            return $q.when(response);
        });
    }

    this.changeProject = function (projectId) {
        return $http.get(apiPath + "/api/getprojecturl/" + projectId).then(function (response) {
            return $q.when(response);
        },function (response) {
            return $q.when(response);
        });
    }

    this.createEstimate = function (estimate) {
        return $http({
            method: "POST",
            url: apiPath + "/api/estimate",
            data: estimate
        }).then(function (response) {
            return $q.when(response);
        });
    }
});
