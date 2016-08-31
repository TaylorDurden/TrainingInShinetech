app.service("globalService", function ($rootScope, $interval) {
    $rootScope.timerStop = false;
    this.timerCancel = function () {
        $rootScope.timerStop = true;
        $interval.cancel($rootScope.timer1);
        $interval.cancel($rootScope.timer2);
    };
});

app.service("userService", function ($http) {
    this.login = function (username, password) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/user/login?username=" + username + "&password=" + password
        });
        return request;
    };

    this.createUser = function (user) {
        var request = $http({
            method: "POST",
            url: apiPath() + "/api/user",
            data: user
        });
        return request;
    };

    this.deleteUser = function (userid) {
        var request = $http({
            method: "DELETE",
            url: apiPath() + "/api/user/" + userid
        });
        return request;
    };

    this.updateUser = function (user) {
        var request = $http({
            method: "PUT",
            url: apiPath() + "/api/user",
            data: user
        });
        return request;
    };

    this.getAllUser = function () {
        return $http.get(apiPath() + "/api/user");
    };

    this.getUserById = function (userId) {
        return $http.get(apiPath() + "/api/user/" + userId);
    };

    this.getUserByName = function (userName) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/user?username=" + userName
        });
        return request;
    };
});

app.service("projectService", function($http) {
    this.createProject = function(project) {
        var request = $http({
            method: "POST",
            url: apiPath() + "/api/project",
            data: project,
            async: false
        });
        return request;
    };

    this.updateProject = function(project) {
        var request = $http({
            method: "PUT",
            url: apiPath() + "/api/project",
            data: project,
            async: false
        });
        return request;
    };

    this.deleteProject = function(projectId) {
        var request = $http({
            method: "DELETE",
            url: apiPath() + "/api/project/" + projectId,
            async: false
        });
        return request;
    };

    this.getAllProjects = function () { return $http.get(apiPath() + "/api/project"); };
});

app.service("dashboardService", function ($http) {

    this.getProjectByName = function(name) {
        return $http.get(apiPath() + "/api/project?name=" + name);
    };

    this.getProjectGuid = function (projectId) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/projectid/" + projectId,
            async: false
        });
        return request;
    };
});

app.service("estimateService", function($http) {
    this.createEstimate = function(estimate) {
        var request = $http({
            method: "POST",
            url: apiPath() + "/api/estimate",
            data: estimate,
            async: false
        });
        return request;
    };

    this.deleteEstimate = function(projectId) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/estimateDelete?projectId=" + projectId,
            async: false
        });
        return request;
    };
});

app.service("estimatesService", function ($http) {
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

    this.showCards = function(projectId) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/estimatesShowCard?projectId=" + projectId,
            async: false
        });
        return request;
    };

    this.isClear = function(projectId) {
        var request = $http({
            method: "GET",
            url: apiPath() + "/api/estimatesIsClear?projectId=" + projectId,
            async: false
        });
        return request;
    };
});