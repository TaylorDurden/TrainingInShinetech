$(document).ready(function () {
    var options = {
        rules: {
            username: {
                required: true,
                minlength: 4
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

    $('#signupForm').validate(options);

    $('#userPicture').on('click',function() {
        $('#imgUpload').click();
    });

    $('#btnSubmit').on('click', function () {
        var validator = $('#signupForm').validate(options);
        if (!validator.form()) {
            return false;
        }
        createUser();
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
});

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

function createUser() {
    $('#btnSubmit').startSpin();

    var user = {
        username: $('#username').val(),
        password: $('#password').val(),
        email: $('#email').val(),
        image: $('#userPicture').attr('src')
    };

    $.ajax({
        url: apiPath() + '/api/user',
        type: 'POST',
        data: user,

        error: function (e) {
            $('#btnSubmit').stopSpin();
        },
        success: function (data) {
            if (data.indexOf('registered') > 1) {
                showErrorTips(data);
                clearForm();
            } else {
                toLogin();
            }
            $('#btnSubmit').stopSpin();
        }
    });

    return false;
}

function toLogin() {
    location.href = "login.html";
}

function clearForm() {
    $('#signupErrorTips').text(data);   
}

function clearForm() {
    $('#username').val('');
    $('#password').val('');
    $('#confirmPassword').val('');
    $('#email').val('');
    $('#username').focus();
}

function showErrorTips(data) {
    $('#signupErrorTips').addClass('show');
    $('#signupErrorTips').text(data);
}