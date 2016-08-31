var User_Name_Cookie = "user_name";
var User_Id_Cookie = "user_id";
var Project_Id_Cookie = "project_id";
var Presenter_Cookie = "presenter";
var Project_GUID_Cookie = "project_guid";

function getUrlString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var parameter = window.location.href.match(reg);
    var context = "";
    if (parameter != null) {
        context = parameter[2];
    }
    return context == null || context === "" || context === "undefined" ? "" : context;
};

function refresh() {
    history.go(0);
}

function redirectToDashboard() {
    location.href = "#/main/dashboard";
}

function redirectToLogin() {
    location.href = "#/main/login";
}

function redirectToEstimate(guid) {
    location.href = "#/main/projectEstimate?watch=1&projectGuid=" + guid;
}

function redirectToRegister() {
    location.href = "#/main/register";
}

function stopAllPolling() {
    clearInterval(appendPokerPolling);
    clearInterval(estimateFinishedPolling);
}

function closeModal() {
    $(".close")[0].click();
}