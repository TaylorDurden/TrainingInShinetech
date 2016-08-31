app.controller("projectController", function($scope, $location, $cookieStore, projectService) {
    checkCookie($cookieStore, $location);

    $scope.projects = [{}];

    projectService.getAllProjects()
        .success(function(returnObject) {
            $scope.projects = returnObject;
            console.log("Get all projects success!");
        })
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
                console.log("Update project success!");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };

    $scope.deleteProject = function() {
        projectService.deleteProject($scope.currentProject.Id)
            .success(function() {
                $location.path("/project");
                console.log("Delete project success!");
                $scope.projects = [{}];
                projectService.getAllProjects()
                    .success(function(returnObject) {
                        $scope.projects = returnObject;
                        console.log("Get all projects success!");
                    })
                    .error(function(errorMessage) { console.log(errorMessage); });
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };
});