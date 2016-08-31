app.service("bugService", function ($http) {

    this.createBug = function (bugViewModel,developerViewModels,callback) {
        $http({
            method: "POST",
            url: apiPath() + "/api/bug",
            data: { BugViewModel: bugViewModel, DeveloperViewModels: developerViewModels }
        }).success(callback);
    };

    this.deleteBug = function (bugId, callback) {
        $http({
            method: "DELETE",
            url: apiPath() + "/api/bug/" + bugId
        }).success(callback);
    };

    this.updateBug = function (bugViewModel,developerViewModels, callback) {
        $http({
            method: "PUT",
            url: apiPath() + "/api/bug",
            data: { BugViewModel: bugViewModel, DeveloperViewModels: developerViewModels }
        }).success(callback);
    };

    this.getBugListViewModelByCondition = function (condition, pageindex) {
        var strurl = apiPath() + "/api/bug?condition=" + condition + "&strpage=" + pageindex;
        var result = $http.get(strurl);
        return result;
    };

    this.getBugById = function (bugId) {
        return $http.get(apiPath() + "/api/bug/" + bugId);
    };

    this.getDashboard = function (condition) {
        return $http.get(apiPath() + "/api/getDashboard?condition=" + condition);
    };

    this.getBugStatuss = function () {
        return $http.get(apiPath() + "/api/bug/getBugStatuss");
    };
});
