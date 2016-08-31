app.controller("indexController", function($scope, $location, $route, $cookieStore, timerService, projectService, estimateService) {
    //$scope.$on("$routeChangeSuccess", function() { showMenu($scope, checkPath($location)); });

    $scope.project = {};
    $scope.createProject = function() {
        projectService.createProject($scope.project)
            .success(function(returnObject) {
                if (returnObject.Message) {
                    $scope.createProjectErrorTips = returnObject.Message;
                    return;
                }

                angular.element("#modalCreateProject").modal("hide");
                $location.path("/dashboard");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };

    $scope.toLogin = function() { logout($location, $cookieStore, timerService, estimateService); };
});