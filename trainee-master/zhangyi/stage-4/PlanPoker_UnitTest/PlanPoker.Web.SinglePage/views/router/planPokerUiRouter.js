angular.module("mainApp").config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.when("", "/main/login");

    $stateProvider
        .state("main", {
            url: "/main",
            templateUrl: "main.html"

        })
        .state("main.login", {
            url: "/login",
            templateUrl: "views/template/login/Login.html"
        })
        .state("main.register", {
            url: "/register",
            templateUrl: "views/template/register/register.html"
        })
        .state("main.dashboard", {
            url: "/dashboard",
            templateUrl: "views/template/dashboard/dashboard.html"
        })
        .state("main.profile", {
            url: "/profile",
            templateUrl: "views/template/profile/profile.html"
        })
        .state("main.project", {
            url: "/project",
            templateUrl: "views/template/project/project.html"
        })
        .state("main.projectEstimate", {
            url: "/projectEstimate",
            templateUrl: "views/template/projectEstimate/projectEstimate.html"
        });
});