app.controller("dashboardController", function($scope, $rootScope, $location, $route, $cookies, $cookieStore, globalService, projectService, estimateService) {
    checkCookie($cookieStore, $location);

    globalService.timerCancel();

    $scope.projects = [];
    projectService.getAllProjects().then(function (ret) {
        console.log("Success!");
        angular.forEach(ret.data, function(data) {
            $scope.projects.push(data.Name);
        });
    });
    $scope.Change = function(currentProject) {
        projectService.getProjectByName(currentProject).then(function (ret) {
            $cookieStore.put("Cookie_Project_Id", ret.data.Id);
        });
    };

    $scope.pokers = [
        { Id: '1', Src: '/images/1.jpg' },
        { Id: '2', Src: '/images/2.jpg' },
        { Id: '3', Src: '/images/3.jpg' },
        { Id: '5', Src: '/images/5.jpg' },
        { Id: '8', Src: '/images/8.jpg' },
        { Id: '13', Src: '/images/13.jpg' },
        { Id: '20', Src: '/images/20.jpg' },
        { Id: '40', Src: '/images/40.jpg' },
        { Id: '100', Src: '/images/100.jpg' },
        { Id: 'xs', Src: '/images/xs.jpg' },
        { Id: 's', Src: '/images/s.jpg' },
        { Id: 'm', Src: '/images/m.jpg' },
        { Id: 'l', Src: '/images/l.jpg' },
        { Id: 'yes', Src: '/images/yes.jpg' },
        { Id: 'no', Src: '/images/no.jpg' },
        { Id: 'unknown', Src: '/images/unknown.jpg' },
        { Id: 'java', Src: '/images/java.jpg' }
    ];
    $scope.clickPoker = function(id) {
        $rootScope.timerStop = true;
        $cookieStore.put("Cookie_Poker_Id", id);
        var estimate = {
            ProjectId: $cookieStore.get('Cookie_Project_Id'),
            UserId: $cookieStore.get('Cookie_User_Id'),
            SelectedPoker: $cookieStore.get('Cookie_Poker_Id')
        };
        estimateService.createEstimate(estimate).then(function(ret) {
            console.log("Create Estimate Success!");
            var pId = $cookieStore.get('Cookie_Project_Id');
            var url = "/estimate/false/" + pId + "/";
            console.log(url);
            $location.path(url);
        });
    }
});