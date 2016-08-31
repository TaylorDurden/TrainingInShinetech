appModule.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var result = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('passwordmatch', result);
                });
            });
        }
    }
}]);

appModule.directive('fileReader', function ($q) {
    var slice = Array.prototype.slice;
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel, ctrl) {
            if (!ngModel) {
                return;
            }
            ngModel.$render = function () { };
            element.bind('change', function (e) {
                var element = e.target;
                //console.log(element.files[0].size + element.files[0].type.split('/')[1]);
                //if (!/(gif|jpg|jpeg|png|GIF|JPG|JPEG|PNG)$/.test(element.files[0].type.split('/')[1])) {
                //    scope.isStatus = true;
                //    scope.message = 'The style of picture must be gif or jpeg or jpg or png!'; 
                //    return false;
                //}
                //if (element.files[0].size > 1024 * 600 || element.files[0].size < 1024 * 10) {
                //    scope.isStatus = true;
                //    scope.message = 'The size of file between 5kb and 600kb!';
                //    return false;
                //}
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

