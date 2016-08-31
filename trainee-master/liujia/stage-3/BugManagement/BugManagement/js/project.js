$(document).ready(function () {
    function setForm() {
        $("#ProjectName").val("");
        $("#Description").val("");
        $("#MainContact").val("");
        $("#ContactEmail").val("");
        $("#StartTime").val("");
        $("#ProjectId").val("");
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
        var projectId = this.getAttribute("data-value");
        $.getJSON("/Project/GetProject/" + projectId, null, function (jsonObj) {
            if (jsonObj != null) {
                $("#ProjectName").val(jsonObj.ProjectName);
                $("#Description").val(jsonObj.Description);
                $("#MainContact").val(jsonObj.MainContact);
                $("#ContactEmail").val(jsonObj.ContactEmail);
                var startTime = ConvertJSONDateToJSDateObject(jsonObj.StartTime);
                $("#StartTime").val(startTime);
                $("#ProjectId").val(jsonObj.ProjectId);
            }
        });
    });

    $(".selectDelete").on("click", function () {
        var projectId = this.getAttribute("data-value");
        $("#hdSelctedProjectId").val(projectId);
    });

    $("#btnDelete").on("click", function () {
        var projectId = $("#hdSelctedProjectId").val();
        var actionStr = "/Project/Delete/projectId=" + projectId;
        $("#myform").attr({ "action": actionStr }).submit();
    });
});

function myformListSubmit(index) {
    var actionStr = "/Project/Query?pageIndex=" + index;
    $("#myListform").attr({ "action": actionStr }).submit();
}