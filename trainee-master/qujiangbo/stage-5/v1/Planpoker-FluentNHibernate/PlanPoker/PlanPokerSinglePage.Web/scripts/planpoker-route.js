appModule.config(function ($routeProvider) {
    $routeProvider    
    
        .when("/login", {
            templateUrl: "template/Login.html",
            controller: "loginController"
        })
    
        .when("/register", {
            templateUrl: "template/Register.html",
            controller: "registerController"
        })
    
        .when("/dashboard", {
            templateUrl: "template/Dashboard.html",
            controller:"dashboardController"
        })
    
        .when("/projectEstimate/:projectGuid/", {
            templateUrl: "template/ProjectEstimate.html",
            controller: "estimateController"
        })

        .when("/projectEstimate/:presenter/:projectGuid/", {
            templateUrl: "template/ProjectEstimate.html",
            controller: "estimateController"
        })
    
        .when("/profile", {
            templateUrl: "template/Profile.html",
            controller: "profileController"
        })
    
        .when("/project", {
            templateUrl: "template/Project.html",
            controller: "projectController"
        })

        .otherwise("/login");
});