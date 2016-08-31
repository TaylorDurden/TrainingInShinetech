app.service("projectService", function($http) {
    this.createProject = function (project) {
        var request = $http({
            method: "POST",
            url: apiPath() + "/api/project",
            data: project,
            async: false
        });
        return request;
    };

    this.deleteProject = function (projectId) {
        var request = $http({
            method: "DELETE",
            url: apiPath() + "/api/project/" + projectId,
            async: false
        });
        return request;
    };

    this.updateProject = function (project) {
        var request = $http({
            method: "PUT",
            url: apiPath() + "/api/project",
            data: project,
            async: false
        });
        return request;
    };

    this.getAllProjects = function () { return $http.get(apiPath() + "/api/project"); };

    this.getProjectByName = function (name) {
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