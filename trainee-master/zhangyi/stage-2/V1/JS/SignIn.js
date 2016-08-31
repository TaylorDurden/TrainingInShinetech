$().ready(function() {
    $('#myForm').validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            password: {
                required: 'Password is required',
                minlength: 'Password length should be large than 5'
            },
            email: {
                required: 'Email is required',
                email: 'Email format is not correct'
            } 
        }
    });
});