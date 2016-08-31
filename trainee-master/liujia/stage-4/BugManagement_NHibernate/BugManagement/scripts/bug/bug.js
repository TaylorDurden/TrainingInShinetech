app.controller("bugController", function ($scope, $rootScope, $location, $http, $cookieStore, bugService,projectService,bugTypeService,developerService) {

    checkCookie($cookieStore, $location);

    $scope.serchCondition = "";
    $scope.selectedDevelopers = [];
    $scope.developers = [];
    $scope.currentPage = PageInfo_CurrentPage;

    bugTypeService.getBugTypes().then(function (response) {
        $scope.bugTypes = response.data;
    });

    projectService.getProjects().then(function (response) {
        $scope.projects = response.data;
    });

    bugService.getBugStatuss().then(function (response) {
        $scope.statuss = response.data;
    });

    var developerInit = function (callSelectDevelopers) {
        $scope.isLoadingDevelopers = true;
        developerService.getDevelopers().then(function (response) {
            $scope.isLoadingDevelopers = false;
            $scope.developers = response.data;
        }).then(callSelectDevelopers);
    }

    var callback = function () {
        $("#myCreateModal").modal("hide");
        $scope.getData();
    };

    var callSelectDevelopers = function () {
        developerService.getDeveloperByBugId($scope.bug.BugId).then(function (response) {
            if (response.data != null) {
                $scope.selectedDevelopers = response.data;

                if ($scope.selectedDevelopers != null && $scope.selectedDevelopers.length > 0) {
                    $scope.selectedDevelopers.forEach(function (d) {
                        $scope.developers.forEach(function (developer) {
                            if (developer.DeveloperId === d.DeveloperId) {
                                $scope.developers.splice($scope.developers.indexOf(developer), 1);
                            }
                        });
                    });
                }
            }
        });
    }
    
    var serchBug = function (pageIndex) {
        bugService.getBugListViewModelByCondition($scope.serchCondition, pageIndex).then(function (respone) {
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

    $scope.serch = function () {
        $scope.currentPage = 1;
        serchBug(1);
    }

    $scope.createBugModal = function () {
        $scope.bug = {
            BugId: 0,
            Title: "",
            Smmary: "",
            Description: "",
            Type: "",
            ProjectId: "",
            Status: "New",
            Createtime: new Date().toDateString(),
            Creator: $cookieStore.get(Cookie_UserName),
            strDevelopers: "",
            strDocuments: ""
        };
        developerInit();
        $scope.selectedDevelopers = [];
    }

    $scope.editBugModal = function (bug) {
        $scope.bug = bug;
        developerInit(callSelectDevelopers);
    }

    $scope.saveBug = function () {
        if ($scope.bug.BugId === 0) {
            bugService.createBug($scope.bug,$scope.selectedDevelopers,callback);
        } else {
            bugService.updateBug($scope.bug,$scope.selectedDevelopers,callback);
        }
    };
    
    $scope.changeStatus = function (status) {
        $scope.bug.Status = status.Value;
    }

    $scope.selectDeveloper = function(scope) {
        $scope.developers.splice(scope.$index, 1);
        $scope.selectedDevelopers.push(scope.x);
    }

    $scope.removeSelectedDeveloper = function (scope) {
        $scope.selectedDevelopers.splice(scope.$index, 1);
        $scope.developers.push(scope.x);
    }

    $scope.load = function (pageIndex) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        serchBug(pageIndex);
    };
});
