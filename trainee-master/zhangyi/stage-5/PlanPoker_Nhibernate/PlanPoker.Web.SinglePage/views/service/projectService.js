angular.module("mainApp").service("projectService", ["$http", "$q", function ($http, $q) {

    this.checkProjectNameExist = function (name) {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/projectcheck?projectName=" + name
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.createProject = function (project) {
        var delay = $q.defer();
        $http({
            method: "POST",
            url: apiPath + "/api/project",
            data: JSON.stringify(project)
        }).success(function () {
            delay.resolve();
        }).error(function () {
        });
        return delay.promise;
    }

    this.getProjects = function () {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/project"
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.rebindProjectInformation = function (projectId) {
        var delay = $q.defer();
        Cookies.remove(Project_Id_Cookie);
        $http({
            method: "Get",
            url: apiPath + "/api/getprojecturl/" + projectId
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

    this.deleteProject = function (projectId) {
        var delay = $q.defer();
        $http({
            method: "Delete",
            url: apiPath + "/api/project?id=" + projectId
        }).success(function () {
            delay.resolve();
        });
        return delay.promise;
    }

    this.editProject = function (project) {
        var delay = $q.defer();
        $http({
            method: "put",
            url: apiPath + "/api/project",
            data: JSON.stringify(project)
        }).success(function () {
            delay.resolve();
        });
        return delay.promise;
    }

    this.searchProject = function (searchItem) {
        var delay = $q.defer();
        $http({
            method: "Get",
            url: apiPath + "/api/project?name=" + searchItem
        }).success(function (data) {
            delay.resolve(data);
        });
        return delay.promise;
    }

}])