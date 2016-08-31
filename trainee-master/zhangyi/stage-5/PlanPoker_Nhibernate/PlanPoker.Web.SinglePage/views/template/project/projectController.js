angular.module("mainApp").controller("projectController", ["$rootScope", "$scope", "$http", "checkUserProvider", "projectService", function ($rootScope,$scope, $http, checkUserProvider, projectService) {
    checkUserProvider.checkUser();
    $rootScope.isShowNewProject = false;
    $rootScope.isShowSettings = true;
    $scope.projects = {};
    $scope.project = {};
    $scope.project.isExist = false;

    $scope.onProjectDelete = function () {
        projectService.deleteProject($scope.project.Id).then(function () {
            closeModal();
            $scope.onProjectsTableInit();
        });
    }

    $scope.onProjectNameChange = function () {
        projectService.checkProjectNameExist($scope.project.Name).then(function (data) {
            $scope.project.isExist = (data === true);
        });
    }

    $scope.editModal = function (project) {
        $scope.project.Id = project.Id;
        $scope.project.Name = project.Name;
        $scope.project.ProjectGuid = project.ProjectGuid;
    }

    $scope.deleteModal = function (project) {
        $scope.project.Id = project.Id;
    }

    $scope.onProjectEdit = function () {
        projectService.editProject($scope.project).then(function () {
            closeModal();
            $scope.onProjectsTableInit();
        });
    }

    $scope.onProjectsTableInit = function () {
        projectService.getProjects().then(function (data) {
            $scope.projects = data;
        });
    };

    $scope.onProjectSearch = function () {
        projectService.searchProject($scope.searchItem).then(function (data) {
            $scope.projects = data;
        });
    };

}]);