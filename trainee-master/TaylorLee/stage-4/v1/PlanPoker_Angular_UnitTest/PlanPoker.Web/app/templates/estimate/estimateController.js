var estimateModule = angular.module('EstimateModule', []);

estimateModule.controller('estimateController', function ($scope, $state, $stateParams, $cookieStore, webApiService, cookiesValidation) {
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
    }//

    var showCard = function () {

        $scope.flipped = true;

        var nums = [];

        angular.forEach($scope.estimates.EstimateViewModel, function (data) {
            nums.push(data.SelectedPoker);
        });

        $scope.average = calculateAverageHour(nums);

    }
    var queryFinished = function () {
        webApiService.checkEstimateCleared($cookieStore.get(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (data) {
                if ($stateParams.presenter) {
                    return;
                }
                $state.go('dashboard');
            } else {
                showCard();
            }
        });
        return false;
    }

    var queryPoker = function () {
        webApiService.queryPokers($cookieStore.get(EstimatedProject_Id_Cookie))
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
        });
    };

    var setQueryPokerTimer = function () {
        $scope.timerQueryPoker = setInterval(function () {
            queryPoker();
        }, $scope.interval);
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
        }, $scope.interval);
    }

    var changeIsShow = function () {
        webApiService.changeIsShow($cookieStore.get(EstimatedProject_Id_Cookie));
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

    var deleteEstimate = function () {
        webApiService.deleteEstimate($cookieStore.get(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (data) {
                if ($stateParams.presenter) {
                    return;
                }
                clearQueryFinished();
                $state.go('dashboard');
            }
        });
    }

    $scope.nextEstimate = function () {
        clearQueryFinished();
        deleteEstimate($cookieStore.get(EstimatedProject_Id_Cookie));
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

    $scope.interval = 3000;

    $scope.ProjectName = $cookieStore.get(EstimatedProject_Name_Cookie);

    $scope.estimates = [];

    $scope.flipped = false;

    setQueryPokerTimer();
});
