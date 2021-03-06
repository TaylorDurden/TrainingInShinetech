﻿angular.module("mainApp").directive("pwCheck", function () {
    return {
        require: "ngModel",
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = "#" + attrs.pwCheck;
            $(elem).add(firstPassword).on("keyup", function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity("pwmatch", v);
                });
            });
        }
    };
});

angular.module("mainApp").directive("fileReader", function ($q) {
    var slice = Array.prototype.slice;

    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;

            ngModel.$render = function () { };

            element.bind("change", function (e) {
                var element = e.target;

                $q.all(slice.call(element.files, 0).map(readFile))
                    .then(function (values) {
                        if (element.multiple) ngModel.$setViewValue(values);
                        else ngModel.$setViewValue(values.length ? values[0] : null);
                    });

                function readFile(file) {
                    var deferred = $q.defer();

                    var reader = new FileReader();
                    reader.onload = function (e) {
                        deferred.resolve(e.target.result);
                    };
                    reader.onerror = function (e) {
                        deferred.reject(e);
                    };
                    reader.readAsDataURL(file);
                    return deferred.promise;
                }
            });
        }
    };
});