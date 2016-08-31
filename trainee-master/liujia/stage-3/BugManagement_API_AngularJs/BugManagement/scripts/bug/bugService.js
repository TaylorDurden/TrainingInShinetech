app.service("bugService", function ($http) {

    this.createBug = function (bugViewModel, callback) {
        var strurl = apiPath() + "/api/bug";
        $http.post(strurl, bugViewModel).success(callback);
    };

    this.deleteBug = function (bugId, callback) {
        $http({
            method: "DELETE",
            url: apiPath() + "/api/bug/" + bugId
        }).success(callback);
    };

    this.updateBug = function (bugViewModel, callback) {
        $http({
            method: "PUT",
            url: apiPath() + "/api/bug",
            data: bugViewModel
        }).success(callback);
    };

    this.getBugByCondition = function (condition, pageindex, pageSize) {
        var strurl = apiPath() + "/api/bug?condition=" + condition + "&strpage=" + pageindex + "&strpagesize=" + pageSize;
        var result = $http.get(strurl);
        return result;
    };

    this.getBugById = function (bugId) {
        return $http.get(apiPath() + "/api/bug/" + bugId);
    };

    this.getDashboard = function (condition) {
        return $http.get(apiPath() + "/api/getDashboard?condition=" + condition);
    };
});
