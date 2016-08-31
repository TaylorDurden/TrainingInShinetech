function getQueryString(name) {
    return Cookies(name);
};

function getUrlString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var parameter = window.location.search.substr(1).match(reg);
    var context = "";
    if (parameter != null) {
        context = parameter[2];
    }
    return context == null || context == "" || context == "undefined" ? "" : context;
};

var User_Name_Cookie = 'user_name';
var User_Id_Cookie = 'user_id';
var Project_Id_Cookie = 'project_id';
var Presenter_Cookie = 'presenter';
var Project_GUID_Cookie = 'project_guid';

$(document).ready(function () {
    $("body").on("click", "#logout", function () {
        Cookies.remove(User_Name_Cookie);
        Cookies.remove(User_Id_Cookie);
        Cookies.remove(Project_Id_Cookie);
        Cookies.remove(Presenter_Cookie);
        Cookies.remove(Project_GUID_Cookie);
        location.href = "Login.html";
    });
});

function refresh() {
    location.href = location.href;
}

function readURL(input) {
    var result = '';
    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|JPEG|PNG)$/.test(input.files[0].name)) {
        $('#userPictureError').addClass('show');
        $('#userPictureError').text('The style of picture must be in .gif,jpeg,jpg,png');
        return false;
    }
    if (input.files[0].size > 1024 * 600 || input.files[0].size < 1024 * 5) {
        $('#userPictureError').addClass('show');
        $('#userPictureError').text('The size of file betwwen 5kb and 600kb!');
        return false;
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.createElement('img');
            document.body.insertAdjacentElement('beforeEnd', img);
            img.style.visibility = 'hidden';
            img.src = e.target.result;
            var imgwidth = img.offsetWidth;
            var imgheight = img.offsetHeight;
            if (imgwidth > 400 || imgheight > 400) {
                $('#userPictureError').addClass('show');
                $('#userPictureError').text('The width or the heigth  must be not more than 400px!');
                return false;
            }
            $('#userPicture').attr('src', e.target.result);
            $('#hideImage').val(e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
        return result;
    }
}

function redirectToDashboard() {
    location.href = 'DashBoard.html';
}

function redirectToLogin() {
    location.href = "Login.html";
}

function redirectToEstimate() {
    location.href = "ProjectEstimate.html?projectGuid=" + Cookies(Project_GUID_Cookie);
}

function redirectToRegister() {
    location.href = 'Register.html';
}