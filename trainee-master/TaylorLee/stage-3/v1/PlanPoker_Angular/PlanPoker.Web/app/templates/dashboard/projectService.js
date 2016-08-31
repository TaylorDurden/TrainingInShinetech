apiPathService.factory('webApiService', ['$http', function($http) {
    return {
        getProject: function () {
            return $http({
                method: 'GET',
                url: apiPath + '/api/project'
            });
        },
        newProject: function (project) {
            return $http({
                method: 'POST',
                data: project,
                url: apiPath + '/api/project'
            });
        },
        checkProjectName: function (projectName) {
            return $http({
                method: 'GET',
                url: apiPath + '/api/projectcheck?projectName=' + projectName
            });
        },
        editProject: function (project) {
            return $http({
                method: 'PUT',
                data: project,
                url: apiPath + '/api/project'
            });
        },
        deleteProject: function (id) {
            return $http({
                method: 'Delete',
                url: apiPath + '/api/project?id=' + id
            });
        }
    };
}]);