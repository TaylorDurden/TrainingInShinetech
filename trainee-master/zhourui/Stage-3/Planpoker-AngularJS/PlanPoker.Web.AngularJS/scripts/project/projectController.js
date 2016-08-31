app.controller("projectController", function($scope, $location, $cookieStore, projectService) {
    checkCookie($cookieStore, $location);

    $scope.projects = [{}];

    projectService.getAllProjects().then(function(ret) {
        $scope.projects = ret.data;
    });

    $scope.currentProject = {};
    $scope.setCurrentProject = function(project) {
        $scope.currentProject = project;
    };

    $scope.editProject = function() {
        projectService.updateProject($scope.currentProject).then(function() {
            console.log("Update project success.");
            $location.path("/project");
        });
    };

    $scope.deleteProject = function() {
        projectService.deleteProject($scope.currentProject.Id).then(function() {
            console.log("Delete project success.");
            $location.path("/project");
        });
    };
});