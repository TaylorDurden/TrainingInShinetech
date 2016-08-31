var refresult = '';
var ref = '';
function appendPoker(data) {
    var parentDiv = $('#planpokerList');
    parentDiv.empty();

    var length = data.EstimateViewModel.length;
    for (var i = 0; i < length; i++) {
        parentDiv.append('<div class="col-xs-4 col-md-2 plan-poker">' +
            '<div class="thumbnail">' +
            '<img src="../image/planpoker.jpg?v=5" id="card" alt="pic">' +
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
        $(this).find('#card').attr('src', '../image/' + imgNum + '.jpg?v=5');
    });
    var average = calculateAverageHour(nums);
    $('#average').text(average);
    $('#estimateResult').removeClass('hide');
    if (getUrlString('presenter')) {
        $('#nextBtn').removeClass('hide');
    }
}

function changeIsShow() {
    $.ajax({
        url: apiPath() + '/api/estimateShowCard?projectId=' + getQueryString(Project_Id_Cookie),
        async: false,
        type: 'GET',
        error: function (error) {
            console.log(error);
        },
        success: function (data) {
        }
    });
}

function showPokerResult() {
    $('#projectEstimateBtnShow').hide();
    clearInterval(refresult);
    changeIsShow();
    ref = setInterval(function () { queryFinished() }, 1000);
}


function queryAppendPoker() {
    $.ajax({
        url: apiPath() + '/api/estimate?projectId=' + getQueryString(Project_Id_Cookie),
        type: 'GET',
        async: false,
        error: function (error) {

        },
        success: function (data) {
            if (data) {
                appendPoker(data);
                if (data.IsShow) {
                    if (getUrlString('presenter')) {
                        $('#projectEstimateBtnShow').removeClass('hide');
                    }
                    showPokerResult();
                }
            }
        }

    });
    return false;
}

function getProjectNameById(projectId) {
    $.ajax({
        url: apiPath() + "/api/project/" + getQueryString(Project_Id_Cookie),
        type: 'GET',
        async: false,
        error: function (error) {
        },
        success: function (data) {
            $('#estimateProjectName').text(data.Name);
        }
    });
}

$(document).ready(function () {
    $('#projectEstimateBtnShow').on('click', function () {
        showPokerResult();
    });

    $('#nextBtn').on('click', function () {
        clearAndRedirect();
    });
    if (getUrlString('presenter')) {
        $("#estimateShow").removeClass('hide');
    }
});

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

function queryFinished() {
    $.ajax({
        url: apiPath() + '/api/estimateIsCleared?projectId=' + getQueryString(Project_Id_Cookie),
        type: 'GET',
        async: false,
        error: function (error) {
            console.log(error);
        },
        success: function (data) {
            if (data) {
                //ShowCard();
                if (getUrlString('presenter')) {
                    return;
                }
                location.href = 'Dashboard.html';
            } else {
                ShowCard();
            }
        }

    });
    return false;
}

function clearAndRedirect() {
    deleteEstimate(getQueryString(Project_Id_Cookie));
}

function deleteEstimate(projectId) {
    $.ajax({
        url: apiPath() + '/api/estimateDelete?projectId=' + projectId,
        type: 'GET',
        async: false,
        error: function (error) {
            console.log('error');
        },
        success: function (data) {
            location.href = location.href;
        }
    });
}

function getProjectId() {
    $.ajax({
        url: apiPath() + "/api/projectid/" + getUrlString("projectGuid"),
        type: "GET",
        async: false,
        success: function (data) {
            Cookies(Project_Id_Cookie, data, { expires: 1 });
            getProjectNameById(data);
            refresult = setInterval(function () { queryAppendPoker() }, 1000);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("error");
        }
    });
}

$(document).ready(function () {
    if (getUrlString('presenter')) {
        $(".navbar-toggle").addClass("hide");
        $(".navbar-right").addClass("hide");
    }
    getProjectId();
});
