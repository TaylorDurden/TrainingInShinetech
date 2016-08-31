app.controller("indexController", function ($scope, $location, $route, $cookies, $cookieStore, globalService, userService, projectService, estimateService) {
    $scope.$on("$routeChangeSuccess", function() {
        if ($location.path().indexOf("login") > 0 || $location.path().indexOf("register") > 0) {
            $scope.newProjectMenu = false;
            $scope.dropdownMenu = false;
        }
        if ($location.path().indexOf("dashboard") > 0 || $location.path().indexOf("profile") > 0 || $location.path().indexOf("project") > 0) {
            $scope.newProjectMenu = true;
            $scope.dropdownMenu = true;
        }
        if ($location.path().indexOf("estimate") > 0) {
            if ($location.path().indexOf("true") > 0) {
                $scope.newProjectMenu = false;
                $scope.dropdownMenu = false;
            } else {
                $scope.newProjectMenu = true;
                $scope.dropdownMenu = true;
            }
        }
    });

    $scope.project = { Id: 0, Name: "", ProjectGuid: "" };
    $scope.createProject = function() {
        projectService.createProject($scope.project).then(function() {
            console.log("Create project success!");
            angular.element("#modalCreateProject").modal("hide");
            $location.path('/dashboard');
        }, function () {
            $scope.createProjectErrorTips = "project name already exists!";
        });
    }
    $scope.changeProjectName = function() {
        $scope.project.Name = $scope.projectName;
    };

    $scope.toLogin = function () {
        estimateService.deleteEstimate($cookieStore.get("Cookie_Project_Id"));
        $cookieStore.remove("Cookie_User_Id");
        $cookieStore.remove("Cookie_User_Name");
        $cookieStore.remove("Cookie_Project_Id");
        $cookieStore.remove("Cookie_Poker_Id");

        globalService.timerCancel();

        $location.path("/login");
    };
});

app.controller("loginController", function($scope, $rootScope, $location, $route, $cookies, $cookieStore, userService) {
    $scope.login = function() {
        userService.login($scope.user.username, $scope.user.password).then(
            function() {
                console.log("Login Success!");
                userService.getUserByName($scope.user.username).then(function(ret) {
                    console.log("Get User Info Success");
                    $rootScope.timerStop = false;
                    $cookieStore.put("Cookie_User_Id", ret.data.UserId);
                    $cookieStore.put("Cookie_User_Name", ret.data.UserName);
                    $location.path("/dashboard");
                }, function(err) { console.log(err) });
            }, function () { $scope.signinErrorTips="user name of password wrong!" });
    };
});

app.controller("registerController", function ($scope, $location, $route, $cookies, $cookieStore, userService) {
    $scope.user = {
        userid: 0,
        username: "",
        password: "",
        email: "",
        image: ""
    };

    $scope.user.image = "images/user.png";

    $scope.register = function() {
        var promisePost = userService.createUser($scope.user);
        promisePost.then(function() {
            console.log("Success!");
            $location.path("/login");
        }, function(err) {
            console.log(err);
            $scope.signupErrorTips = err.data;
        });
    }
});

app.controller("profileController", function($scope, $location, $route, $cookies, $cookieStore, userService) {
    checkCookie($cookieStore, $location);

    $scope.user = {
        userid: 0,
        username: "",
        password: "",
        email: "",
        image: ""
    };

    var originalPassword = "";
    
    userService.getUserById($cookieStore.get("Cookie_User_Id")).then(function (ret) {
        console.log("Success!");

        $scope.user.username = ret.data.UserName;
        $scope.user.email = ret.data.Email;
        $scope.user.image = ret.data.Image;

        originalPassword = ret.data.Password;
    });

    $scope.changeImage = function() {
        $scope.user.image = $scope.image;
    };

    $scope.UpdateProfile = function() {
        if ($scope.oldpassword === originalPassword) {
            $scope.user.userid = $cookieStore.get("Cookie_User_Id");
            $scope.user.password = $scope.newpassword;
            $scope.user.image = $scope.image;
            userService.updateUser($scope.user).then(function() {
                console.log("Update profile success!");
                $location.path('/dashboard');
            });
        } else {
            $scope.updateErrorTips = "the old password is wrong, please check it and input again.";
        }
    };
});

app.controller("projectController", function($scope, $location, $route, $cookies, $cookieStore, projectService) {
    checkCookie($cookieStore, $location);

    $scope.projects = [{}];

    projectService.getAllProjects().then(function(ret) {
        $scope.projects = ret.data;
    });

    $scope.currentProject = {};
    $scope.setCurrentProject = function (project) {
        $scope.currentProject = project;
    };

    $scope.editProject = function() {
        projectService.updateProject($scope.currentProject).then(function () {
            console.log("Update project success.");
            $location.path("/project");
        });
    };

    $scope.deleteProject = function() {
        projectService.deleteProject($scope.currentProject.Id).then(function () {
            console.log("Delete project success.");
            $location.path("/project");
        });
    };
});

