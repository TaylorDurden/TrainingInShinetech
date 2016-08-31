app.controller("bugController", function ($scope, $rootScope, $location, $http, $cookieStore, bugService,projectService,bugTypeService,developerService) {

    checkCookie($cookieStore, $location);

    $scope.whereCondition = '';
    $scope.selectDevelopers = [];
    $scope.developers = [];

    $scope.currentPage = PageInfo_CurrentPage;
    $scope.pageSize = PageInfo_PageSize;
    $scope.totalPage = PageInfo_TotalPage;
    $scope.pages = PageInfo_Pages;

    var bugTypeInit = function () {
        bugTypeService.getAllBugType().then(function (response) {
            $scope.bugTypes = response.data;
        });
    }
    var projectInit = function () {
        projectService.getAllProject().then(function(response) {
            $scope.projects = response.data;
        });
    }
    var developerInit = function (callSelectDevelopers) {
        $scope.isLoadingDevelopers = true;
        developerService.getAllDeveloper().then(function (response) {
            $scope.isLoadingDevelopers = false;
            $scope.developers = response.data;
        }).then(callSelectDevelopers);
    }

    $scope.statuss = bugStatus();
    bugTypeInit();
    projectInit();
    
    var callback = function () {
        $("#myCreateModal").modal("hide");
        $scope.getData();
    };

    var callSelectDevelopers = function () {
        developerService.getDeveloperByBugId($scope.bug.BugId).then(function (response) {
            if (response.data != null) {
                $scope.selectDevelopers = response.data;

                if ($scope.selectDevelopers != null && $scope.selectDevelopers.length > 0) {
                    $scope.selectDevelopers.forEach(function (d) {

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
    
    $scope.serch = function () {
        $scope.getData(1);
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
        $scope.selectDevelopers = [];
    }

    $scope.editBugModal = function (bug) {
        $scope.bug = bug;

        developerInit(callSelectDevelopers);
    }

    $scope.saveBug = function () {
        var strDevelopers = "";
        if ($scope.selectDevelopers.length > 0) {
            $scope.selectDevelopers.forEach(function(d) {
                strDevelopers += d.DeveloperId.toString() + ",";
            });
            strDevelopers = strDevelopers.substr(0, strDevelopers.length - 1);
        }

        $scope.bug.strDevelopers = strDevelopers;
        if ($scope.bug.BugId === 0) {
            bugService.createBug($scope.bug, callback);
        } else {
            bugService.updateBug($scope.bug, callback);
        }
    };
    
    $scope.changeStatus = function (status) {
        $scope.bug.Status = status.value;
    }

    $scope.selectDeveloper = function(scope) {
        $scope.developers.splice(scope.$index, 1);
        $scope.selectDevelopers.push(scope.x);
    }

    $scope.removeSelectedDeveloper = function (scope) {
        $scope.selectDevelopers.splice(scope.$index, 1);
        $scope.developers.push(scope.x);
    }

    $scope.load = function (condition, pageIndex, pageSize, callGetPageBack) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        bugService.getBugByCondition(condition, pageIndex, pageSize).then(function (respone) {
            var totalPage = Math.ceil(respone.data.ModelCount / pageSize);
            callGetPageBack && callGetPageBack(respone.data.ModelList, totalPage);
        });
    };
});
