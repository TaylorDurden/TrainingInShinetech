app.service("projectService", function ($http) {
    
    this.createProject = function (projectViewModel,callback) {
        var strurl = apiPath() + "/api/project";
        $http.post(strurl, projectViewModel).success(callback);
    };

    this.deleteProject = function (projectId, callback) {
        $http({
            method: "DELETE",
            url: apiPath() + "/api/project/" + projectId
        }).success(callback);
    };

    this.updateProject = function (projectViewModel, callback) {
        $http({
            method: "PUT",
            url: apiPath() + "/api/project",
            data: projectViewModel
        }).success(callback);
    };

    this.getProjectByCondition = function (condition, pageindex,pageSize) {
        var strurl = apiPath() + "/api/project?condition=" + condition + "&strpage=" + pageindex + "&strpagesize=" + pageSize;
        var result = $http.get(strurl);
        return result;
    };

    this.getprojectById = function (projectId) {
        return $http.get(apiPath() + "/api/project/" + projectId);
    };

    this.getAllProject = function () {
        return $http.get(apiPath() + "/api/allProject");
    };

    this.checkProjectNameIsExist = function(projectName) {
        return $http.get(apiPath() + "/api/project/checkProjectName?projectName=" + projectName);
    };
});
