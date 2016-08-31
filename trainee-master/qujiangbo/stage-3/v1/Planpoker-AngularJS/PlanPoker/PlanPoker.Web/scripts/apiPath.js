function apiPath() {
    var path = "http://localhost:8888";
    return path;
}

function checkUser() {
    if (Cookies(User_Name_Cookie) == undefined) {
        location.href = "Login.html";
    }
}

function planPokerApp() {
    var planPokerApp = angular.module('plan-poker-app', []);
    return planPokerApp;
}



