app.controller("dashboardController", function ($scope, $location, $route, $cookies, $cookieStore, bugService, projectService, bugTypeService, developerService) {

    checkCookie($cookieStore, $location);

    $scope.whereCondition = '';
    $scope.developers = [];
    $scope.selectDevelopers = [];

    $scope.statuss = bugStatus();

    var bugTypeInit = function () {
        bugTypeService.getAllBugType().then(function (response) {
            $scope.bugTypes = response.data;
        });
    }

    var projectInit = function () {
        projectService.getAllProject().then(function (response) {
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

    bugTypeInit();  
    projectInit();

    var queryDashboard = function () {
        bugService.getDashboard($scope.whereCondition).then(function (response) {
            var dashboardListViewModel = response.data;
            if (dashboardListViewModel != null) {
                $scope.assignedBugs = dashboardListViewModel.AssignedBugList;
                $scope.inProgressBugs = dashboardListViewModel.InProgressBugList;
                $scope.inTestBugs = dashboardListViewModel.InTestBugList;
                $scope.doneBugs = dashboardListViewModel.DoneBugList;
            }
        });
    }

    var callback = function () {
        $("#myCreateModal").modal("hide");
        queryDashboard();
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

    queryDashboard();

    $scope.serch = function () {
        queryDashboard();
    }

    $scope.saveBug = function () {
        var strDevelopers = "";
        if ($scope.selectDevelopers.length > 0) {
            $scope.selectDevelopers.forEach(function (d) {
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

    $scope.editBugModal = function (bug) {
        $scope.bug = bug;
        $scope.selectDevelopers = [];
        developerInit(callSelectDevelopers);
    }

    $scope.changeStatus = function (status) {
        $scope.bug.Status = status.value;
    }

    $scope.selectDeveloper = function (scope) {
        $scope.developers.splice(scope.$index, 1);
        $scope.selectDevelopers.push(scope.x);
    }

    $scope.removeSelectedDeveloper = function (scope) {
        $scope.selectDevelopers.splice(scope.$index, 1);
        $scope.developers.push(scope.x);
    }
});

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    var bugId = $(ev.currentTarget)[0].getAttribute("data-value");
    $("#hdBugId").val(bugId);
    ev.dataTransfer.setData("Text", ev.currentTarget.id);
}

function drop(ev) {
    ev.preventDefault();

    var bugId = $("#hdBugId").val();
    var status = $(ev.currentTarget)[0].getAttribute("data-value");

    var flag = $.getJSON(apiPath() + "/api/UpdateBugStatus?bugId=" + bugId + "&stauts=" + status);

    if (flag) {
        var data = ev.dataTransfer.getData("Text");
        ev.currentTarget.children[1].appendChild(document.getElementById(data));
    }
}