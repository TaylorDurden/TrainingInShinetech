angular.module("mainApp").controller("dashboardController", ["$rootScope", "$scope", "$http", "checkUserProvider", "projectService", "projectEstimateService", "$cookieStore", function ($rootScope, $scope, $http, checkUserProvider, projectService, projectEstimateService, $cookieStore) {
    checkUserProvider.checkUser();
    $rootScope.isShowNewProject = true;
    $rootScope.isShowSettings = true;
    stopAllPolling();

    $scope.projects = {};

    $scope.estimate = {};

    $scope.project = {
        isExist: false
    };

    $scope.onInitProjects = function () {
        projectService.getProjects().then(function (data) {
            $scope.projects.options = data;
            if ($cookieStore.get(Project_Id_Cookie) != undefined) {
                $scope.selectedId = Number($cookieStore.get(Project_Id_Cookie));
                $scope.selectOption = Number($cookieStore.get(Project_Id_Cookie));
                $scope.onProjectSelectChange();
            }
        });
    };

    $scope.onProjectNameChange = function () {
        projectService.checkProjectNameExist($scope.project.Name).then(function (data) {
            $scope.project.isExist = (data === true);
        });
    }

    $scope.onProjectCreate = function () {
        projectService.createProject($scope.project).then(function () {
            closeModal();
            $scope.onInitProjects();
        });
    }

    $scope.onEstimateCardclick = function (card) {
        $scope.estimate.ProjectId = $cookieStore.get(Project_Id_Cookie);
        $scope.estimate.UserId = $cookieStore.get(User_Id_Cookie);
        $scope.estimate.SelectedPoker = card;
        projectEstimateService.submitEstimate($scope.estimate).then(function () {
            redirectToEstimate($cookieStore.get(Project_GUID_Cookie));
        });
    }

    $scope.onProjectSelectChange = function () {
        projectService.rebindProjectInformation($scope.selectOption).then(function (data) {
            $scope.href = "#/main/projectEstimate?watch=1&presenter=1&projectGuid=" + data;
            $cookieStore.put(Project_GUID_Cookie, data);
            $cookieStore.put(Project_Id_Cookie, $scope.selectOption);
        });
    };
}]);

