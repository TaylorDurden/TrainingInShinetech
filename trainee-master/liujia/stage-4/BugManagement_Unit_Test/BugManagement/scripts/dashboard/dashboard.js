app.controller("dashboardController", function ($scope, $location, $route, $cookies, $cookieStore, bugService, projectService, bugTypeService, developerService) {

    checkCookie($cookieStore, $location);

    $scope.serchCondition = "";
    $scope.selectedDevelopers = [];
    $scope.developers = [];

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

    var queryDashboard = function () {
        bugService.getDashboard($scope.serchCondition).then(function (response) {
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

    queryDashboard();

    $scope.serch = function () {
        queryDashboard();
    }

    $scope.saveBug = function () {
        bugService.updateBug($scope.bug, $scope.selectedDevelopers, callback);
    };

    $scope.editBugModal = function (bug) {
        $scope.bug = bug;
        $scope.selectedDevelopers = [];
        developerInit(callSelectDevelopers);
    }

    $scope.changeStatus = function (status) {
        $scope.bug.Status = status.Value;
    }

    $scope.selectDeveloper = function (scope) {
        $scope.developers.splice(scope.$index, 1);
        $scope.selectedDevelopers.push(scope.x);
    }

    $scope.removeSelectedDeveloper = function (scope) {
        $scope.selectedDevelopers.splice(scope.$index, 1);
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