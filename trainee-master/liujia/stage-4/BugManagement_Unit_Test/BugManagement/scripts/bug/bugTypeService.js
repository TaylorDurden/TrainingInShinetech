app.service("bugTypeService", function ($http) {
    
    this.getBugTypes = function () {
        return $http.get(apiPath() + "/api/bugType");
    };

});