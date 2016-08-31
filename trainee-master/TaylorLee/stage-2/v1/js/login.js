$(document).ready(function () {
    $("#signinForm").validate({
        rules: {
            email: {
                required: true,
				email: true
            },
            password: {
                required: true
            }
        }
    });

    var options = {
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true
            }
        }
    };
});