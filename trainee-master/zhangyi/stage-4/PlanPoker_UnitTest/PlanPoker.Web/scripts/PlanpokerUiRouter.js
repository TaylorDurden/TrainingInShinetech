var myApp = angular.module("mainApp", ['ui.router']);

myApp.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.when("", "/main");

    $stateProvider
        .state("main", {
            url: "/main",
            templateUrl: "main.html"
            
        })
        .state("main.login", {
            url: "/login",
            templateUrl: "views/Login.html"
        })
        .state("main.register", {
            url: "/register",
            templateUrl: "views/register.html"
        })
        .state("main.dashboard", {
            url: "/dashboard",
            templateUrl: "views/dashboard.html"
        })
        .state("main.profile", {
            url: "/profile",
            templateUrl: "views/profile.html"
        })
        .state("main.project", {
            url: "/project",
            templateUrl: "views/project.html"
        })
        .state("main.projectEstimate", {
            url: "/projectEstimate",
            templateUrl: "views/projectEstimate.html"
        });
});