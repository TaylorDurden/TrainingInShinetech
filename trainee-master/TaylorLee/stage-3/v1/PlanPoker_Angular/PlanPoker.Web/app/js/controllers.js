var User_Name_Cookie = 'user_name';
var User_Id_Cookie = 'user_id';
var EstimatedProject_Id_Cookie = 'project_id';
var EstimatedProject_Name_Cookie = 'project_name';
var EstimatedProjectGuid_Cookie = 'estimatedProjectGuid';
var EstimatedStartDate_Cookie = 'StartDate';

var loginModule = angular.module('LoginModule', []);

loginModule.controller('loginController', function ($scope, $state, $stateParams, webApiService) {
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
                .success(function (data, status, headers, config) {
                    if (!isNaN(data)) {
                        Cookies(User_Name_Cookie, $scope.userLoginModel.userName, { expires: 1 });
                        Cookies(User_Id_Cookie, data, { expires: 1 });
                        $state.go('dashboard');
                    } else {
                        $scope.loginFailed = true;
                        $scope.failedMsg = data;
                    }
                }).error(function (data, status, headers, config) {
                    
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

    $scope.$watch('userRegisterModel.confirmPassword', function (newValue, oldValue) {
        $scope.confirmFailed = false;
    });

    $scope.userNameExisted = false;

    $scope.checkUser = function () {
        webApiService.checkUser($scope.userRegisterModel.userName)
        .success(function (data, status, headers, config) {
            if (data) {
                $scope.userNameExisted = true;
            } else {
                $scope.userNameExisted = false;
            }
        }).error(function (data, status, headers, config) {
        });
    }

    $scope.userRegister = function (valid) {
        if (valid) {
            if (!validatePassword($scope.userRegisterModel.password, $scope.userRegisterModel.confirmPassword)) {
                $scope.confirmFailed = true;
                return;
            }
            webApiService
            .register($scope.userRegisterModel)
            .success(function (data, status, headers, config) {
                $state.go('login');
            }).error(function (data, status, headers, config) {
                $scope.rejected = true;
                $scope.rejectedMsg = data;
            });
        }
    };

    var validatePassword = function(password, confirm) {
        if (password === confirm) {
            return true;
        }
        return false;
    }
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

                $q.all(slice.call(element.files, 0).map(readFile))
                    .then(function (values) {
                        if (element.multiple) {
                            ngModel.$setViewValue(values);
                        } else {
                            ngModel.$setViewValue(values.length ? values[0] : null);
                        }
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

var projectModule = angular.module('ProjectModule', []);

projectModule.controller('projectController', function ($rootScope, $scope, $state, cookiesValidation) {
    cookiesValidation.ValidateCookies();

    $rootScope.items = [];

    $rootScope.ProjectName = Cookies(EstimatedProject_Name_Cookie);
    $rootScope.ProjectId = Cookies(EstimatedProject_Id_Cookie);
    $rootScope.ProjectGuid = Cookies(EstimatedProjectGuid_Cookie);
    $rootScope.StartDate = Cookies(EstimatedStartDate_Cookie);

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

projectModule.controller('DatepickerDemoCtrl', function ($rootScope, $scope, $log) {
    $scope.today = function() {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function() {
        $scope.dt = null;
    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: new Date(),
        showWeeks: true
    };

    $scope.dateOptions = {
        dateDisabled: disabled,
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    // Disable weekend selection
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }

    $scope.toggleMin = function() {
        $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
        $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
    };

    $scope.toggleMin();

    $scope.open1 = function() {
        $scope.popup1.opened = true;
    };

    $scope.open2 = function() {
        $scope.popup2.opened = true;
    };

    $scope.setDate = function(year, month, day) {
        $scope.dt = new Date(year, month, day);
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    $scope.popup2 = {
        opened: false
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date(tomorrow);
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.events = [
      {
          date: tomorrow,
          status: 'full'
      },
      {
          date: afterTomorrow,
          status: 'partially'
      }
    ];

    function getDayClass(data) {
        var date = data.date,
          mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0,0,0,0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0,0,0,0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }

    $scope.mytime = new Date();

    $scope.hstep = 1;
    $scope.mstep = 15;

    $scope.options = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30]
    };

    $scope.ismeridian = true;
    $scope.toggleMode = function () {
        $scope.ismeridian = !$scope.ismeridian;
    };

    $scope.update = function () {
        var d = new Date();
        d.setHours(14);
        d.setMinutes(0);
        $scope.mytime = d;
    };
    
    $scope.changed = function () {
        $log.log('Time changed to: ' + $scope.mytime);
        var d = $scope.mytime;
        $scope.UTC = Date.UTC(d.getUTCFullYear(), d.getUTCMonth(), d.getUTCDate(), d.getUTCHours(), d.getUTCMinutes(), d.getUTCSeconds());
        $rootScope.StartDate = $scope.mytime.toUTCString();
    };

    $scope.clear = function () {
        $scope.mytime = null;
    };
});

projectModule.controller('accordionController', function ($rootScope, $scope, webApiService) {

    var transformModel = function (projectModel) {
        var item = {
            Id: '',
            Name: '',
            ProjectGuid: '',
            StartDate: '',
            Checked: false,
            Closed: false
        };

        item.Id = projectModel.Id;
        item.Name = projectModel.Name;
        item.ProjectGuid = projectModel.ProjectGuid;
        item.StartDate = projectModel.StartDate;
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
            link: function(scope, element, attrs) {
                scope.checkCount = 0;

                var getLocalDateTime = function (utc) {
                    var utcTime = new Date(utc);
                    var dateTimeOffset = new Date().getTimezoneOffset();
                    return new Date(utcTime.setMinutes(utcTime.getMinutes() - dateTimeOffset))
                        .toLocaleString();
                };

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
                    Cookies.set(EstimatedStartDate_Cookie, popoverScope.items[index].StartDate, { expires: 1 });
                    $rootScope.ProjectName = Cookies.set(EstimatedProject_Name_Cookie);
                    $rootScope.ProjectId = Cookies.set(EstimatedProject_Id_Cookie);
                    $rootScope.ProjectGuid = Cookies.set(EstimatedProjectGuid_Cookie);
                    $rootScope.StartDate = getLocalDateTime(popoverScope.items[index].StartDate);

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
                    .success(function (data, status, headers, config) {
                        $rootScope.items[index].Name = popoverScope.editProjectText;
                    })
                    .error(function (data, status, headers, config) {
                    });
                }

                scope.deleteProject = function(popoverScope) {
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
    $scope.open = function(size) { 
        var modalInstance = $uibModal.open({
            templateUrl: 'ModalContent.html', 
            controller: 'modalInstanceController', 
            size: size 

        });
        modalInstance.result.then(function(selectedItem) {
        }, function() {
            $log.info('Modal dismissed at: ' + new Date());
        });
    }
});

projectModule.controller('modalInstanceController', function ($rootScope, $scope, $uibModalInstance, webApiService) { 
    $scope.project = {
        name : '',
        projectGuid : '',
        startDate : ''
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

    var getUTCTimeWithoutGMTString = function(utc) {
        return utc.replace(' GMT', '');
    }
    
    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        $scope.project.startDate = getUTCTimeWithoutGMTString($rootScope.StartDate);
        var root = $rootScope;
        $uibModalInstance.close(); 
        webApiService.newProject($scope.project)
        .success(function (data, status, headers, config) {
            var item = transformModel(data);
            $rootScope.items.push(item);
        }).error(function (data, status, headers, config) {
        });
    };

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel'); 
    }

    var transformModel = function(projectModel) {
        var item = {
            Id: '',
            Name: '',
            ProjectGuid: '',
            StartDate: '',
            Checked: false,
            Closed: false
        };

        item.Id = projectModel.Id;
        item.Name = projectModel.Name;
        item.ProjectGuid = projectModel.ProjectGuid;
        item.StartDate = projectModule.StartDate;
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
        link: function(scope, element, attrs) {
            scope.toEstimate = function(pokerIndex) {
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


var estimateModule = angular.module('EstimateModule', []);

estimateModule.controller('estimateController', function ($scope, $state, $stateParams, webApiService, cookiesValidation) {
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
    var queryFinished = function () {
        webApiService.checkEstimateCleared(Cookies(EstimatedProject_Id_Cookie))
        .success(function (data) {
            if (data) {
                if ($stateParams.presenter) {
                    return;
                }
                $state.go('dashboard');
            } else {
                showCard();
            }
        })
        .error(function () { });
        return false;
    }

    var queryPoker = function () {
        webApiService.queryPokers(Cookies(EstimatedProject_Id_Cookie))
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
        })
        .error(function () {

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
    var clearQueryFinished = function () {
        clearInterval($scope.timerQueryFinished);
    }

    var setQueryFinishedTimer = function() {
        $scope.timerQueryFinished = setInterval(function () {
            queryFinished();
        }, 3000);
    }

    var changeIsShow = function () {
        webApiService.changeIsShow(Cookies(EstimatedProject_Id_Cookie))
        .success(function (data) {
        })
        .error(function () {
        });
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

    

    var deleteEstimate = function (projectId) {
        webApiService.deleteEstimate(Cookies(EstimatedProject_Id_Cookie))
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
        deleteEstimate(Cookies(EstimatedProject_Id_Cookie));
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

    $scope.ProjectName = Cookies(EstimatedProject_Name_Cookie);

    $scope.estimates = [];

    $scope.flipped = false;

    setQueryPokerTimer();
});

estimateModule.directive('estimatepoker', function (webApiService, $cookies, $state) {
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
        link: function (scope, element, attrs) {
            scope.planpoker = 'planpoker';
            scope.defaultImageUrl = '/app/imgs\\user.png';
        }
    }
});

var profileModule = angular.module('ProfileModule', []);

profileModule.controller('modalProfileController', function ($scope, $uibModal, $log) { //
    $scope.open = function (size) {
        var modalInstance = $uibModal.open({
            templateUrl: 'ModalProfile.html',
            controller: 'modalProfileInstanceController',
            size: size
        });
        modalInstance.result.then(function (selectedItem) {
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    }
});

profileModule.controller('modalProfileInstanceController', function ($rootScope, $scope, $uibModalInstance, webApiService) {

    $scope.user = {};
    webApiService.getUserById(Cookies(User_Id_Cookie))
    .success(function (data) {
        $scope.user = data;
    })
    .error(function () { });

    $scope.userNameExisted = false;

    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        if (!validatePassword($scope.user.Password, $scope.confirmPassword)) {
            $scope.confirmFailed = true;
            return;
        }
        $uibModalInstance.close();
        editUser();
    };

    

    var editUser = function () {
        var isExisted = false;
        webApiService.editUser($scope.user)
        .success(function (data, status, headers, config) { 
        }).error(function (data, status, headers, config) {
        });

        return isExisted;
    }

    $scope.$watch('confirmPassword', function (newValue, oldValue) {
        $scope.confirmFailed = false;
    });

    var validatePassword = function (password, confirm) {
        if (password === confirm) {
            return true;
        }
        return false;
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
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