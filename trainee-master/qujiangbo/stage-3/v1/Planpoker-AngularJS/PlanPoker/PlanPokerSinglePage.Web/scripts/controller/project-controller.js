// controller for the project page
appModule.controller('projectController', function ($scope, $http, userLogin) {
    userLogin.loginCheck();
    $scope.projects = [];
    $scope.project = { Id: '', Name: '', oldName: '' };

    var bindData = function () {
        $http.get(apiPath() + "/api/project").success(function (response) {
            $scope.projects = response;
            $scope.pageSize = 5;
            $scope.curPage = 0;
            $scope.pageCount = Math.ceil($scope.projects.length / $scope.pageSize) - 1;
        })
    }
    bindData();
    $scope.textChange = function () {
        $http({
            method: "Get",
            url: apiPath() + "/api/projectcheck?projectName=" + $scope.project.Name
        }).success(function (response) {
            if (response) {
                $scope.isStatus = response;
            }
        }).error(function (e) {
            console.log('error');
        });
    }

    $scope.editProject = function (project) {
        $scope.project.Id = project.Id;
        $scope.project.Name = project.Name;
        $scope.project.oldName = project.Name;
    }

    $scope.editProjects = function () {
        if ($scope.project.oldName == $scope.project.Name) {
            $('.close')[0].click();
            return;
        }
        $http({
            method: 'PUT',
            url: apiPath() + "/api/project",
            data: $scope.project
        }).success(function () {
            $('.close')[0].click();
            bindData();
        }).error(function () {
            console.log('error');
        })
    }

    $scope.deleteProject = function (project) {
        $scope.project.Id = project.Id;
    }
    $scope.deleteProjects = function () {
        $http({
            method: 'Delete',
            url: apiPath() + "/api/project?id=" + $scope.project.Id
        }).success(function () {
            console.log('ok');
            bindData();
        }).error(function () {
            console.log('error');
        })
    }
})
.filter('pageStartFrom', [function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    }
}])