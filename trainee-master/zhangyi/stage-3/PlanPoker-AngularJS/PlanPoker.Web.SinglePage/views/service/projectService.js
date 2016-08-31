angular.module("mainApp").service("projectService", ['$http', function ($http) {

    this.projectNameChange = function ($scope) {
        $http({
            method: "Get",
            url: apiPath() + "/api/projectcheck?projectName=" + $scope.project.Name
        }).success(function (data) {
            $scope.project.isExist = (data === 'true');
        }).error(function (e) {
        });
    }

    this.createProject = function ($scope) {
        $http({
            method: 'POST',
            url: apiPath() + "/api/project",
            data: JSON.stringify($scope.project)
        }).success(function () {
            $('.close')[0].click();
            $scope.initProjects();
        }).error(function () {
        });
    }

    this.initProjectsSelect = function ($scope) {
        $http({
            method: "Get",
            url: apiPath() + "/api/project"
        }).success(function (data) {
            $scope.projects.options = data;
            if (Cookies(Project_Id_Cookie) != undefined) {
                $scope.selectedId = Number(Cookies(Project_Id_Cookie));
                $scope.selectOption = Number(Cookies(Project_Id_Cookie));
                $scope.onProjectSelectChange();
            }
        }).error(function (e) {
        });
    }

    this.projectSelectChange = function ($scope) {
        Cookies.remove(Project_Id_Cookie);
        $http({
            method: "Get",
            url: apiPath() + "/api/getprojecturl/" + $scope.selectOption
        }).success(function (data) {
            $scope.href = data.replace('"', '').replace('"', '');
            Cookies(Project_Id_Cookie, $scope.selectOption, { expires: 1 });
            $http({
                method: "Get",
                url: apiPath() + "/api/getprojecturl/" + $scope.selectOption
            }).success(function (data2) {
                data2 = data2.replace('"', '').replace('"', '');
                $scope.href = '#/main/projectEstimate?watch=1&presenter=1&projectGuid=' + data2;
                Cookies(Project_GUID_Cookie, data2, { expires: 1 });
            }).error(function (e) {
            });
        }).error(function (e) {
        });
    }

    this.deleteProject = function ($scope) {
        $http({
            method: 'Delete',
            url: apiPath() + "/api/project?id=" + $scope.project.Id
        }).success(function () {
            $('.close')[0].click();
            $scope.onInitProjectsTable();
        }).error(function (e) {
            console.log("error");
        });
    }

    this.EditProject = function ($scope) {
        $http({
            method: 'put',
            url: apiPath() + "/api/project",
            data: JSON.stringify($scope.project)
        }).success(function () {
            $('.close')[0].click();
            $scope.onInitProjectsTable();
        }).error(function () {
        });
    }

    this.InitProjectsTable = function ($scope) {
        $http({
            method: "Get",
            url: apiPath() + "/api/project"
        }).success(function (data) {
            $scope.projects = data;
        }).error(function (e) {
        });
    }

    this.SearchProject = function ($scope) {
        $http({
            method: "Get",
            url: apiPath() + "/api/project?name=" + $scope.searchItem
        }).success(function (data) {
            $scope.projects = data;

        }).error(function (e) {
        });
    }

}])