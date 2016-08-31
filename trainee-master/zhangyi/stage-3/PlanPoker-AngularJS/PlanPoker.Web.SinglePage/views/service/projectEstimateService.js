angular.module("mainApp").service("projectEstimateService", ['$http', function ($http) {

    this.estimateCardclick= function($scope,card) {
        if (Cookies(Project_Id_Cookie)) {
            $scope.estimate.ProjectId = Cookies(Project_Id_Cookie);
            $scope.estimate.UserId = Cookies(User_Id_Cookie);
            $scope.estimate.SelectedPoker = card;
            $http({
                method: 'POST',
                url: apiPath() + "/api/estimate",
                data: JSON.stringify($scope.estimate)
            }).success(function () {
                redirectToEstimate();
            }).error(function () {
            });
        }
    }

}])