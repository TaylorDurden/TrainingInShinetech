app.controller("estimateController", function($scope, $rootScope, $location, $route, $routeParams, $cookies, $cookieStore, projectService, estimateService, $q, $interval) {
    if ($routeParams.presentation === undefined || $routeParams.presentation === "false") {
        checkCookie($cookieStore, $location);
    };

    $scope.projects = [];
    projectService.getAllProjects()
        .success(function(returnObject) { $scope.projects = returnObject; })
        .error(function(errorMessage) { console.log(errorMessage); });

    if ($routeParams.projectId !== undefined) {$scope.selectedProjectId = $routeParams.projectId;}

    $scope.changeProject = function() {
        projectService.getProjectById($scope.selectedProjectId)
            .success(function(returnObject) { $location.path("/estimate/true/" + returnObject.Id + "/"); })
            .error(function(errorMessage) { console.log(errorMessage); });
    };

    $scope.presentationShow =
        $routeParams.presentation !== undefined && $routeParams.presentation === "true";

    $rootScope.isTimerStopped = false;
    if ($routeParams.projectId !== undefined) {
        $scope.estimates = "";
        $rootScope.timer1 = $interval(function() { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService); }, 1000);
        $rootScope.timer1.then(function() { console.log("timer2 done!"); });

        $scope.showEstimatesResult = function() {
            loopEstimateResult($rootScope, $scope, $routeParams, $location, $interval, estimateService);
        };

        $scope.nextProject = function() {
            $scope.presentationShow = true;
            $scope.presentationNext = false;

            $rootScope.isTimerStopped = false;
            estimateService.deleteEstimate($routeParams.projectId);
            $location.path("/estimate/true/");
        };
    }
});

