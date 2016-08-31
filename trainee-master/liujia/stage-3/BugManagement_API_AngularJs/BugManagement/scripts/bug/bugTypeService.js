app.service("bugTypeService", function ($http) {
    
    this.getAllBugType = function () {
        return $http.get(apiPath() + "/api/bugType");
    };

});