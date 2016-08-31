var myApp = angular.module('myApp', ['ngCookies', 'ngAnimate', 'ui.router', 'ui.bootstrap', 'LoginModule', 'RegisterModule', 'ProjectModule', 'EstimateModule', 'ProfileModule', 'ApiService']);

myApp.run(function ($rootScope, $state, $stateParams) {
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
});

myApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/login');
    $stateProvider
        .state('login', {
            url: '/login',
            views: {
                '': {
                    templateUrl: '/app/templates/login/Login.html'
                }
            }
        })
        .state('register', {
            url: '/register',
            templateUrl: '/app/templates/register/Register.html'
        })
        .state('dashboard', {
            url: '/dashboard',
            templateUrl: '/app/templates/dashboard/Dashboard.html'
        })
        .state('ProjectEstimate', {
            url: '/ProjectEstimate',
            templateUrl: '/app/templates/estimate/ProjectEstimate.html'
        })
        .state('ProjectEstimate.presenter', {
            url: '/presenter/:presenter/:projectid',
            templateUrl: '/app/templates/ProjectEstimate.html',
            controller: function($stateParams) {
                
            }
        });
});
