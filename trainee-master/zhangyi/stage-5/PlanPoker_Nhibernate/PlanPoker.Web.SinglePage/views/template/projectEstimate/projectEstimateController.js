var appendPokerPolling = "";
var estimateFinishedPolling = "";
angular.module("mainApp").controller("estimateController", ["$rootScope", "$scope", "$http", "projectEstimateService", function ($rootScope, $scope, $http, projectEstimateService) {
    $rootScope.isShowNewProject = false;
    $rootScope.isShowSettings = false;
    $scope.showDiv = Boolean(getUrlString("presenter"));
    $scope.projectIdEstimate = "";
    $scope.currentProjectGuid = getUrlString("projectGuid");
    $scope.isClickShow = false;
    $scope.resultDiv = false;
    $scope.resultNext = false;

    $scope.onProjectEstimateInit = function () {
        projectEstimateService.initProjectEstimate($scope.currentProjectGuid).then(function (data) {
            $scope.projectIdEstimate = data.Id;
            $scope.projectName = data.Name;
            appendPokerPolling = setInterval(function () { $scope.queryAppendPoker() }, 1000);
        });
    };

    $scope.queryAppendPoker = function () {
        projectEstimateService.queryAppendPoker($scope.projectIdEstimate).then(function (data) {
            if (data) {
                $scope.appendPoker(data);
                if (data.IsShow) {
                    if (getUrlString("presenter")) {
                        $scope.showDiv = true;
                    }
                    $scope.showPokerResult($scope, $http);
                }
            }
        });
    };

    $scope.onShowClick = function () {
        $scope.showPokerResult($scope, $http);
    };

    $scope.onNextClick = function () {
        projectEstimateService.startNewEstimate($scope.projectIdEstimate).then(function () {
            refresh();
        });
    };

    $scope.showPokerResult = function () {
        $scope.isClickShow = true;
        $scope.showDiv = false;
        clearInterval(appendPokerPolling);
        $scope.changeIsShow();
        estimateFinishedPolling = setInterval(function () { $scope.queryFinished() }, 1000);
    };

    $scope.queryFinished = function () {
        if ($scope.isClickShow) {
            projectEstimateService.CheckEstimateIsFinished($scope.projectIdEstimate).then(function (data) {
                if (data === true) {
                    if (getUrlString("presenter")) {
                        return;
                    }
                    $scope.isClickShow = false;
                    redirectToDashboard();
                } else {
                    $scope.ShowCard();
                    $scope.resultDiv = true;
                    $scope.resultNext = Boolean(getUrlString("presenter"));
                }
            });
        }
    };

    $scope.appendPoker = function (data) {
        if (data === null || data === undefined) {
            return;
        }
        var parentDiv = $("#planpokerList");
        parentDiv.empty();

        var length = data.EstimateViewModel.length;
        for (var i = 0; i < length; i++) {
            parentDiv.append('<div class="col-xs-4 col-md-2 plan-poker">' +
                '<div class="thumbnail">' +
                '<img src="image/planpoker.jpg?v=1" id="card" alt="pic">' +
                '<div class="caption user-info" >' +
                '<img style="width: 40px; height: 40px;" src="' + (data.EstimateViewModel[i].UserImage == null ? "..\\upload\\user.png" : data.EstimateViewModel[i].UserImage) + '" alt="pic">' +
                '<input type="hidden" id="hour" value="' + data.EstimateViewModel[i].SelectedPoker + '"/>' +
                '<input id="userName" type="hidden" value ="' + data.EstimateViewModel[i].UserName + '"/>' +
                '<label class="pull-right">' + data.EstimateViewModel[i].UserName + "</label>" +
                "</div></div></div>");
        }
    }

    $scope.ShowCard = function () {
        if ($scope.isClickShow) {
            var imgsPlanpoker = $(".plan-poker");
            var nums = [];
            imgsPlanpoker.each(function () {
                var imgNum = $(this).find("#hour").val();
                nums.push(imgNum);
                $(this).find("#card").attr("src", "image/" + imgNum + ".jpg?v=1");
            });
            var average = $scope.calculateAverageHour(nums);
            $("#average").text(average);
        }
    }

    $scope.changeIsShow = function () {
        $http({
            method: "Get",
            url: apiPath + "/api/estimateShowCard?projectId=" + $scope.projectIdEstimate
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

}]);


