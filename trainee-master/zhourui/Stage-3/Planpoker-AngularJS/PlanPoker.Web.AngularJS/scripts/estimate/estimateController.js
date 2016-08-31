app.controller("estimateController", function ($scope, $rootScope, $location, $route, $routeParams, $cookies, $cookieStore, projectService, estimateService, $q, $interval) {
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
        projectService.getProjectByName($scope.selectedProjectName).then(function(ret) {
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
        $scope.estimates = ""; //[{ ProjectId: '', SelectedPoker: '', UserImage: '', UserName: '', pokerImage: '' }];
        $rootScope.timer1 = $interval(function() { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) }, 1000);
        $rootScope.timer1.then(success);

        function success() {
            console.log("timer1 done!");
        }

        $scope.showCards = function() {
            $scope.presentation = false;
            $scope.presentationNext = true;

            estimateService.showCards($routeParams.projectId);

            $rootScope.timer2 = $interval(function() { loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) }, 1000, 5);
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

var isShow = false;
function loopEstimates($scope, $rootScope, $location, $routeParams, $interval, estimateService) {
    if (!$rootScope.timerStop) {
        estimateService.getPokerViewModels($routeParams.projectId).then(function (ret) {
            console.log("Get Poker ViewModel Success!");
            if (ret.data) {
                $scope.estimates = ret.data.EstimateViewModel;
                isShow = ret.data.IsShow;

                if (!isShow) {
                    angular.forEach($scope.estimates, function (estimate) {
                        estimate.pokerImage = "/images/planpoker.jpg";
                    });
                } else {
                    angular.forEach($scope.estimates, function (estimate) {
                        estimate.pokerImage = "/images/" + estimate.SelectedPoker + ".jpg";
                    });

                    var pokerIds = [];
                    angular.forEach($scope.estimates, function (estimate) {
                        pokerIds.push(estimate.SelectedPoker);
                    });
                    $scope.averageTime = calculateAverageHour(pokerIds);
                    $scope.presentationResult = true;
                }

                $scope.getUserImage = function (estimate) {
                    if (estimate) {
                        return estimate[0].UserImage;
                    };
                };
                $scope.getUserName = function (estimate) {
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