app.directive("pageInfo", function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: 'Views/paging.html',
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
                scope[attrs.method](scope.whereCondition, scope.currentPage, scope.pageSize, function (data, totalPage) {
                    scope.totalPage = totalPage;
                    var pages = [];
                    if (scope.totalPage > 1) {
                        if (scope.currentPage > scope.totalPage) {
                            scope.currentPage = scope.totalPage;
                        }
                        for (var pageindex = 1; pageindex <= scope.totalPage; pageindex++) {
                            pages.push(pageindex);
                        }
                    }
                    scope.pages = pages;
                    
                    //if (scope.currentPage > 1 && scope.currentPage < scope.totalPage) {
                    //    scope.pages = [
                    //        scope.currentPage - 1,
                    //        scope.currentPage,
                    //        scope.currentPage + 1
                    //    ];
                    //} else if (scope.currentPage == 1 && scope.totalPage > 1) {
                    //    scope.pages = [
                    //        scope.currentPage,
                    //        scope.currentPage + 1
                    //    ];
                    //} else if (scope.currentPage == scope.totalPage && scope.totalPage > 1) {
                    //    scope.pages = [
                    //        scope.currentPage - 1,
                    //        scope.currentPage
                    //    ];
                    //}
                    scope.list = data;
                });
            };
            scope.getData();
        }
    }
});