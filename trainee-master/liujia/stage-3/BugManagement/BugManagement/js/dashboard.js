$(document).ready(function () {
    $('#collapseOne').collapse('show');
    $('#collapseTwo').collapse('show');
    $('#collapseThree').collapse('show');
    $('#collapseFour').collapse('show');


    $('#btnSave').on("click", function () {
        saveForm('Dashboard');
    });
});

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    var bugId = $(ev.currentTarget)[0].getAttribute("data-value");
    $("#hdBugId").val(bugId);
    ev.dataTransfer.setData("Text", ev.currentTarget.id);
}

function drop(ev) {
    ev.preventDefault();

    var bugId = $("#hdBugId").val();
    var status = $(ev.currentTarget)[0].getAttribute("data-value");

    var flag = true;
    $.getJSON("/Bug/UpdateBugStatus?bugId=" + bugId + "&stauts=" + status, null, function (jsonObj) {
        if (jsonObj != "") {
            flag = false;
            alert(jsonObj);
        }
    });
    if (flag) {
        var data = ev.dataTransfer.getData("Text");
        ev.currentTarget.children[1].appendChild(document.getElementById(data));
    }
}
