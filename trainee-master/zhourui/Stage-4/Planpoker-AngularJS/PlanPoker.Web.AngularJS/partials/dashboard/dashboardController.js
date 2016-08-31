app.controller("dashboardController", function($scope, $rootScope, $location, $cookieStore, timerService, projectService, estimateService) {
    checkCookie($cookieStore, $location);

    timerService.cancelTimer();

    $scope.projects = [{}];
    projectService.getAllProjects()
        .success(function(returnObject) { $scope.projects = returnObject; })
        .error(function(errorMessage) { console.log(errorMessage) });

    $scope.changeProject = function() { $cookieStore.put("Cookie_Project_Id", $scope.selectedProjectId); };

    $scope.pokers = [
        { Id: "1", Src: "./images/1.jpg" },
        { Id: "2", Src: "./images/2.jpg" },
        { Id: "3", Src: "./images/3.jpg" },
        { Id: "5", Src: "./images/5.jpg" },
        { Id: "8", Src: "./images/8.jpg" },
        { Id: "13", Src: "./images/13.jpg" },
        { Id: "20", Src: "./images/20.jpg" },
        { Id: "40", Src: "./images/40.jpg" },
        { Id: "100", Src: "./images/100.jpg" },
        { Id: "xs", Src: "./images/xs.jpg" },
        { Id: "s", Src: "./images/s.jpg" },
        { Id: "m", Src: "./images/m.jpg" },
        { Id: "l", Src: "./images/l.jpg" },
        { Id: "yes", Src: "./images/yes.jpg" },
        { Id: "no", Src: "./images/no.jpg" },
        { Id: "unknown", Src: "./images/unknown.jpg" },
        { Id: "java", Src: "./images/java.jpg" }
    ];
    $scope.clickPoker = function(pokerId) { createEstimate($scope, $rootScope, $cookieStore, $location, estimateService, pokerId); };
});