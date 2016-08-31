app.service("userService", function($http) {

    this.createUser = function(user) { return $http.post(apiUrl + "/api/user", user); };

    this.deleteUser = function(userid) { return $http.delete(apiUrl + "/api/user/" + userid); };

    this.updateUser = function(user) { return $http.put(apiUrl + "/api/user", user); };

    this.login = function(username, password) { return $http.get(apiUrl + "/api/user/login?username=" + username + "&password=" + password); };

    this.getAllUser = function() { return $http.get(apiUrl + "/api/user"); };

    this.getUserById = function(userId) { return $http.get(apiUrl + "/api/user/" + userId); };

    this.getUserByName = function(userName) { return $http.get(apiUrl + "/api/user?username=" + userName); };
});


app.service("projectService", function($http) {

    this.createProject = function(project) { return $http.post(apiUrl + "/api/project", project); };

    this.deleteProject = function(projectId) { return $http.delete(apiUrl + "/api/project/" + projectId); };

    this.updateProject = function(project) { return $http.put(apiUrl + "/api/project", project); };

    this.getAllProjects = function() { return $http.get(apiUrl + "/api/project"); };

    this.getProjectById = function(projectId) { return $http.get(apiUrl + "/api/projectid/?id=" + projectId) };

    this.getProjectByName = function(name) { return $http.get(apiUrl + "/api/project?name=" + name); };
});


app.service("estimateService", function($http) {

    this.createEstimate = function(estimate) { return $http.post(apiUrl + "/api/estimates", estimate); };

    this.deleteEstimate = function(projectId) { return $http.delete(apiUrl + "/api/estimatesDelete?projectId=" + projectId); };

    this.getEstimates = function(projectId) { return $http.get(apiUrl + "/api/estimates?projectId=" + projectId); };

    this.showEstimatesResult = function(projectId) { return $http.get(apiUrl + "/api/showEstimatesResult?projectId=" + projectId); };
});