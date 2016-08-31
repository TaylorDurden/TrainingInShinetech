app.directive("pageInfo", function () {
    return {
        restrict: "E",
        replace: true,
        templateUrl: "Views/paging.html",
        link: function (scope, ele, attrs) {
            if (!scope[attrs.method]) {
                throw new Error('load method is undefined');
            }
            scope.next = function () {
                if (scope.currentPage < scope.totalPage) {
                    scope.currentPage++;
                    scope.getData(scope.currentPage);
                }
            };
            scope.prev = function () {
                if (scope.currentPage > 1) {
                    scope.currentPage--;
                    scope.getData(scope.currentPage);
                }
            };
            scope.getData = function (page) {
                page && (scope.currentPage = page);
                scope[attrs.method](scope.currentPage);
            };
            scope.getData();
        }
    }
});