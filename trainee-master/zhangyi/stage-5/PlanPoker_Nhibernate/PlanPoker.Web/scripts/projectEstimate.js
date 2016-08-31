var refresult = '';
var ref = '';
var projectIdEstimate = '';

function appendPoker(data) {
    if (data == "null") {
        return;
    }
    var parentDiv = $('#planpokerList');
    parentDiv.empty();

    var length = data.EstimateViewModel.length;
    for (var i = 0; i < length; i++) {
        parentDiv.append('<div class="col-xs-4 col-md-2 plan-poker">' +
            '<div class="thumbnail">' +
            '<img src="../image/planpoker.jpg?v=1" id="card" alt="pic">' +
            '<div class="caption user-info" >' +
            '<img style="width: 40px; height: 40px;" src="' + (data.EstimateViewModel[i].UserImage == null ? "..\\upload\\user.png" : data.EstimateViewModel[i].UserImage) + '" alt="pic">' +
            '<input type="hidden" id="hour" value="' + data.EstimateViewModel[i].SelectedPoker + '"/>' +
            '<input id="userName" type="hidden" value ="' + data.EstimateViewModel[i].UserName + '"/>' +
            '<label class="pull-right">' + data.EstimateViewModel[i].UserName + '</label>' +
            '</div></div></div>');
    }
}

function ShowCard() {
    var imgsPlanpoker = $('.plan-poker');
    var nums = [];
    imgsPlanpoker.each(function () {
        var imgNum = $(this).find('#hour').val();
        var userNum = $(this).find('#userName').val();
        nums.push(imgNum);
        $(this).find('#card').attr('src', '../image/' + imgNum + '.jpg?v=1');
    });
    var average = calculateAverageHour(nums);
    $('#average').text(average);
    $('#estimateResult').removeClass('hide');
    if (getUrlString('presenter')) {
        $('#nextBtn').removeClass('hide');
    }
}

function changeIsShow(http) {
    http({
        method: "Get",
        url: apiPath() + '/api/estimateShowCard?projectId=' + projectIdEstimate
    }).success(function (data) {
    }).error(function (e) {
    });
}

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

mainApp.controller('estimateController',['$scope', '$http',  function ($scope, $http) {
    $scope.projectName = '';
    $scope.showDiv = Boolean(getUrlString('presenter'));
    $scope.resultDiv = false;
    $scope.resultNext = false;
    $scope.onProjectEstimateInit = function () {
        $http({
            method: "Get",
            url: apiPath() + "/api/projectid/" + getUrlString("projectGuid")
        }).success(function (data) {
            projectIdEstimate = data.Id;
            $scope.projectName = data.Name;
            refresult = setInterval(function () { $scope.queryAppendPoker() }, 1000);
        }).error(function (e) {
        });
    };

    $scope.queryAppendPoker = function () {
        $http({
            method: "Get",
            url: apiPath() + '/api/estimate?projectId=' + projectIdEstimate
        }).success(function (data) {
            if (data) {
                appendPoker(data);
                if (data.IsShow) {
                    if (getUrlString('presenter')) {
                        $scope.showDiv = true;
                    }
                    $scope.showPokerResult($scope, $http);
                }
            }
        }).error(function (e) {
        });
        return false;
    };
    $scope.clickShow = function() {
        $scope.showPokerResult($scope, $http);
    };

    $scope.clickNext = function() {
        $.ajax({
            url: apiPath() + '/api/estimateDelete?projectId=' + projectIdEstimate,
            type: 'GET',
            async: false,
            error: function() {
            },
            success: function(data) {
                refresh();
            }
        });
    };

    $scope.showPokerResult = function () {
        $scope.showDiv = false;
        clearInterval(refresult);
        changeIsShow($http);
        ref = setInterval(function () { $scope.queryFinished() }, 1000);
    };

    $scope.queryFinished = function () {
        $http({
            method: "Get",
            url: apiPath() + '/api/estimateIsCleared?projectId=' + projectIdEstimate
        }).success(function (data) {
            if (data=="true") {
                if (getUrlString('presenter')) {
                    return;
                }
                redirectToDashboard();
            } else {
                ShowCard();
                $scope.resultDiv = true;
                $scope.resultNext= Boolean(getUrlString('presenter'));
            }
        }).error(function () {
        });
        return false;
    };
}]);
