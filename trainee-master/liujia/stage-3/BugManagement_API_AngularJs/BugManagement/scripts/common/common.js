var Cookie_UserId = "Cookie_UserId";
var Cookie_UserName = "Cookie_UserName";
var Cookie_Email = "Cookie_Email";

var PageInfo_CurrentPage = 1;
var PageInfo_PageSize = 3;
var PageInfo_TotalPage = 1;
var PageInfo_Pages = [];

function userStatus() {
    var status = [
        { text: "New", value: "New" },
        { text: "Checked", value: "Checked" },
        { text: "Delete", value: "Delete" }
        //{ text: "select status", value: "" }
    ];
    return status;
}

function userTypes() {
    var types = [
        { text: "Admin", value: "Admin" },
        { text: "Developer", value: "Developer" },
        { text: "QA", value: "QA" }
        //{ text: "select type", value: "" }
    ];
    return types;
}

function developerStatus() {
    var status = [
        { text: "status1", value: "status1" },
        { text: "status2", value: "status2" },
        { text: "status3", value: "status3" },
        { text: "status4", value: "status4" }
        //{ text: "select status", value: "" }
    ];
    return status;
}

function bugStatus() {
    var status = [
        { text: "New", value: "New" },
        { text: "Assigned", value: "Assigned" },
        { text: "InProgress", value: "InProgress" },
        { text: "InTest", value: "InTest" },
        { text: "Done", value: "Done" }
        //{ text: "select status", value: "" }
    ];
    return status;
}