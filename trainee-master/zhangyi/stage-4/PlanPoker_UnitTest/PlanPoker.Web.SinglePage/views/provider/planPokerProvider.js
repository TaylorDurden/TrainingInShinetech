angular.module("mainApp").provider("checkUserProvider", function () {

    this.$get = function () {
        var result = {};
        result.checkUser = function () {
            if (Cookies(User_Name_Cookie) == undefined) {
                redirectToLogin();
            }
        }
        return result;
    }
});
