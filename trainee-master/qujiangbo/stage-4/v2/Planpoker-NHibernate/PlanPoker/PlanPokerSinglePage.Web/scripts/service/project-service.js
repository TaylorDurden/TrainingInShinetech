appModule.service("projectService", function ($q, $http) {
    this.getAllProjects = function () {
        return $http.get(apiPath + "/api/project").then(function (response) {
            return $q.when(response);
        });
    }

    this.checkProjectNameRepeat = function (name) {
        return $http({
            method: "Get",
            url: apiPath + "/api/projectcheck?projectName=" + name
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.createProject = function (project) {
        return $http({
            method: "POST",
            url: apiPath + "/api/project",
            data: project,
            headers: { 'Content-Type': "application/json" }
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.editProject = function (project) {
        return $http({
            method: "PUT",
            url: apiPath + "/api/project",
            data: project
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.deleteProject = function (projectId) {
        return $http({
            method: "Delete",
            url: apiPath + "/api/project?id=" + projectId
        }).then(function (response) {
            return $q.when(response);
        });
    }
});
