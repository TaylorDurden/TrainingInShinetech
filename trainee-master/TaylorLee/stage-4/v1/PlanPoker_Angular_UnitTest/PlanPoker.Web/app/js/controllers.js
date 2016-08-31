var User_Name_Cookie = 'user_name';
var User_Id_Cookie = 'user_id';
var EstimatedProject_Id_Cookie = 'project_id';
var EstimatedProject_Name_Cookie = 'project_name';
var EstimatedProjectGuid_Cookie = 'estimatedProjectGuid';

var loginModule = angular.module('LoginModule', []);

loginModule.controller('loginController', function ($scope, $state, $stateParams, $cookieStore, webApiService) {
        $scope.userLoginModel = {
            userName: '',
            password: ''
        };
        $scope.loginFailed = false;
        
        $scope.userLogin = function (valid) {
            if (valid) {
                var userName = $scope.userLoginModel.userName;
                var password = $scope.userLoginModel.password;
                $scope.submitted = false;

                webApiService
                .login(userName, password)
                .success(function (data) {
                    if (!isNaN(data)) {
                        $cookieStore.put(User_Name_Cookie, $scope.userLoginModel.userName);
                        $cookieStore.put(User_Id_Cookie, data);
                        $state.go('dashboard');
                    } else {
                        $scope.loginFailed = true;
                        $scope.failedMsg = data;
                    }
                });
            } 
        };
    }
);

var registerModule = angular.module('RegisterModule', []);

registerModule.controller('registerController', function($scope, $state, $stateParams, webApiService ) {
    $scope.userRegisterModel = {
        userName: '',
        password: '',
        confirmPassword: '',
        email: '',
        image: ''
    };

    $scope.confirmFailed = false;
    $scope.rejected = false;

    $scope.$watch('userRegisterModel.confirmPassword', function () {
        $scope.confirmFailed = false;
    });

    $scope.userNameExisted = false;

    $scope.checkUser = function () {
        webApiService.checkUser($scope.userRegisterModel.userName)
        .success(function (data) {
            if (data) {
                $scope.userNameExisted = true;
            } else {
                $scope.userNameExisted = false;
            }
        });
    }
    var validatePassword;
    $scope.userRegister = function (valid) {
        if (valid) {
            if (!validatePassword($scope.userRegisterModel.password, $scope.userRegisterModel.confirmPassword)) {
                $scope.confirmFailed = true;
                return;
            }
            webApiService
            .register($scope.userRegisterModel)
            .success(function () {
                $state.go('login');
            }).error(function (data) {
                $scope.rejected = true;
                $scope.rejectedMsg = data;
            });
        }
    };
    validatePassword = function(password, confirm) {
        if (password === confirm) {
            return true;
        }
        return false;
    };
});

registerModule.directive('fileReader', function ($q) {
    var slice = Array.prototype.slice;

    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;

            ngModel.$render = function () { };

            element.bind('change', function (e) {
                var element = e.target;

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

                $q.all(slice.call(element.files, 0).map(readFile))
                    .then(function (values) {
                        if (element.multiple) {
                            ngModel.$setViewValue(values);
                        } else {
                            ngModel.$setViewValue(values.length ? values[0] : null);
                        }
                    });
            }); 

        } 
    }; 
});

var projectModule = angular.module('ProjectModule', []);

projectModule.controller('projectController', function ($rootScope, $scope, $state, $cookieStore, cookiesValidation) {
    cookiesValidation.ValidateCookies();

    $rootScope.items = [];

    $rootScope.ProjectName = $cookieStore.get(EstimatedProject_Name_Cookie);
    $rootScope.ProjectId = $cookieStore.get(EstimatedProject_Id_Cookie);
    $rootScope.ProjectGuid = $cookieStore.get(EstimatedProjectGuid_Cookie);

    $scope.goPresenter = function() {
        var url = $state.href('ProjectEstimate.presenter', { presenter: 1, projectid: $rootScope.ProjectId });
        window.open(url, '_blank');
    }

    if ($rootScope.ProjectId) {
        $rootScope.screenShow = true;
    } else {
        $rootScope.screenShow = false;
    }
});

