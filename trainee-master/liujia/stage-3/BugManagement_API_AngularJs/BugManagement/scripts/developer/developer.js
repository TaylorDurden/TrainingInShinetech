app.controller("developerController", function ($scope, $rootScope, $location, $http, $cookieStore, developerService, userService) {

    checkCookie($cookieStore, $location);

    $scope.whereCondition = "";

    $scope.currentPage = PageInfo_CurrentPage;
    $scope.pageSize = PageInfo_PageSize;
    $scope.totalPage = PageInfo_TotalPage;
    $scope.pages = PageInfo_Pages;

    $scope.statuss = developerStatus();
    var userinit = function () {
        userService.getAllUser().then(function (response) {
            $scope.users = response.data;
        });
    }
    userinit();

    var callback = function () {
        $("#myCreateModal").modal("hide");
        $("#myDeleteModal").modal("hide");
        $scope.getData();
    };
    
    $scope.serch = function () {
        $scope.getData(1);
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

    $scope.load = function (condition, pageIndex, pageSize, callGetPageBack) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        developerService.getDeveloperByCondition(condition, pageIndex, pageSize).then(function (respone) {
            var totalPage = Math.ceil(respone.data.ModelCount / pageSize);
            callGetPageBack && callGetPageBack(respone.data.ModelList, totalPage);
        });
    };
});