mainApp.controller('projectListController', ['$scope', '$http', 'checkUserProvider', 'projectService', function ($scope, $http, checkUserProvider, projectService) {
    checkUserProvider.checkUser();
    $scope.projectList = {
        select: '',
        options: {}
    };
    $scope.selectOption = '';
    $scope.selectedId = '';
    $scope.initProjectList = function () {
        projectService.initProjectListSelect($scope);
    };

    $scope.onProjectSelectChange = function () {
        projectService.projectSelectChange($scope);
    };
}]);

mainApp.controller('addProjectController', ['$scope', '$http', 'projectService', function ($scope, $http, projectService) {
    $scope.project = {
        isExist: false,
        Name: ''
    };
    $scope.onProjectNameChange = function () {
        projectService.projectNameChange($scope);
    }

    $scope.onProjectCreate = function () {
        projectService.createProject($scope);
    }
}]);

mainApp.controller('CardsController', ['$scope', '$http', 'projectEstimateService', function ($scope, $http, projectEstimateService) {
    $scope.estimate = {
        ProjectId: '',
        UserId: '',
        SelectedPoker: ''
    };

    $scope.onEstimateCardclick = function (card) {
        projectEstimateService.estimateCardclick($scope, card);
    }
}]);