app.controller("dashboardController", function($scope, $rootScope, $location, $route, $cookies, $cookieStore, globalService, projectService, dashboardService, estimateService) {
    checkCookie($cookieStore, $location);

    globalService.timerCancel();

    $scope.projects = [];
    projectService.getAllProjects().then(function(ret) {
        console.log("Success!");
        angular.forEach(ret.data, function(data) {
            $scope.projects.push(data.Name);
        });
    });
    $scope.Change = function(currentProject) {
        dashboardService.getProjectByName(currentProject).then(function(ret) {
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
    $scope.clickPoker = function (id) {
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

app.controller("estimateController", function($scope, $rootScope, $location, $route, $routeParams, $cookies, $cookieStore, userService, projectService, dashboardService, estimateService, estimatesService, $q, $interval) {
    if ($routeParams.presentation === undefined || $routeParams.presentation === "false") {
        checkCookie($cookieStore, $location);
    }

    $scope.projectNames = [];
    projectService.getAllProjects().then(function(ret) {
        angular.forEach(ret.data, function(project) {
            $scope.projectNames.push(project.Name);
        });
    });

    $scope.changeProject = function() {
        console.log($scope.projectName);
        dashboardService.getProjectByName($scope.selectedProjectName).then(function(ret) {
            $location.path("/estimate/true/" + ret.data.Id + "/");
        });
    };

    if ($routeParams.presentation !== undefined && $routeParams.presentation === "true") {
        $scope.presentation = true;
    } else {
        $scope.presentation = false;
    }

    $rootScope.timerStop = false;
    if ($routeParams.projectId !== undefined) {
        $scope.estimates = "";//[{ ProjectId: '', SelectedPoker: '', UserImage: '', UserName: '', pokerImage: '' }];
        $rootScope.timer1 = $interval(function () { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimatesService) }, 1000);
        $rootScope.timer1.then(success);

        function success() {
            console.log("timer1 done!");
        }

        $scope.showCards = function() {
            $scope.presentation = false;
            $scope.presentationNext = true;

            estimatesService.showCards($routeParams.projectId);

            $rootScope.timer2 = $interval(function () { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimatesService) }, 1000, 5);
            $rootScope.timer2.then(success);

            function success() { console.log("timer2 done!"); }

            var pokerIds = [];
            angular.forEach($scope.estimates, function(estimate) {
                pokerIds.push(estimate.SelectedPoker);
            });

            $scope.averageTime = calculateAverageHour(pokerIds);
            $scope.presentationResult = true;
        };

        $scope.nextProject = function() {
            $scope.presentation = true;
            $scope.presentationNext = false;

            $rootScope.timerStop = false;
            estimateService.deleteEstimate($routeParams.projectId);
            $location.path("/estimate/true/");
        }
    }
});

function checkCookie($cookieStore, $location) {
    if (!$cookieStore.get('Cookie_User_Id')) {
        $location.path("/login");
    }
};

function calculateAverageHour(nums) {
    var length = nums.length;
    var sum = 0;
    var nanNum = 0;

    for (var i = 0; i < length; i++) {
        if (isNaN(nums[i])) {
            nanNum--;
            continue;
        }
        sum += Number(nums[i]);
    }

    var average = sum / (length + nanNum);
    return isNaN(Math.round(average)) ? 0 : Math.round(average);
}

var isShow = false;

function loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimatesService) {
    if (!$rootScope.timerStop) {
        estimatesService.getPokerViewModels($routeParams.projectId).then(function(ret) {
            console.log("Get Poker ViewModel Success!");
            if (ret.data) {
                $scope.estimates = ret.data.EstimateViewModel;
                isShow = ret.data.IsShow;

                if (!isShow) {
                    angular.forEach($scope.estimates, function(estimate) {
                        estimate.pokerImage = "/images/planpoker.jpg";
                    });
                } else {
                    angular.forEach($scope.estimates, function(estimate) {
                        estimate.pokerImage = "/images/" + estimate.SelectedPoker + ".jpg";
                    });

                    var pokerIds = [];
                    angular.forEach($scope.estimates, function (estimate) {
                        pokerIds.push(estimate.SelectedPoker);
                    });
                    $scope.averageTime = calculateAverageHour(pokerIds);
                    $scope.presentationResult = true;
                }

                $scope.getUserImage = function(estimate) {
                    if (estimate) {
                        return estimate[0].UserImage;
                    };
                };
                $scope.getUserName = function(estimate) {
                    if (estimate) {
                        return estimate[0].UserName;
                    };
                };
            } else {
                if (!$scope.presentation) {
                    $location.path("/dashboard");
                }
            }
        });
    } else {
        $interval.cancel($rootScope.timer1);
        $interval.cancel($rootScope.timer2);
    }
};