projectModule.controller('accordionController', function ($rootScope, $scope, $cookieStore, webApiService) {

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
    
    

    webApiService.getProjects()
    .success(function (data) {
        angular.forEach(data, function (data) {
            var item = transformModel(data);
            $rootScope.items.push(item);
        });

        var projectGuid = $cookieStore.get(EstimatedProjectGuid_Cookie);
        if (projectGuid) {
            angular.forEach($rootScope.items, function (data) {
                if (data.ProjectGuid === projectGuid) {
                    data.Checked = true;
                    return;
                }
            });
        }
    });
});


projectModule.directive('options', function (webApiService, $rootScope, $cookieStore) {
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
            link: function(scope) {
                scope.checkCount = 0;

                scope.checkClick = function (popoverScope) {
                    var index = popoverScope.$index;
                    scope.checkCount++;

                    angular.forEach(popoverScope.items, function (data) {
                        data.Checked = false;
                    });
                    popoverScope.items[index].Checked = true;

                    $cookieStore.put(EstimatedProjectGuid_Cookie, popoverScope.items[index].ProjectGuid);
                    $cookieStore.put(EstimatedProject_Id_Cookie, popoverScope.items[index].Id);
                    $cookieStore.put(EstimatedProject_Name_Cookie, popoverScope.items[index].Name);
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
                    if (popoverScope.editProjectText==="") {
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

                scope.deleteProject = function(popoverScope) {
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


projectModule.controller('modalProjectController', function ($scope, $uibModal) { 
    $scope.open = function(size) { 
        $uibModal.open({
            templateUrl: 'ModalContent.html', 
            controller: 'modalInstanceController', 
            size: size 
        });
    }
});

projectModule.controller('modalInstanceController', function ($rootScope, $scope, $uibModalInstance, webApiService) { 
    $scope.project = {
        name : ''
    };

    $scope.projectExisted = false;

    $scope.projectNameChanged = function () {
        webApiService.checkProjectName($scope.project.name)
        .success(function (data) {
            if (data) {
                $scope.projectExisted = true;
            } else {
                $scope.projectExisted = false;
            }
        });
    };
    var transformModel;
    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        $uibModalInstance.close(); 
        webApiService.newProject($scope.project)
        .success(function (data) {
            var item = transformModel(data);
            $rootScope.items.push(item);
        });
    };

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel'); 
    }
    transformModel = function(projectModel) {
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

projectModule.controller('pokerController', function ($scope) {
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

projectModule.directive('scrumpoker', function (webApiService, $cookieStore, $state) {
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
        link: function(scope) {
            scope.toEstimate = function(pokerIndex) {
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


var estimateModule = angular.module('EstimateModule', []);

estimateModule.controller('estimateController', function ($scope, $state, $stateParams, $cookieStore, webApiService, cookiesValidation) {
    var calculateAverageHour = function (nums) {
        var length = nums.length;
        var sum = 0;
        var nanNum = 0;

        for (var i = 0; i < length; i++) {
            if (isNaN(nums[i])) {
                nanNum--;
                continue;
            }
            sum += Number(nums[i]);
        }

        var average = sum / (length + nanNum);
        return isNaN(Math.round(average)) ? 0 : Math.round(average);
    }

    var showCard = function () {

        $scope.flipped = true;

        var nums = [];

        angular.forEach($scope.estimates.EstimateViewModel, function (data) {
            nums.push(data.SelectedPoker);
        });

        $scope.average = calculateAverageHour(nums);
    }
    var clearQueryFinished;
    var queryFinished = function () {
        webApiService.checkEstimateCleared($cookieStore.get(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (data) {
                if ($stateParams.presenter) {
                    return;
                }
                $state.go('dashboard');
                clearQueryFinished();
            } else {
                showCard();
            }
        });
        return false;
    }

    var queryPoker = function () {
        webApiService.queryPokers($cookieStore.get(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (!data) {
                return;
            }
            $scope.estimates = data;
            if (data.IsShow) {
                if ($stateParams.presenter) {
                }
                $scope.showPokerResult();
            }
        });
    };

    var setQueryPokerTimer = function () {
        $scope.timerQueryPoker = setInterval(function() {
            queryPoker();
        }, 3000);
    }

    var clearQueryPoker = function () {
        clearInterval($scope.timerQueryPoker);
    }
    clearQueryFinished = function () {
        clearInterval($scope.timerQueryFinished);
    };
    var setQueryFinishedTimer = function() {
        $scope.timerQueryFinished = setInterval(function () {
            queryFinished();
        }, 3000);
    }

    var changeIsShow = function () {
        webApiService.changeIsShow($cookieStore.get(EstimatedProject_Id_Cookie));
    }

    $scope.showPokerResult = function () {
        if (!$scope.estimates) {
            return;
        }
        clearQueryPoker();
        changeIsShow();
        setQueryFinishedTimer();
        $scope.estimateShow = false;
        if ($stateParams.presenter) {
            $scope.estimateResultShow = true;
        }
    }

    var deleteEstimate = function () {
        webApiService.deleteEstimate($cookieStore.get(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (data) {
                if ($stateParams.presenter) {
                    return;
                }
                clearQueryFinished();
                $state.go('dashboard');
            } 
        })
        .error(function () { });
    }

    $scope.clearAndRedirect = function () {
        clearQueryFinished();
        deleteEstimate($cookieStore.get(EstimatedProject_Id_Cookie));
        setQueryPokerTimer();
        $scope.estimateResultShow = false;
        if ($stateParams.presenter) {
            $scope.estimateShow = true;
        }
        $scope.estimates = [];
        $scope.flipped = false;
        $scope.average = 0;
    }

    if ($stateParams.presenter) {
        $scope.estimateShow = true;
    } else {
        $scope.estimateShow = false;
        cookiesValidation.ValidateCookies();
    }

    $scope.ProjectName = $cookieStore.get(EstimatedProject_Name_Cookie);

    $scope.estimates = [];

    $scope.flipped = false;

    setQueryPokerTimer();
});

estimateModule.directive('estimatepoker', function () {
    return {
        restrict: 'EA',
        replace: true,
        transclude: true,
        template:
            '<div class="col-xs-4 col-md-2 plan-poker">' +
            '<div class="thumbnail">' +
            '<img ng-src="/app/imgs/{{flipped ? estimate.SelectedPoker:planpoker}}.jpg" id="card" alt="pic">' +
            '<div class="caption user-info" >' +
            '<img style="width: 40px; height: 40px;" ng-src="{{estimate.UserImage?estimate.UserImage:defaultImageUrl}}" alt="pic">' +
            '<label class="pull-right">{{estimate.UserName}}</label>' +
            '</div></div></div>',
        link: function (scope) {
            scope.planpoker = 'planpoker';
            scope.defaultImageUrl = '/app/imgs\\user.png';
        }
    }
});

var profileModule = angular.module('ProfileModule', []);

profileModule.controller('modalProfileController', function ($scope, $uibModal) { //
    $scope.open = function (size) {
        $uibModal.open({
            templateUrl: 'ModalProfile.html',
            controller: 'modalProfileInstanceController',
            size: size
        });
    }
});

profileModule.controller('modalProfileInstanceController', function ($rootScope, $scope, $cookieStore, $uibModalInstance, webApiService) {

    $scope.user = {};
    webApiService.getUserById($cookieStore.get(User_Id_Cookie))
    .success(function (data) {
        $scope.user = data;
    });

    $scope.userNameExisted = false;
    var editUser;
    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        webApiService.validatePassword($scope.user.UserName, $scope.confirmPassword)
            .success(function (data) {
                if (data) {
                    $uibModalInstance.close();
                    editUser();
                } else {
                    $scope.confirmFailed = true;
                    return;
                }
            });
    };

    editUser = function () {
        webApiService.editUser($scope.user);
        return false;
    };

    $scope.$watch('confirmPassword', function () {
        $scope.confirmFailed = false;
    });

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
});

projectModule.controller('logOutController', function ($state, $scope, $injector) {
    $scope.logOut = function () {
        Cookies.remove(EstimatedProject_Id_Cookie);
        Cookies.remove(EstimatedProject_Name_Cookie);
        Cookies.remove(EstimatedProjectGuid_Cookie);
        Cookies.remove(User_Id_Cookie);
        Cookies.remove(User_Name_Cookie);
        $state.go('login');
        $injector.get('$templateCache').removeAll();
    };
});