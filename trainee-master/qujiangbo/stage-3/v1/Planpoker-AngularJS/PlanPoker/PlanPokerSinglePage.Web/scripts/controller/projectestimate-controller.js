// controller for the estimate page
appModule.controller('estimateController', ['$scope', '$http', 'userLogin', '$location',"$routeParams", function ($scope, $http, userLogin, $location,$routeParams) {
    userLogin.loginCheck();
    var refresult = '';
    var ref = '';
    $scope.project = { Id: '', Name: '', ProjectGuid: '' };
    $scope.estimates = [];

    if ($routeParams.presenter == 'presenter') {
        $("#estimateShow").removeClass('hide');
    }
    var queryAppendPoker = function () {
        $http({
            method: 'Get',
            url: apiPath() + '/api/estimate?projectId=' + getQueryString(Project_Id_Cookie)
        }).success(function (response) {
            if (response) {
                $scope.estimates = response.EstimateViewModel;
                if (response.IsShow) {
                    if ($routeParams.presenter == 'presenter') {
                        $('#projectEstimateBtnShow').removeClass('hide');
                    }
                    showPokerResult();
                }
            }
        })
        .error(function () {
            console.log('error');
        })
        return false;
    };

    var changeIsShow = function () {
        $http({
            method: 'GET',
            url: apiPath() + '/api/estimateShowCard?projectId=' + getQueryString(Project_Id_Cookie)
        }).success(function (response) {
            ShowCard();
        })
        .error(function () {
            console.log('error');
        })
        return false;
    }

    var ShowCard = function () {
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
        if ($routeParams.presenter == 'presenter') {
            $('#nextBtn').removeClass('hide');
        }
    }

    var queryFinished = function () {
        $http({
            method: 'GET',
            url: apiPath() + '/api/estimateIsCleared?projectId=' + getQueryString(Project_Id_Cookie)
        }).success(function (response) {
            if (response) {
                if ($routeParams.presenter == 'presenter') {
                    return;
                } else {
                    $location.path('/dashboard');
                }
            } else {
                ShowCard();
            }
        }).error(function () {
            console.log('error');
        })
        return false;
    }

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
    var showPokerResult = function () {
        $('#projectEstimateBtnShow').hide();
        clearInterval(refresult);
        changeIsShow();
        ref = setInterval(function () {
            $scope.$apply(queryFinished);
        }, 1000);
    }

    $http({
        method: "Get",
        url: apiPath() + "/api/projectguid/" + $routeParams.projectGuid,
    }).success(function (response) {
        $scope.project = response;
        Cookies(Project_Id_Cookie, response.Id, { expires: 1 });
        refresult = setInterval(function () {
            $scope.$apply(queryAppendPoker);
        }, 1000);
    }).error(function () {
    })

    $scope.showPoker = function () {
        $('#projectEstimateBtnShow').hide();
        clearInterval(refresult);
        changeIsShow();
        ref = setInterval(function () {
            $scope.$apply(queryFinished);
        }, 1000);
    }

    $scope.clearAndRedirect = function () {
        $http({
            method: 'GET',
            url: apiPath() + '/api/estimateDelete?projectId=' + getQueryString(Project_Id_Cookie),
        }).success(function () {
            $location.path('/projectEstimate/presenter/' + $routeParams.projectGuid);
        }).error(function () {
            console.log('error');
        })
    }
}])