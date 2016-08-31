angular.module("mainApp").controller('projectController', ['$scope', '$http', 'checkUserProvider', 'projectService', function ($scope, $http, checkUserProvider, projectService) {
    checkUserProvider.checkUser();
    $scope.projects = {
    };

    $scope.searchItem = '';

    $scope.project = {
        isExist: false,
        Name: '',
        Id: ''
    };

    $scope.onDeleteProject = function () {
        projectService.deleteProject($scope);
    }

    $scope.onProjectNameChange = function () {
        projectService.projectNameChange($scope);
    }

    $scope.editModal = function (project) {
        $scope.project.Id = project.Id;
        $scope.project.Name = project.Name;
    }

    $scope.deleteModal = function (project) {
        $scope.project.Id = project.Id;
    }

    $scope.onEditProject = function () {
        projectService.EditProject($scope);
    }

    $scope.onInitProjectsTable = function () {
        projectService.InitProjectsTable($scope);
    };

    $scope.onSearchProject = function () {
        projectService.SearchProject($scope);
    };

}]);