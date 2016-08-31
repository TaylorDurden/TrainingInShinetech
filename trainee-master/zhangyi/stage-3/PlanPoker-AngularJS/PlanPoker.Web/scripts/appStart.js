var mainApp = angular.module("mainApp", []);

mainApp.provider('checkUserProvider', function() {

    this.$get = function() {
        var result = {};
        result.checkUser = function() {
            if (Cookies(User_Name_Cookie) == undefined) {
                redirectToLogin();
            }
        }
        return result;
    }
});

mainApp.directive('pwCheck', function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            $(elem).add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('pwmatch', v);
                });
            });
        }
    };
});