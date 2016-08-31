$(document).ready(function () {
    function setForm() {
        $("#FristName").val("");
        $("#LastName").val("");
        $("#Email").val("");
        $("#Status").val("");
        $("#UserId").val("");
        $("#DeveloperId").val("");
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
        var developerId = this.getAttribute("data-value");
        $.getJSON("/Developer/GetDeveloper/" + developerId, null, function (jsonObj) {
            if (jsonObj != null) {
                $("#FristName").val(jsonObj.FristName);
                $("#LastName").val(jsonObj.LastName);
                $("#Email").val(jsonObj.Email);
                $("#Status").val(jsonObj.Status);
                $("#UserId").val(jsonObj.UserId);
                $("#DeveloperId").val(jsonObj.DeveloperId);
            }
        });
    });

    $(".selectDelete").on("click", function () {
        var developerId = this.getAttribute("data-value");
        $("#hdSelctedDeveloperId").val(developerId);
    });

    $("#btnDelete").on("click", function () {
        var developerId = $("#hdSelctedDeveloperId").val();
        var actionStr = "/Developer/Delete/developerId=" + developerId;
        $("#myform").attr({ "action": actionStr }).submit();
    });
});

function myformListSubmit(index) {
    var actionStr = "/Developer/Query?pageIndex=" + index;
    $("#myListform").attr({ "action": actionStr }).submit();
}