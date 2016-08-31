app.service("developerService", function ($http) {

    this.createDeveloper = function (developerViewModel, callback) {
        var strurl = apiPath() + "/api/developer";
        $http.post(strurl, developerViewModel).success(callback);
    };

    this.deleteDeveloper = function (developerid,callback) {
        $http({
            method: "DELETE",
            url: apiPath() + "/api/developer/" + developerid
        }).success(callback);
    };

    this.updateDeveloper = function (developer,callback) {
        $http({
            method: "PUT",
            url: apiPath() + "/api/developer",
            data: developer
        }).success(callback);
    };

    this.getDeveloperByCondition = function (condition, pageindex, pagesize) {
        var strurl = apiPath() + "/api/developer?condition=" + condition + "&strpage=" + pageindex + "&strpagesize=" + pagesize;
        var result = $http.get(strurl);
        return result;
    };

    this.getDeveloperById = function(developerId, callback) {
        $http.get(apiPath() + "/api/developer/" + developerId).success(callback);
    };

    this.getAllDeveloper = function () {
        return  $http.get(apiPath() + "/api/allDeveloper");
    };

    this.getDeveloperByBugId = function (bugId) {
        return $http.get(apiPath() + "/api/developer?bugId=" + bugId);
    }; 
});
