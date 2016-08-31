app.controller("projectController", function($scope, $location, $cookieStore, timerService, projectService) {
    checkCookie($cookieStore, $location);

    timerService.cancelTimer();

    $scope.projects = [{}];

    projectService.getAllProjects()
        .success(function(returnObject) { $scope.projects = returnObject; })
        .error(function(errorMessage) { console.log(errorMessage); });

    $scope.currentProject = {};
    $scope.setCurrentProject = function(project) { $scope.currentProject = project; };

    $scope.editProject = function() {
        projectService.updateProject($scope.currentProject)
            .success(function(returnObject) {
                if (returnObject.Message) {
                    $scope.createProjectErrorTips = returnObject.Message;
                    return;
                }
                $location.path("/project");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };

    $scope.deleteProject = function() {
        projectService.deleteProject($scope.currentProject.Id)
            .success(function() {
                $location.path("/project");
                $scope.projects = [{}];
                projectService.getAllProjects()
                    .success(function(returnObject) { $scope.projects = returnObject; })
                    .error(function(errorMessage) { console.log(errorMessage); });
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };
});