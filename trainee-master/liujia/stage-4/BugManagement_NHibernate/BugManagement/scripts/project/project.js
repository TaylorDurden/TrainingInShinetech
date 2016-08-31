app.controller("projectController", function ($scope, $rootScope, $location, $http, $cookieStore, projectService) {

    checkCookie($cookieStore, $location);

    $scope.serchCondition = "";
    $scope.currentPage = PageInfo_CurrentPage;

    var callback = function () {
        $("#myCreateModal").modal("hide");
        $("#myDeleteModal").modal("hide");
        $scope.getData();
    };
    
    var serchProject = function (pageIndex) {
        projectService.getProjectListViewModelByCondition($scope.serchCondition, pageIndex).then(function (respone) {
            if (respone.data != null) {
                $scope.pages = respone.data.Pages;
                if (respone.data.Pages == null) {
                    $scope.totalPage = 1;
                } else {
                    $scope.totalPage = respone.data.Pages.length;
                }
                $scope.list = respone.data.Models;
            }
        });
    }

    $scope.ProjectNameChange = function () {
        if ($scope.project.ProjectName !== "") {
            projectService.checkProjectNameIsExist($scope.project.ProjectName,$scope.project.ProjectId).then(
                function (response) {
                    $scope.isExist = response.data;
                });
        }
    }

    $scope.serch = function () {
        $scope.currentPage = 1;
        serchProject(1);
    }

    $scope.createProjectModal = function () {
        $scope.project = {
            ProjectId: 0,
            ProjectName: "",
            Description: "",
            MainContact: "",
            ContactEmail: "",
            StartTime: ""
        };
    }

    $scope.editProjectModal = function (project) {
        $scope.project = project;
    }

    $scope.saveProject = function () {
        if ($scope.project.ProjectId === 0) {
            projectService.createProject($scope.project, callback);
        } else {
            projectService.updateProject($scope.project, callback);
        }
    };

    $scope.deleteProjectModal = function (project) {
        $scope.projectId = project.ProjectId;
    }

    $scope.deleteProject = function () {
        projectService.deleteProject($scope.projectId, callback);
    }

    $scope.load = function (pageIndex) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        serchProject(pageIndex);
    };
});