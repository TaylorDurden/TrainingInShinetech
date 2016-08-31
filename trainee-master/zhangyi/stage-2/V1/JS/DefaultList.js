$().ready(function () {
    $('#myForm').validate({
        rules: {
            name: {
                required: true,
            },
            description: {
                required: true,
            },
            email: {
                required: true,
                email: true
            },
            mainContact: {
                required: true
            },
            startTime: {
                required: true
            }
        },
        messages: {
            name: {
                required: 'Name is required',
            },
            description: {
                required: 'Description is required',
            },
            mainContact: {
                required: 'Main contact is required'
            },
            startTime: {
                required: 'Start time is required',
               
            },
            email: {
                required: 'Email is required',
                email: 'Email format is not correct'
            }
        }
    });
});