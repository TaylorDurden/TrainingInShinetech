appModule.controller("dashboardController", function ($scope, loginService, $location, $cookieStore, dashboardService, projectService) {
    loginService.loginCheck().then(function (response) {
        if ($cookieStore.get("User_Id_Cookie") !== response.data.UserId) {
            $location.path("/login");
        }
    });

    dashboardService.getAllProjects().then(function (response) {
        $scope.projectId = $cookieStore.get("Project_Id_Cookie");
        $scope.projects = response.data;
    });
    
    $scope.projectsName = { };
    $scope.changeProject = function () {
        $cookieStore.put("Project_Id_Cookie", $("#projectName").val());
        dashboardService.changeProject($cookieStore.get("Project_Id_Cookie")).then(function (response) {
            $("#projectGuid").attr("href", response.data);
        }, function () {
            $("#projectGuid").attr("href", "#");            
        });
    }

    $scope.selectPokerCard = function (selectedPoker) {
        $scope.estimate = {};
        $scope.estimate.ProjectId = $cookieStore.get("Project_Id_Cookie");
        $scope.estimate.UserId = $cookieStore.get("User_Id_Cookie");
        $scope.estimate.SelectedPoker = selectedPoker;
        $scope.selectGuid = $("#projectName option:selected").attr("data-guid");

        if ($scope.estimate.SelectedPoker === "") {
            return false;
        }
        if ($scope.estimate.ProjectId === "-1" || $scope.estimate.ProjectId == null || $scope.estimate.ProjectId == undefined || $scope.estimate.ProjectId === "" || $scope.selectGuid === "") {
            return false;
        }

        dashboardService.createEstimate($scope.estimate).then(function () {
            $cookieStore.put("Project_Id_Cookie", $scope.estimate.ProjectId);            
            $location.path("/projectEstimate/" + $scope.selectGuid);
        });

        return false;
    }
})