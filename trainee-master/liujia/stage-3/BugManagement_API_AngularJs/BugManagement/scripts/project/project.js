app.controller("projectController", function ($scope, $rootScope, $location, $http, $cookieStore, projectService) {

    checkCookie($cookieStore, $location);

    $scope.whereCondition = '';

    $scope.currentPage = PageInfo_CurrentPage;
    $scope.pageSize = PageInfo_PageSize;
    $scope.totalPage = PageInfo_TotalPage;
    $scope.pages = PageInfo_Pages;

    var callback = function () {
        $("#myCreateModal").modal("hide");
        $("#myDeleteModal").modal("hide");
        $scope.getData();
    };
    
    $scope.ProjectNameChange = function () {
        if ($scope.project.ProjectName !== "" && $scope.HdProjectName !== $scope.project.ProjectName) {
            projectService.checkProjectNameIsExist($scope.project.ProjectName).then(
                function (response) {
                    $scope.isExist = response.data;
                });
        }
    }

    $scope.serch = function () {
        $scope.getData(1);
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
        $scope.HdProjectName = "";
    }

    $scope.editProjectModal = function (project) {
        $scope.project = project;
        $scope.HdProjectName = $scope.project.ProjectName;
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

    $scope.load = function (condition, pageIndex, pageSize, callGetPageBack) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        projectService.getProjectByCondition(condition, pageIndex, pageSize).then(function (respone) {
            var totalPage = Math.ceil(respone.data.ModelCount / pageSize);
            callGetPageBack && callGetPageBack(respone.data.ModelList, totalPage);
        });
    };
});