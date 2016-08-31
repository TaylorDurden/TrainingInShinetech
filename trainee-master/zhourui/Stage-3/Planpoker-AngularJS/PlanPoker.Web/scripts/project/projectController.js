app.controller("projectController", function($scope, projectService) {

    $scope.clearControls = function() {
        $scope.OperType = 1;

        $scope.projectId = "";
        $scope.projectName = "";
        $scope.projectGuid = "";
    };
    $scope.create = function() {
        var Project = {
            ProjectId: 0,
            ProjectName: $scope.projectName,
            ProjectGuid: $scope.projectGuid
        };
        var promisePost = projectService.createProject(Project);
        promisePost.then(function(ret) {
            $scope.projectName = ret.data.ProjectName;
            $scope.clear();
        }, function(err) {
            console.log(err);
        });
    };
    $scope.delete = function(Project) {
        var promiseDelete = projectService.deleteUser(Project.projectId);
        promiseDelete.then(function(ret) {
            $scope.Message = "Project Deleted Successfuly";
            clearControls();
        }, function(err) {
            console.log(err);
        });
    };
    $scope.update = function() {
        var Project = {
            ProjectId: getProjectByName($scope.projectName).projectId,
            ProjectName: $scope.projectName,
            ProjectGuid: $scope.projectGuid
        };
        var promisePost = projectService.updateProject(Project);
        promisePost.then(function(ret) {
            $scope.projectName = ret.data.projectName;
            clearControls();
        }, function(err) {
            console.log(err);
        });
    };
    $scope.getByProjectId = function(projectId) {
        var promiseGetSingle = projectService.getProjectById(projectId);

        promiseGetSingle.then(function(ret) {
                var prj = ret.data;

                $scope.projectId = prj.ProjectId;
                $scope.projectName = prj.ProjectName;
                $scope.projectGuid = prj.ProjectGuid;
            },
            function(err) {
                console.log(err);
            });
    };
    $scope.getByProjectname = function(projectName) {
        var promiseGetSingle = projectService.getProjectByName(projectName);

        promiseGetSingle.then(function(ret) {
                var prj = ret.data;

                $scope.projectId = prj.ProjectId;
                $scope.projectName = prj.ProjectName;
                $scope.projectGuid = prj.ProjectGuid;
            },
            function(err) {
                console.log(err);
            });
    };
});