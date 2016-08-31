$().ready(function () {
    $('#myForm').validate({
        rules: {
            firstName: {
                required: true,
            },
            lastName: {
                required: true,
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 5
            },
            repartPassword: {
                required: true,
                equalTo: "#password"
            }
        },
        messages: {
            firstName: {
                required: 'FirstName is required',
            },
            lastName: {
                required: 'LastName is required',
            },
            password: {
                required: 'Password is required',
                minlength: 'Password length should be large than 5',
            },
            repartPassword: {
                required: 'RepartPassword is required',
                equalTo: 'RepartPassword is not equal with Password',
            },
            email: {
                required: 'Email is required',
                email: 'Email format is not correct'
            }
        }
    });
});