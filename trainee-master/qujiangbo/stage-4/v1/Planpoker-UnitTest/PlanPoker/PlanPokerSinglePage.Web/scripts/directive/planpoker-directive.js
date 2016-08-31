appModule.directive("fileReader", function ($q) {
    var slice = Array.prototype.slice;
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, elements, attrs, ngModel) {
            if (!ngModel) {
                return;
            }
            ngModel.$render = function () { };
            elements.bind("change", function (element) {
                var elementFile = element.target;
                $q.all(slice.call(elementFile.files, 0).map(readFile))
                    .then(function (values) {
                        if (elementFile.multiple) ngModel.$setViewValue(values);
                        else ngModel.$setViewValue(values.length ? values[0] : null);
                    });

                function readFile(file) {
                    var deferred = $q.defer();

                    var reader = new FileReader();
                    reader.onload = function (element) {
                        deferred.resolve(element.target.result);
                    };
                    reader.onerror = function (element) {
                        deferred.reject(element);
                    };
                    reader.readAsDataURL(file);
                    return deferred.promise;
                }
            });
        }
    };
});

appModule.directive("pwCheck", [function () {
    return {
        require: "ngModel",
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = "#" + attrs.pwCheck;
            elem.add(firstPassword).on("keyup", function () {
                scope.$apply(function () {
                    var result = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity("passwordmatch", result);
                });
            });
        }
    }
}]);

