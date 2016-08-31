var estimateModule = angular.module('EstimateModule', []);

estimateModule.controller('estimateController', function ($scope, $state, $stateParams, webApiService, cookiesValidation) {
    var calculateAverageHour = function (nums) {
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

    var showCard = function () {

        $scope.flipped = true;

        var nums = [];

        angular.forEach($scope.estimates.EstimateViewModel, function (data) {
            nums.push(data.SelectedPoker);
        });

        $scope.average = calculateAverageHour(nums);

    }
    var queryFinished = function () {
        webApiService.checkEstimateCleared(Cookies(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (data) {
                if ($stateParams.presenter) {
                    return;
                }
                $state.go('dashboard');
            } else {
                showCard();
            }
        })
        .error(function () { });
        return false;
    }

    var queryPoker = function () {
        webApiService.queryPokers(Cookies(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (!data) {
                return;
            }
            $scope.estimates = data;
            if (data.IsShow) {
                if ($stateParams.presenter) {
                }
                $scope.showPokerResult();
            }
        })
        .error(function () {

        });
    };

    var setQueryPokerTimer = function () {
        $scope.timerQueryPoker = setInterval(function () {
            queryPoker();
        }, 3000);
    }

    var clearQueryPoker = function () {
        clearInterval($scope.timerQueryPoker);
    }
    var clearQueryFinished = function () {
        clearInterval($scope.timerQueryFinished);
    }

    var setQueryFinishedTimer = function () {
        $scope.timerQueryFinished = setInterval(function () {
            queryFinished();
        }, 3000);
    }

    var changeIsShow = function () {
        webApiService.changeIsShow(Cookies(EstimatedProject_Id_Cookie))
        .success(function (data) {
        })
        .error(function () {
        });
    }

    $scope.showPokerResult = function () {
        if (!$scope.estimates) {
            return;
        }
        clearQueryPoker();
        changeIsShow();
        setQueryFinishedTimer();
        $scope.estimateShow = false;
        if ($stateParams.presenter) {
            $scope.estimateResultShow = true;
        }
    }



    var deleteEstimate = function (projectId) {
        webApiService.deleteEstimate(Cookies(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (data) {
                if ($stateParams.presenter) {
                    return;
                }
                clearQueryFinished();
                $state.go('dashboard');
            }
        })
        .error(function () { });
    }

    $scope.clearAndRedirect = function () {
        clearQueryFinished();
        deleteEstimate(Cookies(EstimatedProject_Id_Cookie));
        setQueryPokerTimer();
        $scope.estimateResultShow = false;
        if ($stateParams.presenter) {
            $scope.estimateShow = true;
        }
        $scope.estimates = [];
        $scope.flipped = false;
        $scope.average = 0;
    }

    if ($stateParams.presenter) {
        $scope.estimateShow = true;
    } else {
        $scope.estimateShow = false;
        cookiesValidation.ValidateCookies();
    }

    $scope.ProjectName = Cookies(EstimatedProject_Name_Cookie);

    $scope.estimates = [];

    $scope.flipped = false;

    setQueryPokerTimer();
});

estimateModule.directive('estimatepoker', function (webApiService, $cookies, $state) {
    return {
        restrict: 'EA',
        replace: true,
        transclude: true,
        template:
            '<div class="col-xs-4 col-md-2 plan-poker">' +
            '<div class="thumbnail">' +
            '<img ng-src="/app/imgs/{{flipped ? estimate.SelectedPoker:planpoker}}.jpg" id="card" alt="pic">' +
            '<div class="caption user-info" >' +
            '<img style="width: 40px; height: 40px;" ng-src="{{estimate.UserImage?estimate.UserImage:defaultImageUrl}}" alt="pic">' +
            '<label class="pull-right">{{estimate.UserName}}</label>' +
            '</div></div></div>',
        link: function (scope, element, attrs) {
            scope.planpoker = 'planpoker';
            scope.defaultImageUrl = '/app/imgs\\user.png';
        }
    }
});