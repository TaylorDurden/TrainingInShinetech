$().ready(function () {
    $('#myForm').validate({
        rules: {
            title: {
                required: true,
            },
            summary: {
                required: true,
            },
            description: {
                required: true,
            }
        },
        messages: {
            title: {
                required: 'Title is required',
            },
            summary: {
                required: 'Summary is required',
            },
            description: {
                required: 'Description is required'
            }
        }
            
    });
});