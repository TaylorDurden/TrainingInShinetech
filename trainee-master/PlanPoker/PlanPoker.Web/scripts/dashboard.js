$(document).ready(function () {
    $("body").on("click", "#logout", function () {
        Cookies.remove(User_Name_Cookie);
        Cookies.remove(User_Id_Cookie);
        Cookies.remove(Project_Id_Cookie);
        Cookies.remove(Presenter_Cookie);
        location.href = "Login.html";
    });

    checkUser();
    bindData();
    getProjectGuid();
    function bindData() {
        $.ajax({
            url: apiPath() + "/api/project",
            type: "Get",
            contentType: "application/json",
            dataType: 'json',
            success: function (data) {
                var proName = "";
                var proId = getQueryString(Project_Id_Cookie);
                if (proId) {
                    proName += "<option selected='selected' value='-1'>Select Project</option>";
                }
                else {
                    proName += "<option value='-1'>Select Project</option>";
                }
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Id == proId) {
                        proName += "<option selected='selected' value='" + data[i].Id + "' data-guid=" + data[i].ProjectGuid + ">" + data[i].Name + "</option>";
                    } else {
                        proName += "<option value='" + data[i].Id + "'data-guid=" + data[i].ProjectGuid + ">" + data[i].Name + "</option>";
                    }
                }
                $("#projectName").empty();
                $("#projectName").append(proName);
            }
        });
    };

    function getProjectGuid() {
        $.ajax({
            url: apiPath() + "/api/getprojecturl/" + Cookies(Project_Id_Cookie),
            type: "GET",
            success: function (data) {
                $("#projectGuid").text("View result");
                $("#projectGuid").attr("href", data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#projectGuid").text("");
                $("#projectGuid").attr("href", "#");
            }
        });
    }

    $('#projectName').on('change', function (e) {
        Cookies(Project_Id_Cookie, $("#projectName").val(), { expires: 1 });
        getProjectGuid();
    });

    $("#saveProject").on("click", function () {
        var proName = $("#ProName").val();
        if (proName == '') {
            $("#projecthint").text("Pelease input project name!");
            $("#ProName").focus();
            return false;
        }
        var result = queryByName(proName);
        if (result == false) {
            var projects = {
                Name: proName
            };
            $.ajax({
                url: apiPath() + "/api/project",
                type: "POST",
                data: projects,
                success: function () {
                    bindData();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("error");
                }
            });
        }
    });



    function createEstimate(picId, userId, proId) {
        var estimate = {
            ProjectId: proId,
            UserId: userId,
            SelectedPoker: picId
        };

        $.ajax({
            url: apiPath() + "/api/estimate",
            type: "POST",
            data: estimate,
            async: false,
            error: function () {
                console.log("error");
            },
            success: function () {
                console.log("ok");
                Cookies(Project_Id_Cookie, proId, { expires: 1 });
            }
        });
    }
    $(".plan-poker-thumbnail").click(
        function () {
            var picId = this.getAttribute("data-value");
            var userId = getQueryString(User_Id_Cookie);
            var proId = getQueryString(Project_Id_Cookie);
            if (picId == '') {
                return false;
            }
            if (proId == '-1' || proId == null) {
                console.log("There is no project name,please input project name!");
                return false;
            }
            createEstimate(picId, userId, proId);
            location.href = "ProjectEstimate.html?projectGuid=" + $("#projectName option:selected").attr("data-guid");
        });


    $("#createProject").click(function () {
        $("#projecthint").text("");
        $("#ProName").val("");
        $("#ProName").style = "background-color:#000";
    });

    $('#btnResult').on('click', function () {
        var proId = $("#projectName").val();
        Cookies(Presenter_Cookie, '1', { expires: 1 });
        location.href = $("#projectGuid").text() ? $("#projectGuid").text() : "#";
    });

    function queryByName(proName) {
        var result = true;
        $.ajax({
            url: apiPath() + "/api/projectcheck?projectName=" + proName,
            type: "Get",
            async: false,
            error: function () {
                console.log("error");
            },
            success: function (data) {
                result = data;
                if (data) {
                    $("#projecthint").text("Project name is exist!");
                } else {
                    $('.close')[0].click();
                }
            }
        });
        return result;
    };
});



