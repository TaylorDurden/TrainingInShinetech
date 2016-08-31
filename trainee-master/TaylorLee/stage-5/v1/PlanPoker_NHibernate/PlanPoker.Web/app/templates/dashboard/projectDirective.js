angular
    .module('ProjectModule')
    .directive('options', function (webApiService, $rootScope, $cookieStore) {
    return {
        restrict: 'EA',
        replace: true,
        transclude: true,
        template:
            '<div class="panel-body">' +
                '<div class="row">' +
                    '<input type="checkbox" name="project" ng-click="checkClick(this)" ng-model="item.Checked"/>' +
                    '<label class="btn col-sm-4" ng-class="{ ' + "'btn-success'" + ' : item.Checked, ' + "'btn-info'" + ' : !item.Checked }" name="project" ng-model="item.Checked" ng-click="checkClick(this)" uib-btn-checkbox>{{item.Name}}</label>' +
                    '<button uib-popover-template="dynamicPopover.templateUrl" popover-placement="left" popover-title="{{dynamicPopover.title}}" type="button" ng-disabled="item.Closed" ng-click="disableOthers(this)" class="btn btn-default pull-right">Edit</button>' +
                '</div>' +
            '</div>',
        link: function (scope) {
            scope.checkCount = 0;

            scope.checkClick = function (checkItemScope) {
                var index = checkItemScope.$index;
                scope.checkCount++;

                angular.forEach(checkItemScope.items, function (data) {
                    data.Checked = false;
                });
                checkItemScope.items[index].Checked = true;

                $cookieStore.put(EstimatedProjectGuid_Cookie, checkItemScope.items[index].ProjectGuid);
                $cookieStore.put(EstimatedProject_Id_Cookie, checkItemScope.items[index].Id);
                $cookieStore.put(EstimatedProject_Name_Cookie, checkItemScope.items[index].Name);
                $rootScope.ProjectName = $cookieStore.get(EstimatedProject_Name_Cookie);
                $rootScope.ProjectId = $cookieStore.get(EstimatedProject_Id_Cookie);
                $rootScope.ProjectGuid = $cookieStore.get(EstimatedProjectGuid_Cookie);

                if ($rootScope.ProjectId) {
                    $rootScope.screenShow = true;
                }
            }

            scope.popCount = 0;

            scope.disableOthers = function (popoverScope) {
                var index = popoverScope.$index;
                scope.popCount++;
                if (scope.popCount === 1) {
                    angular.forEach($rootScope.items, function (data) {
                        data.Closed = true;
                    });
                    $rootScope.items[index].Closed = false;
                    scope.editProjectText = $rootScope.items[index].Name;
                    scope.ProjectId = $rootScope.items[index].Id;
                    return;
                }
                if (scope.popCount === 2) {
                    angular.forEach($rootScope.items, function (data) {
                        data.Closed = false;
                    });
                    scope.popCount = 0;
                    return;
                }
            }

            scope.nameChanged = function (popoverScope) {
                if (popoverScope.editProjectText !== "") {
                    scope.isNameEmpty = false;
                }
                webApiService.checkProjectName(popoverScope.editProjectText)
                .success(function (data) {
                    if (data) {
                        scope.isNameRepeated = true;
                    } else {
                        scope.isNameRepeated = false;
                    }
                });
            };

            scope.ProjectName = '';
            scope.isNameEmpty = false;
            scope.isNameRepeated = true;
            scope.saveProject = function (popoverScope) {
                if (popoverScope.editProjectText === "") {
                    scope.isNameEmpty = true;
                    return;
                }
                if (scope.isNameRepeated) {
                    return;
                }

                var index = popoverScope.$index;
                var project = {
                    Id: 0,
                    Name: '',
                    ProjectGuid: ''
                }

                project.Id = $rootScope.items[index].Id;
                project.Name = popoverScope.editProjectText;
                project.ProjectGuid = $rootScope.items[index].ProjectGuid;

                webApiService.editProject(project)
                .success(function () {
                    $rootScope.items[index].Name = popoverScope.editProjectText;
                });
            }

            scope.deleteProject = function (popoverScope) {
                var index = popoverScope.$index;
                var id = $rootScope.items[index].Id;
                webApiService.deleteProject(id)
                .success(function () {
                    angular.forEach($rootScope.items, function (data) {
                        data.Closed = false;
                    });
                    if ($rootScope.items[index].Checked) {
                        $cookieStore.remove(EstimatedProjectGuid_Cookie);
                        $cookieStore.remove(EstimatedProject_Id_Cookie);
                        $cookieStore.remove(EstimatedProject_Name_Cookie);
                    }
                    $rootScope.ProjectName = '';
                    $rootScope.items.splice(index, 1);
                });
            }

            scope.dynamicPopover = {
                templateUrl: 'myPopoverTemplate.html',
                title: 'Edit',
                deleteUrl: 'deleteTemplate.html'
            }
        }
    }
}
);


angular
    .module('ProjectModule')
    .directive('scrumpoker', function (webApiService, $cookieStore, $state) {
    return {
        restrict: 'EA',
        replace: true,
        transclude: true,
        template:
            '<div class="col-md-2 col-xs-3">' +
                '<a href class="thumbnail img-thumbnail plan-poker-thumbnail">' +
                    '<img class="img-responsive" src="/app/imgs/{{poker}}.jpg?v=5" ng-click="toEstimate($index)"/>' +
                '</a>' +
            '</div>',
        link: function (scope) {
            scope.toEstimate = function (pokerIndex) {
                var estimate = {
                    ProjectId: '',
                    UserId: '',
                    SelectedPoker: ''
                };

                estimate.ProjectId = $cookieStore.get(EstimatedProject_Id_Cookie);
                estimate.UserId = $cookieStore.get(User_Id_Cookie);
                estimate.SelectedPoker = scope.pokers[pokerIndex];

                if (!estimate.ProjectId) {
                    return;
                }

                webApiService.newEstimate(estimate)
                .success(function () {
                    $state.go('ProjectEstimate');
                });
            };

        }
    }
});