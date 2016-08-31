var apiPathService = angular.module('ApiService', []);

apiPathService.factory('webApiService', ['$http', function ($http) {
    var apiPath = 'http://192.168.1.157:8888';

        return {
            login: function(userName, password) {
                return $http({
                    method: 'POST',
                    url: apiPath + '/api/user?username=' + userName + '&password=' + password
                });
            },
            register: function (registerModel) {
                return $http({
                    method: 'POST',
                    data: registerModel,
                    url: apiPath + '/api/user'
                });
            },
            getProjects: function () {
                return $http({
                    method: 'GET',
                    url: apiPath + '/api/project'
                });
            },
            newProject: function(project) {
                return $http({
                    method: 'POST',
                    data:project,
                    url: apiPath + '/api/project'
                });
            },
            checkProjectName: function(projectName) {
                return $http({
                    method: 'GET',
                    url: apiPath + '/api/projectcheck?projectName=' + projectName
                });
            },
            editProject: function(project) {
                return $http({
                    method: 'PUT',
                    data: project,
                    url: apiPath + '/api/project'
                });
            },
            deleteProject: function (id) {
                return $http({
                    method: 'Delete',
                    url: apiPath + '/api/project?id='+id
                });
            },
            newEstimate: function(estimate) {
                return $http({
                    method: 'POST',
                    data: estimate,
                    url: apiPath + '/api/estimate'
                });
            },
            queryPokers: function (projectid) {
                return $http({
                    method: 'GET',
                    url: apiPath + '/api/estimate?projectId=' + projectid
                });
            },
            changeIsShow: function (projectid) {
                return $http({
                    method: 'POST',
                    url: apiPath + '/api/estimateShowCard?projectId=' + projectid
                });
            },
            checkEstimateCleared: function (projectid) {
                return $http({
                    method: 'GET',
                    url: apiPath + '/api/estimateIsCleared?projectId=' + projectid
                });
            },
            deleteEstimate: function(projectId) {
                return $http({
                    method: 'POST',
                    url: apiPath + '/api/estimateDelete?projectId=' + projectId
                });
            },
            getUserById: function (userId) {
                return $http({
                    method: 'GET',
                    url: apiPath + '/api/user/' + userId
                });
            },
            editUser: function (user) {
                return $http({
                    method: 'PUT',
                    data: user,
                    url: apiPath + '/api/user/'
                });
            },
            checkUser: function (userName) {
                return $http({
                    method: 'GET',
                    url: apiPath + '/api/userCheck?userName=' + userName
                });
            },
            validatePassword: function (userName, password) {
                return $http({
                    method: 'POST',
                    url: apiPath + '/api/validatePassword?userName=' + userName + '&password=' + password
                });
            }
        };
    }
]);

myApp.service('cookiesValidation', function ($state, $cookieStore) {
    var userIdCookie = 'user_id';
    this.ValidateCookies = function () {
        if (!$cookieStore.get(userIdCookie)) {
            $state.go('login');
        }
    }
});