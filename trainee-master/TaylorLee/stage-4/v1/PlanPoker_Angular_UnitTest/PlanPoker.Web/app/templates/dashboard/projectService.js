angular.module('ProjectModule', [])
    .factory('webApiService', ['$http', function ($http, webApiService) {
    return {
        getProjects: function () {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/project'
            });
        },
        newProject: function (project) {
            return $http({
                method: 'POST',
                data: project,
                url: webApiService.apiPath + '/api/project'
            });
        },
        checkProjectName: function (projectName) {
            return $http({
                method: 'GET',
                url: webApiService.apiPath + '/api/projectcheck?projectName=' + projectName
            });
        },
        editProject: function (project) {
            return $http({
                method: 'PUT',
                data: project,
                url: webApiService.apiPath + '/api/project'
            });
        },
        deleteProject: function (id) {
            return $http({
                method: 'Delete',
                url: webApiService.apiPath + '/api/project?id=' + id
            });
        },
        convertToCheckItem : function (projectModel) {
            var item = {
                Id: '',
                Name: '',
                ProjectGuid: '',
                Checked: false,
                Closed: false
            };

            item.Id = projectModel.Id;
            item.Name = projectModel.Name;
            item.ProjectGuid = projectModel.ProjectGuid;
            return item;
        }
    };
}]);