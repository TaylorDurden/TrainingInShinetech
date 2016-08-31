app.service("estimateService", function ($http) {
    this.createEstimate = function (estimate) {
        var request = $http({
            method: "POST",
            url: apiPath() + "/api/estimate",
            data: estimate,
            async: false
        });
        return request;
    };

    this.deleteEstimate = function (projectId) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/estimateDelete?projectId=" + projectId,
            async: false
        });
        return request;
    };

    this.getPokerViewModels = function (projectId) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/estimates?projectId=" + projectId,
            async: false
        });
        return request;
    }

    this.showCards = function (projectId) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/estimatesShowCard?projectId=" + projectId,
            async: false
        });
        return request;
    };
});