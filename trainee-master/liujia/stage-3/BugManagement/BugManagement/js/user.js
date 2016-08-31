$(document).ready(function () {
    function setForm() {
        $("#FristName").val("");
        $("#LastName").val("");
        $("#Email").val("");
        $("#Password").val("");
        $("#Type").val("");
        $("#RegisterTime").val("");
        $("#LastLoginTime").val("");
        $("#Status").val("");
        $("#UserId").val("");
    }
    $('#btnCreate').on("click", function () {
        setForm();
    });

    $('#btnSave').on("click", function () {
        var validator = $('#myform').validate();
        if (validator.form()) {
            $("#myform").submit();
        }
    });

    $(".selectEdit").on("click", function () {
        var usreId = this.getAttribute("data-value");
        $.getJSON("/User/GetUser/" + usreId, null, function (jsonObj) {
            if (jsonObj != null) {
                $("#FristName").val(jsonObj.FristName);
                $("#LastName").val(jsonObj.LastName);
                $("#Email").val(jsonObj.Email);
                $("#Password").val(jsonObj.Password);
                $("#Type").val(jsonObj.Type);
                var registerTime = ConvertJSONDateToJSDateObject(jsonObj.RegisterTime);
                $("#RegisterTime").val(registerTime);
                var lastLoginTime = ConvertJSONDateToJSDateObject(jsonObj.LastLoginTime);
                $("#LastLoginTime").val(lastLoginTime);
                $("#Status").val(jsonObj.Status);
                $("#UserId").val(jsonObj.UserId);
            }
        });
    });

    $(".selectDelete").on("click", function () {
        var usreId = this.getAttribute("data-value");
        $("#hdSelctedUserId").val(usreId);
    });

    $("#btnDelete").on("click", function () {
        var userId = $("#hdSelctedUserId").val();
        var actionStr = "/User/Delete/userId=" + userId;
        $("#myform").attr({ "action": actionStr }).submit();
    });
});

function myformListSubmit(index) {
    var actionStr = "/User/Query?pageIndex=" + index;
    $("#myListform").attr({ "action": actionStr }).submit();
}