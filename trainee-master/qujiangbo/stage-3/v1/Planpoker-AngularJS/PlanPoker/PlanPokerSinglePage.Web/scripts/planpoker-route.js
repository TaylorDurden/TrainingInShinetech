appModule.config(function ($routeProvider) {
    $routeProvider    
    // route for the login page
        .when('/login', {
            templateUrl: 'template/Login.html',
            controller: 'loginController'
        })
    // route for the register page
        .when('/register', {
            templateUrl: 'template/Register.html',
            controller: 'registerController'
        })
    // route for the dashboard page
        .when('/dashboard', {
            templateUrl: 'template/Dashboard.html',
            controller:'dashboardController'
        })

    // route for the estimate page
        .when('/projectEstimate/:projectGuid/', {
            templateUrl: 'template/ProjectEstimate.html',
            controller: 'estimateController'
        })

        .when('/projectEstimate/:presenter/:projectGuid/', {
            templateUrl: 'template/ProjectEstimate.html',
            controller: 'estimateController'
        })

    // route for the profile page
        .when('/profile', {
            templateUrl: 'template/Profile.html',
            controller: 'profileController'
        })

    // route for the project page
        .when('/project', {
            templateUrl: 'template/Project.html',
            controller: 'projectController'
        })

        .otherwise('/login');
});