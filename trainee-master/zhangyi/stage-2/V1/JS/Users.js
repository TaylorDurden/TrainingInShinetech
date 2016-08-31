﻿$().ready(function () {
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
                password: true
            }
        },
        messages: {
            firstName: {
                required: 'First name is required',
            },
            lastName: {
                required: 'Last name is required',
            },
            email: {
                required: 'Email is required',
                email: 'Email format is not correct'
            },
            password: {
                required: 'Password is required',
            }
        }
    });
});