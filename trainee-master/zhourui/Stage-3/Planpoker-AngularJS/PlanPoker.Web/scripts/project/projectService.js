app.service("projectService", function($http) {

    this.createProject = function(Project) {
        var request = $http({
            method: "POST",
            url: "/api/project",
            data: Project
        });
        return request;
    };
    this.deleteProject = function(projectId) {
        var request = $http({
            method: "DELETE",
            url: "/api/project/" + projectId
        });
        return request;
    };
    this.updateProject = function(Project) {
        var request = $http({
            method: "PUT",
            url: "/api/project",
            data: Project
        });
        return request;
    };
    this.getAllProject = function() {
        return $http.get("/api/project");
    };
    this.getProjectById = function(projectId) {
        return $http.get("/api/project/{id}" + projectId);
    };
    this.getProjectByName = function(projectName) {
        return $http.get("/api/project/search") + projectName;
    };
});