function getQueryString(name) {
    return Cookies(name);
};

var User_Name_Cookie = 'user_name';
var User_Id_Cookie = 'user_id';
var Project_Id_Cookie = 'project_id';
var Presenter_Cookie = 'presenter';

function checkUser() {
    if (Cookies(User_Name_Cookie) == undefined) {
        location.href = "Login.html";
    }
}

function getUrlString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var parameter = window.location.search.substr(1).match(reg);
    var context = "";
    if (parameter != null) {
        context = parameter[2];
    }
    return context == null || context == "" || context == "undefined" ? "" : context;
};

$(document).ready(function () {
    $("body").on("click", "#logout", function () {
        Cookies.remove(User_Name_Cookie);
        Cookies.remove(User_Id_Cookie);
        location.href = "Login.html";
    });
});


   