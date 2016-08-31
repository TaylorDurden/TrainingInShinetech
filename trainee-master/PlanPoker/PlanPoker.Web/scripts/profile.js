$(document).ready(function () {
    $("body").on("click", "#logout", function () {
        Cookies.remove(User_Name_Cookie);
        Cookies.remove(User_Id_Cookie);
        Cookies.remove(Project_Id_Cookie);
        Cookies.remove(Presenter_Cookie);
        location.href = "Login.html";
    });

    bindData();

    var options = {
        rules: {
            oldpassword: {
                required: true,
                minlength: 6
            },
            password: {
                required: true,
                minlength: 6
            },
            confirmPassword: {
                required: true,
                minlength: 6,
                equalTo: '#password'
            },
            email: {
                required: true,
                email: true
            }
        }
    };

    $('#updateForm').validate(options);

    $('#userPicture').on('click', function () {
        $('#imgUpload').click();
    });

    $('#btnSubmit').on('click', function () {
        var validator = $('#updateForm').validate(options);
        if (!validator.form()) {
            return false;
        }
        updateUser();
    });

    $('#imgUpload').change(function () {
        $('#userPicture').attr('src', '../upload/user.png');
        $('#userPictureError').addClass('hide');
        $('#userPictureError').text('');
        readURL(this);
    });

    $('body').on('keyup', '#password', function () {
        var strongRegex = new RegExp('^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$', 'g');
        var mediumRegex = new RegExp('^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$', 'g');
        var enoughRegex = new RegExp('(?=.{6,}).*', 'g');

        $('#passstrength').removeClass('label-danger');
        $('#passstrength').removeClass('label-primary');
        $('#passstrength').removeClass('label-success');
        $('#passstrength').removeClass('label-warning');

        if (!enoughRegex.test($(this).val())) {
            $('#passstrength').addClass('label-danger');
            $('#passstrength').text('Strength: More Characters');
        } else if (strongRegex.test($(this).val())) {
            $('#passstrength').addClass('label-primary');
            $('#passstrength').text('Strength: Strong!');
        } else if (mediumRegex.test($(this).val())) {
            $('#passstrength').addClass('label-success');
            $('#passstrength').text('Strength: Medium!');
        } else {
            $('#passstrength').addClass('label-warning');
            $('#passstrength').text('Strength: Weak!');
        }
        return true;
    });

    //$('#profile').on('click', function () {
    //    $('#profile').attr('href', 'Profile.html?userId=' + getQueryString(User_Id_Cookie));
    //});
    //$('#project').on('click', function () {
    //    $('#project').attr('href', 'Project.html?userId=' + getQueryString(User_Id_Cookie));
    //});
});

var oldpassword = "";

function bindData() {
    var userid = getQueryString(User_Id_Cookie);
    $.ajax({
        url: apiPath() + "/api/user/" + userid,
        type: "Get",
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {
            $("#username").val(data.UserName);
            oldpassword = data.Password;
            $("#email").val(data.Email);
            $("#userPicture").attr("src", data.Image);
        }
    });
};

function readURL(input) {
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
        }
        reader.readAsDataURL(input.files[0]);
    }
}


function updateUser() {
    $('#btnSubmit').startSpin();

    var user = {
        userid: getQueryString(User_Id_Cookie),
        username: $("#username").val(),
        password: $('#password').val(),
        email: $('#email').val(),
        image: $('#userPicture').attr('src')
    };

    if ($("#oldpassword").val() == oldpassword) {
        $.ajax({
            url: apiPath() + '/api/user',
            type: 'Put',
            data: user,

            error: function (e) {
                console.log(e.toString());
                $('#btnSubmit').stopSpin();
            },
            success: function(data) {
                $('#updateProfileModal').modal({ keyboard: false });
                clearForm();
                $('#btnSubmit').stopSpin();
            }
        });
    } else {
        showErrorTips("old password wrong");
        clearForm();
        $('#btnSubmit').stopSpin();
    }

    return false;
}

function clearForm() {
    bindData();
    $('#password').val('');
    $('#confirmPassword').val('');
    $('#oldpassword').val('');
    $('#oldpassword').focus();
}

function showErrorTips(data) {
    $('#signupErrorTips').addClass('show');
    $('#signupErrorTips').text(data);
}