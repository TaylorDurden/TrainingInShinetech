app.controller("developerController", function ($scope, $rootScope, $location, $http, $cookieStore, developerService, userService) {

    checkCookie($cookieStore, $location);

    $scope.serchCondition = "";
    $scope.currentPage = PageInfo_CurrentPage;

    developerService.getDeveloperStatuss().then(function (response) {
        $scope.statuss = response.data;
    });

    userService.getUsers().then(function (response) {
        $scope.users = response.data;
    });

    var serchDeveloper = function (pageIndex) {
        developerService.getDeveloperListViewModelByCondition($scope.serchCondition, pageIndex).then(function (respone) {
            if (respone.data != null) {
                $scope.pages = respone.data.Pages;
                if (respone.data.Pages == null) {
                    $scope.totalPage = 1;
                } else {
                    $scope.totalPage = respone.data.Pages.length;
                }
                if ($scope.currentPage > $scope.totalPage) {
                    $scope.currentPage = $scope.totalPage;
                }
                $scope.list = respone.data.Models;
            }
        });
    }

    var callback = function () {
        $("#myCreateModal").modal("hide");
        $("#myDeleteModal").modal("hide");
        $scope.getData();
    };
    
    $scope.serch = function () {
        $scope.currentPage = 1;
        serchDeveloper(1);
    }

    $scope.createDeveloperModal = function () {
        $scope.developer = {
            DeveloperId: 0,
            FristName: "",
            LastName: "",
            Email: "",
            Status: ""
        };
    }

    $scope.editDeveloperModal = function (developer) {
        $scope.developer = developer;
    }

    $scope.saveDeveloper = function () {
        if ($scope.developer.DeveloperId === 0) {
            developerService.createDeveloper($scope.developer, callback);
        } else {
            developerService.updateDeveloper($scope.developer, callback);
        }
    };

    $scope.deleteDeveloperModal = function (developer) {
        $scope.developerId = developer.DeveloperId;
    }

    $scope.deleteDeveloper = function () {
        developerService.deleteDeveloper($scope.developerId, callback);
    }

    $scope.load = function (pageIndex) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        serchDeveloper(pageIndex);
    };
});