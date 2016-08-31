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
    function bindData() {
        $("#userId").val(getQueryString(User_Id_Cookie));
        var parentList = $('#projectList');
        parentList.empty();
        $.ajax({
            url: apiPath() + "/api/project",
            type: "Get",
            contentType: "application/json",
            dataType: 'json',
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    parentList.append('<tr><td>' + data[i].Id + '</td><td>' + data[i].Name + '</td>' +
                        '<td><a href="#" data-toggle="modal" data-target="#editModal" class="projectEdit" data-value="' + data[i].Id + ',' + data[i].Name + '">Edit </a>' +
                        '<a href="#" data-toggle="modal" data-target="#deleteModal" class="projectDelete" data-value="' + data[i].Id + '">Delete</a></td></tr>'
                    );
                }
            }
        });
    };

    $("body").on("click", ".projectEdit", function () {
        var projectStr = this.getAttribute("data-value");
        if (projectStr) {
            var proStr = projectStr.split(',');
            $("#projectId").val(proStr[0]);
            $("#ProName").val(proStr[1]);
            $("#oldProName").val(proStr[1]);
        }
        $("#projecthint").text("");
    });

    $("#saveProject").on("click", function () {
        var proName = $("#ProName").val();
        if (proName == '') {
            $("#projecthint").text("Pelease input project name!");
            $("#ProName").focus();
            return false;
        }
        if ($("#ProName").val() == $("#oldProName").val())
        {
            $('#editModal').modal('hide');
            return false;
        }
        var result = queryByName(proName);
        if (result == false) {
            var projects = {
                id:$("#projectId").val(),
                Name: $("#ProName").val()
            };
            $.ajax({
                url: apiPath() + "/api/project",
                type: "PUT",
                data: projects,
                success: function () {
                    bindData();
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            });
        }
    });

    function queryByName(proName) {
        var result = true;
        var userId = getQueryString(User_Id_Cookie);
        if (userId == '' || userId == undefined || userId < 1) {
            location.href = "Login.html";
            return false;
        }
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

    $("body").on("click", ".projectDelete", function () {
        $("#deleteprojectId").val(this.getAttribute("data-value"));
    });

    $("body").on("click", "#deleteProject", function () {
        var projectId = $("#deleteprojectId").val();
        $.ajax({
            url: apiPath() + "/api/project?id=" + projectId,
            type: "Delete",
            success: function () {
                bindData();
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }); 

    $("body").on("click", ".btn-search", function () {
        var projectSearch = $("#projectSearch").val();
        if (projectSearch == '' || projectSearch == 'Input any text you want to search...') {
            $("#projectSearch").focus();
            return false;
        }
        $.ajax({
            url: apiPath() + "/api/project?name=" + projectSearch,
            type: "Get",
            success: function (data) {
                if (data.length > 0) {
                    var parentList = $('#projectList');
                    parentList.empty();
                    for (var i = 0; i < data.length; i++) {
                        parentList.append('<tr><td>' + data[i].Id + '</td><td>' + data[i].Name + '</td>' +
                            '<td><a href="#" data-toggle="modal" data-target="#editModal" class="projectEdit" data-value="' + data[i].Id + ',' + data[i].Name + '">Edit </a>' +
                            '<a href="#" data-toggle="modal" data-target="#deleteModal" class="projectDelete" data-value="' + data[i].Id + '">Delete</a></td></tr>'
                        );
                    }
                } else
                {
                    var parentList = $('#projectList');
                    parentList.empty();
                    parentList.append('<tr><td colspan=3 class="text-danger text-center">not find your result !</td></tr>');
                }
                
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    });
});




