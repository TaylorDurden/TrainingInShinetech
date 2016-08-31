var projectModule = angular.module('ProjectModule', []);

projectModule.controller('projectController', function ($rootScope, $scope, $state, cookiesValidation) {
    cookiesValidation.ValidateCookies();

    $rootScope.items = [];

    $rootScope.ProjectName = Cookies(EstimatedProject_Name_Cookie);
    $rootScope.ProjectId = Cookies(EstimatedProject_Id_Cookie);
    $rootScope.ProjectGuid = Cookies(EstimatedProjectGuid_Cookie);

    $scope.goPresenter = function () {
        var url = $state.href('ProjectEstimate.presenter', { presenter: 1, projectid: $rootScope.ProjectId });
        window.open(url, '_blank');
    }

    if ($rootScope.ProjectId) {
        $rootScope.screenShow = true;
    } else {
        $rootScope.screenShow = false;
    }
});

projectModule.controller('accordionController', function ($rootScope, $scope, webApiService) {

    var transformModel = function (projectModel) {
        var item = {
            Id: '',
            Name: '',
            ProjectGuid: '',
            Checked: false,
            Closed: false
        };

        item.Id = projectModel.Id;
        item.Name = projectModel.Name;
        item.ProjectGuid = projectModel.ProjectGuid;
        return item;
    };



    webApiService.getProject()
    .success(function (data, status, headers, config) {
        angular.forEach(data, function (data) {
            var item = transformModel(data);
            $rootScope.items.push(item);
        });

        var projectGuid = Cookies(EstimatedProjectGuid_Cookie);
        if (projectGuid) {
            angular.forEach($rootScope.items, function (data) {
                if (data.ProjectGuid === projectGuid) {
                    data.Checked = true;
                    return;
                }
            });
        }
    }).error(function (data, status, headers, config) {
    });

    $scope.oneAtATime = true;
});


projectModule.directive('options', function (webApiService, $rootScope, $cookies) {
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
        link: function (scope, element, attrs) {
            scope.checkCount = 0;

            scope.checkClick = function (popoverScope) {
                var index = popoverScope.$index;
                scope.checkCount++;

                angular.forEach(popoverScope.items, function (data) {
                    data.Checked = false;
                });
                popoverScope.items[index].Checked = true;

                Cookies.set(EstimatedProjectGuid_Cookie, popoverScope.items[index].ProjectGuid, { expires: 1 });
                Cookies.set(EstimatedProject_Id_Cookie, popoverScope.items[index].Id, { expires: 1 });
                Cookies.set(EstimatedProject_Name_Cookie, popoverScope.items[index].Name, { expires: 1 });
                $rootScope.ProjectName = Cookies.set(EstimatedProject_Name_Cookie);
                $rootScope.ProjectId = Cookies.set(EstimatedProject_Id_Cookie);
                $rootScope.ProjectGuid = Cookies.set(EstimatedProjectGuid_Cookie);;

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
                .success(function (data, status, headers, config) {
                    if (data) {
                        scope.isNameRepeated = true;
                    } else {
                        scope.isNameRepeated = false;
                    }
                }).error(function (data, status, headers, config) {
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
                .success(function (data, status, headers, config) {
                    $rootScope.items[index].Name = popoverScope.editProjectText;
                })
                .error(function (data, status, headers, config) {
                });
            }

            scope.deleteProject = function (popoverScope) {
                var index = popoverScope.$index;
                var id = $rootScope.items[index].Id;
                webApiService.deleteProject(id)
                .success(function (data, status, headers, config) {

                    angular.forEach($rootScope.items, function (data) {
                        data.Closed = false;
                    });
                    if ($rootScope.items[index].Checked) {
                        Cookies.remove(EstimatedProjectGuid_Cookie);
                        Cookies.remove(EstimatedProject_Id_Cookie);
                        Cookies.remove(EstimatedProject_Name_Cookie);
                    }
                    $rootScope.ProjectName = '';
                    $rootScope.items.splice(index, 1);
                })
                .error(function (data, status, headers, config) {
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


projectModule.controller('modalProjectController', function ($scope, $uibModal, $log) { //
    $scope.open = function (size) {
        var modalInstance = $uibModal.open({
            templateUrl: 'ModalContent.html',
            controller: 'modalInstanceController',
            size: size

        });
        modalInstance.result.then(function (selectedItem) {
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    }
});

projectModule.controller('modalInstanceController', function ($rootScope, $scope, $uibModalInstance, webApiService) {
    $scope.project = {
        name: ''
    };

    $scope.projectExisted = false;

    $scope.projectNameChanged = function () {
        webApiService.checkProjectName($scope.project.name)
        .success(function (data, status, headers, config) {
            if (data) {
                $scope.projectExisted = true;
            } else {
                $scope.projectExisted = false;
            }
        }).error(function (data, status, headers, config) {
        });
    };

    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        var root = $rootScope;
        $uibModalInstance.close();
        webApiService.newProject($scope.project)
        .success(function (data, status, headers, config) {
            var item = transformModel(data);
            $rootScope.items.push(item);
        }).error(function (data, status, headers, config) {
        });
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }

    var transformModel = function (projectModel) {
        var item = {
            Id: '',
            Name: '',
            ProjectGuid: '',
            Checked: false,
            Closed: false
        };

        item.Id = projectModel.Id;
        item.Name = projectModel.Name;
        item.ProjectGuid = projectModel.ProjectGuid;
        return item;
    };

});

projectModule.controller('pokerController', function ($scope, $state, webApiService) {
    $scope.pokers = [
        '1',
        '2',
        '3',
        '5',
        '8',
        '13',
        '20',
        '40',
        '100',
        's',
        'm',
        'l',
        'xs',
        'yes',
        'no',
        'unknown',
        'java'
    ];
});

projectModule.directive('scrumpoker', function (webApiService, $cookies, $state) {
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
        link: function (scope, element, attrs) {
            scope.toEstimate = function (pokerIndex) {
                var estimate = {
                    ProjectId: '',
                    UserId: '',
                    SelectedPoker: ''
                };

                estimate.ProjectId = Cookies(EstimatedProject_Id_Cookie);
                estimate.UserId = Cookies(User_Id_Cookie);
                estimate.SelectedPoker = scope.pokers[pokerIndex];

                if (!estimate.ProjectId) {
                    return;
                }

                webApiService.newEstimate(estimate)
                .success(function (data, status, headers, config) {
                    $state.go('ProjectEstimate');
                }).error(function (data, status, headers, config) {

                });
            };

        }
    }
});

projectModule.controller('logOutController', function ($state, $scope) {
    $scope.logOut = function () {
        Cookies.remove(EstimatedProject_Id_Cookie);
        Cookies.remove(EstimatedProject_Name_Cookie);
        Cookies.remove(EstimatedProjectGuid_Cookie);
        Cookies.remove(User_Id_Cookie);
        Cookies.remove(User_Name_Cookie);
        $state.go('login');
    };
});