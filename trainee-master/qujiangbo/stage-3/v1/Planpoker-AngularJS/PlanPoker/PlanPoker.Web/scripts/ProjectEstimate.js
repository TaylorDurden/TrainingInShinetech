planPokerApp().service('userLogin', function () {
    this.loginCheck = function () {
        if (Cookies(User_Name_Cookie) == undefined) {
            location.href = "Login.html";
        }
    }
})
.controller('estimateController', ['$scope', '$http', 'userLogin', function ($scope, $http, userLogin) {
    userLogin.loginCheck();
    var refresult = '';
    var ref = '';
    if (getUrlString('presenter')) {
        $("#estimateShow").removeClass('hide');
        $(".navbar-toggle").addClass("hide");
        $(".navbar-right").addClass("hide");
    }

    $scope.project = { Id: '', Name: '', ProjectGuid: '' };
    $scope.estimates = [];

    var queryAppendPoker = function () {
        $http({
            method: 'Get',
            url: apiPath() + '/api/estimate?projectId=' + getQueryString(Project_Id_Cookie)
        }).success(function (response) {
            if (response) {
                $scope.estimates = response.EstimateViewModel;
                if (response.IsShow) {
                    if (getUrlString('presenter')) {
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
            if (response) {
                if (getUrlString('presenter')) {
                    return;
                }
                location.href = 'Dashboard.html';
            } else {
                ShowCard();
            }
        })
        .error(function () {
            console.log('error');
        })
        return false;
    }

    var ShowCard=function() {
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

    var queryFinished = function () {
        $http({
            method: 'GET',
            url:apiPath() + '/api/estimateIsCleared?projectId=' + getQueryString(Project_Id_Cookie)
        }).success(function (response) {
            if (response) {
                if (getUrlString('presenter')) {
                    return;
                } else {
                    location.href = 'Dashboard.html';
                }                
            } else {
                ShowCard();
            }
        }).error(function () {
            console.log('error');
        })
        return false;
    }

    var calculateAverageHour=function(nums) {
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
        url: apiPath() + "/api/projectguid/" + getUrlString("projectGuid"),
    }).success(function (response) {
        $scope.project = response;
        Cookies(Project_Id_Cookie, response.Id, { expires: 1 });
        refresult=setInterval(function () {
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
            location.href = location.href;
        }).error(function () {
            console.log('error');
        })
    }
}])

