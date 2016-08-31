$(document).ready(function () {

    $('#uploadify').uploadify({
        uploader: '/bug/upload',
        swf: '/uploadify/uploadify.swf',
        buttonText: "Attachments",
        width: 260,
        buttonCursor: 'hand',
        fileObjName: 'Filedata',
        fileTypeExts: "*.jpg;*.png",
        fileTypeDesc: "Please select jpg png file",
        auto: true,
        multi: true,
        queueSizeLimit: 5,

        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("The number of file upload is beyond limitation" + $('#uploadify').uploadify('settings', 'queueSizeLimit') + " files！");
                    break;
                case -110:
                    alert("File [" + file.name + "] size exceeds the system limit" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "！");
                    break;
                case -120:
                    alert("File [" + file.name + "] size abnormal！");
                    break;
                case -130:
                    alert("File [" + file.name + "] type is not correct！");
                    break;
            }
        },

        'onFallback': function () {
            alert("You do not install FLASH control, can't upload pictures!Please try again after installing FLASH control.");
        },

        'onUploadSuccess': function (fileObj, data, response) {
            var result = JSON.parse(data);
            $("#showSelectDocument").append("<lable><a href='#' data-value='" + result.path + "' onclick='removeSelectedDocument(this)'><span class='glyphicon glyphicon-remove'></span></a>" + fileObj.name + "<br/></lable>");
        }
    });

    function setForm() {
        $("#BugId").val("");
        $("#Title").val("");
        $("#Smmary").val("");
        $("#Description").val("");
        $("#Type").val("");
        $("#ProjectId").val("");
    }

    $('#btnCreate').on("click", function () {
        setForm();
        initSelectDeveloper();
        initSelectDocument();

        $("#showStatus").text("New");
        $("#Status").val("New");
        var datatime = new Date().Format("yyyy-MM-dd hh:mm:ss");
        $("#showCreateTime").text(datatime);
        $("#Createtime").val(datatime);
        $("#showCreator").text("lliu");
        $("#Creator").val("lliu");
    });
    
    $(".selectEdit").on("click", function () {
        initSelectDeveloper();
        initSelectDocument();

        var bugId = this.getAttribute("data-value");
        $.getJSON("/Bug/GetBug/" + bugId, null, function (jsonObj) {
            if (jsonObj != null) {
                $("#BugId").val(jsonObj.BugId);
                $("#Title").val(jsonObj.Title);
                $("#Smmary").val(jsonObj.Smmary);
                $("#Description").val(jsonObj.Description);
                $("#Type").val(jsonObj.Type);
                $("#ProjectId").val(jsonObj.ProjectId);
                $("#showStatus").text(jsonObj.Status);
                $("#Status").val(jsonObj.Status);
                var createtime = ConvertJSONDateToJSDateObject(jsonObj.Createtime);
                $("#showCreateTime").text(createtime);
                $("#Createtime").val(createtime);
                $("#showCreator").text("lliu");
                $("#Creator").val("lliu");
            }
        });

        $.getJSON("/Bug/GetBugDeveloper/" + bugId, null, function (jsonObj) {
            if (jsonObj != null && jsonObj.length > 0) {
                $.each(jsonObj, function (i, val) {
                    var developerId = val.DeveloperId;
                    var text = val.FristName + " " + val.LastName;
                    $("#showSelectDeveloper").append("<lable><a href='#' data-value='" + developerId + "," + text + "' onclick='removeSelectedDeveloper(this)'><span class='glyphicon glyphicon-remove'></span></a>" + text + "<br/></lable>");

                    $(".SelectDeveloper li a").each(function () {
                        var data_id = this.getAttribute("data-id");
                        if (developerId == data_id) {
                            $(this).parent().remove();
                        }
                    });
                });
            }
        });

        $.getJSON("/Bug/GetBugDocument/" + bugId, null, function (jsonObj) {
            if (jsonObj != null && jsonObj.length > 0) {
                $.each(jsonObj, function (i, val) {
                    var path = val.Path;

                    var strPaths = path.split("\\");

                    var text = strPaths[strPaths.length - 1];
                    $("#showSelectDocument").append("<lable><a href='#' data-value='" + path + "' onclick='removeSelectedDocument(this)'><span class='glyphicon glyphicon-remove'></span></a>" + text + "<br/></lable>");
                });
            }
        });
    });
});

function saveForm(page)
{
    var strDeveloper = getDeveloperValue();
    $("#strDevelopers").val(strDeveloper);
    var strDocument = getDocumentValue();
    $("#strDocuments").val(strDocument);
    var validator = $('#myform').validate();
    if (validator.form()) {
        var actionStr = "/Bug/Create?strPage="+page;
        $("#myform").attr({ "action": actionStr }).submit();
    }
}

function initSelectDeveloper() {
    $("#showSelectDeveloper").children().remove();
    $('.SelectDeveloper li').remove();
    $('.SelectDeveloper').append($('.SelectDeveloperhide li').clone());
}

function initSelectDocument() {
    $("#showSelectDocument").children().remove();
}

function getDeveloperValue() {
    var result = "";
    $("#showSelectDeveloper a").each(function () {
        var dataValue = $(this).attr("data-value").split(',');
        result += dataValue[0] + ",";
    });
    if (result.length > 0) {
        result = result.substr(0, result.length - 1);
    }
    return result;
}

function getDocumentValue() {
    var result = "";
    $("#showSelectDocument a").each(function () {
        var dataValue = $(this).attr("data-value");
        result += dataValue + ",";
    });
    if (result.length > 0) {
        result = result.substr(0, result.length - 1);
    }
    return result;
}

function myformListSubmit(index) {
    var actionStr = "/Bug/Query?pageIndex=" + index;
    $("#myListform").attr({ "action": actionStr }).submit();
}

function statusOnchange(selectValue) {
    if (selectValue == "") {
        selectValue = "New";
    }
    $("#showStatus").text(selectValue);
    $("#Status").val(selectValue);
}

function developerSelect(obj, developerId) {
    $("#showSelectDeveloper").append("<lable><a href='#' data-value='" + developerId + "," + $(obj).text() + "' onclick='removeSelectedDeveloper(this)'><span class='glyphicon glyphicon-remove'></span></a>" + $(obj).text() + "<br/></lable>");
    $(obj).parent().remove();
}

function removeSelectedDeveloper(obj) {
    var valueStr = $(obj).attr("data-value").split(',');
    $(".SelectDeveloper").append("<li><a href='#' data-id='" + valueStr[0] + "' onclick='developerSelect(this," + valueStr[0] + ")'>" + valueStr[1] + "</a></li>");
    $(obj).parent().remove();
}

function removeSelectedDocument(obj) {
    $(obj).parent().remove();
}