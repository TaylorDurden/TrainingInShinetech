$(document).ready(function() {
    $('#signupForm').validate({
        rules: {
            firstName: {
                required: true,
            },
            lastName: {
                required: true,
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
    });

    var options = {
        rules: {
            firstName: {
                required: true,
            },
            lastName: {
                required: true,
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
});