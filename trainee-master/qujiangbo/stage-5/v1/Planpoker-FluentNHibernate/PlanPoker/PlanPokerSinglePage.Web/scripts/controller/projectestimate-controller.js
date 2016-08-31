appModule.controller("estimateController", ["$scope", "loginService", "$location", "$routeParams", "$cookieStore", "estimateService", function ($scope, loginService, $location, $routeParams, $cookieStore,estimateService) {
    loginService.loginCheck().then(function (response) {
        if ($cookieStore.get("User_Id_Cookie") !== response.data.UserId) {
            $location.path("/login");
        }
    });
    $scope.refStart = "";
    $scope.refresult = "";
    $scope.project = { };
    $scope.estimates = [];

    if ($routeParams.presenter === "presenter") {
        $scope.isEstimateShow = true;
    }

    estimateService.getProjectByguid($routeParams.projectGuid).then(function (response) {
        $scope.project = response.data;
        $cookieStore.put("Project_Id_Cookie", response.data.Id);
        $scope.refresult = setInterval(function () {
            $scope.$apply($scope.queryAppendPoker());
        }, 1000);
    });

    $scope.showPoker = function () {
        $scope.isEstimateShow = false;
        clearInterval($scope.refresult);
        $scope.changePokerIsShow();
        $scope.refStart = setInterval(function () {
            $scope.$apply($scope.queryFinished());
        }, 1000);
    }

    $scope.clearEstimateAndRedirect = function () {
        estimateService.clearEstimateAndRedirect($cookieStore.get("Project_Id_Cookie")).then(function () {
            $location.path("/projectEstimate/presenter/" + $routeParams.projectGuid);
        });
    }

    $scope.queryAppendPoker = function () {
        estimateService.queryAppendPoker($cookieStore.get("Project_Id_Cookie")).then(function (response) {
            if (response.data) {
                $scope.estimates = response.data.EstimateViewModel;
                if (response.data.IsShow) {
                    if ($routeParams.presenter === "presenter") {
                        $scope.isEstimateShow = true;
                    }
                    $scope.showPoker();
                }
            }
        });
    };

    $scope.changePokerIsShow = function () {
        estimateService.changePokerIsShow($cookieStore.get("Project_Id_Cookie")).then(function () {
            $scope.showPlanPokerCard();
        });
    }

    $scope.showPlanPokerCard = function () {
        var imgsPlanpoker = $(".plan-poker");
        var nums = [];
        imgsPlanpoker.each(function () {
            var imgNum = $(this).find("#hour").val();
            nums.push(imgNum);
            $(this).find("#card").attr("src", "../image/" + imgNum + ".jpg?v=1");
        });
        var average = $scope.calculateAverageHour(nums);
        $("#average").text(average);
        $scope.isEstimateResultShow = true;
        if ($routeParams.presenter === "presenter") {
            $scope.isNextBtnShow = true;
        }
    }

    $scope.queryFinished = function () {
        estimateService.queryFinished($cookieStore.get("Project_Id_Cookie")).then(function (response) {
            if (response.data) {
                if ($routeParams.presenter === "presenter") {
                    return;
                } else {
                    $location.path("/dashboard");
                }
            } else {
                $scope.showPlanPokerCard();
            }
        });
    }

    $scope.calculateAverageHour = function (nums) {
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
}])