$(document).ready(function () {
    var options = {
        rules: {
            username: {
                required: true
            },
            password: {
                required: true
            }
        }
    };

    $('#signinForm').validate(options);

    $('#signIn').on('click', function () {
        var validator = $('#signinForm').validate(options);
        if (!validator.form()) {
            return false;
        }
        login();
    });

    $('body').on('keyup', '#username', function () {
        $('#signinErrorTips').text('');
    });
    $('body').on('keyup', '#password', function () {
        $('#signinErrorTips').text('');
    });
});

function login() {
    $('#signIn').startSpin();

    var username = $('#username').val();
    var password = $('#password').val();

    $.ajax({
        url: apiPath() + '/api/user',
        type: 'Get',
        data: { username: username, password: password },

        error: function(e) {
            $('#signIn').stopSpin();
        },
        success: function(data) {
            if (!isNaN(data)) {
                Cookies(User_Name_Cookie, username, { expires: 1 });
                Cookies(User_Id_Cookie, data, { expires: 1 });
                location.href = 'Dashboard.html';
            } else {
                showErrorTips(data);
                    clearForm();
            }

            $('#signIn').stopSpin();
        }
    });

    return false;
}

function clearForm() {
}

function clearForm() {
    $('#username').val('');
    $('#password').val('');
    $('#username').focus();
}

function showErrorTips(data) {
    $('#signinErrorTips').addClass('show');
    $('#signinErrorTips').text(data);
}

function redirectToRegister() {
    location.href = 'Register.html';
}