appModule.controller("projectController", function ($scope,$cookieStore, loginService, projectService) {
    loginService.loginCheck().then(function (response) {
        if ($cookieStore.get("User_Id_Cookie") !== response.data.UserId) {
            $location.path("/login");
        }
    });
    $scope.projects = [];
    $scope.project = {};

    $scope.getAllProjects = function () {
        projectService.getAllProjects().then(function (response) {
            $scope.projects = response.data;
            $scope.pageSize = 5;
            $scope.curPage = 0;
            $scope.pageCount = Math.ceil($scope.projects.length / $scope.pageSize) - 1;
        });
    };

    $scope.createProject = function () {
        projectService.createProject($scope.project).then(function () {
            $scope.getAllProjects();
            $(".createClose")[0].click();
        });
    }

    $scope.changeCreateProjectName = function () {
        projectService.checkProjectNameRepeat($scope.project.Name).then(function (response) {
            if (response.data) {
                $scope.isStatus = response.data;
            }
        });
    }



    $scope.editProject = function (project) {
        $scope.project.Id = project.Id;
        $scope.project.Name = project.Name;
        $scope.project.oldName = project.Name;
        $scope.project.ProjectGuid = project.ProjectGuid;
    }

    $scope.changeEditProjectName = function () {
        projectService.checkProjectNameRepeat($scope.project.Name).then(function (response) {
            if (response.data) {
                $scope.isStatus = response.data;
            }
        });
    }

    $scope.editProjects = function () {
        if ($scope.project.oldName === $scope.project.Name) {
            $(".editClose")[0].click();
            return;
        }
        projectService.editProject($scope.project).then(function() {
            $scope.getAllProjects();
            $(".editClose")[0].click();
        });
    }

    $scope.deleteProject = function (project) {
        $scope.project.Id = project.Id;
    }

    $scope.deleteProjects = function () {
        projectService.deleteProject($scope.project.Id).then(function() {
            $scope.getAllProjects();
        });
    }
})
.filter("pageStartFrom", [function () {
    return function (input, start) {
        start = +start;

        return input.slice(start);
    }
}])