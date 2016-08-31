angular.module("mainApp", ['ui.router']);

angular.module("mainApp").config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.when("", "/main/login");

    $stateProvider
        .state("main", {
            url: "/main",
            templateUrl: "main.html"
            
        })
        .state("main.login", {
            url: "/login",
            templateUrl: "views/login/Login.html"
        })
        .state("main.register", {
            url: "/register",
            templateUrl: "views/register/register.html"
        })
        .state("main.dashboard", {
            url: "/dashboard",
            templateUrl: "views/dashboard/dashboard.html"
        })
        .state("main.profile", {
            url: "/profile",
            templateUrl: "views/profile/profile.html"
        })
        .state("main.project", {
            url: "/project",
            templateUrl: "views/project/project.html"
        })
        .state("main.projectEstimate", {
            url: "/projectEstimate",
            templateUrl: "views/projectEstimate/projectEstimate.html"
        });
});