$("[data-operation='btnSignin']").on("click", function () {
    UserLogin();
});

function UserLogin() {
    var email = $("#text_Email").val();
    var password = $("#text_Password").val();

    var url = "/User/Login";
    var parameter = { email: email, password: password };

    $.post(url, parameter, function(data) {
        window.location = "/Dashboard";
    });
}