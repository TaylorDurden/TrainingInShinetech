angular.module("mainApp").controller('dashboardController', ['$scope', '$http', 'checkUserProvider', 'projectService', 'projectEstimateService', function ($scope, $http, checkUserProvider, projectService, projectEstimateService) {
    checkUserProvider.checkUser();
    $scope.projects = {
        select: '',
        options: {}
    };
    $scope.selectOption = '';
    $scope.selectedId = '';
    $scope.initProjects = function () {
        projectService.initProjectsSelect($scope);
    };

    $scope.onProjectSelectChange = function () {
        projectService.projectSelectChange($scope);
    };

    $scope.project = {
        isExist: false,
        Name: ''
    };
    $scope.onProjectNameChange = function () {
        projectService.projectNameChange($scope);
    }

    $scope.onCreateProject = function () {
        projectService.createProject($scope);
    }

    $scope.estimate = {
        ProjectId: '',
        UserId: '',
        SelectedPoker: ''
    };

    $scope.onEstimateCardclick = function (card) {
        projectEstimateService.estimateCardclick($scope, card);
    }
}]);

