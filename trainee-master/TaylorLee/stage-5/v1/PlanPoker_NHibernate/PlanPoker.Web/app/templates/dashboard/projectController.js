var projectModule = angular.module('ProjectModule', []);

projectModule.controller('projectController', function ($rootScope, $scope, $state, $cookieStore, cookiesValidation) {
    cookiesValidation.ValidateCookies();

    $rootScope.items = [];

    $rootScope.ProjectName = $cookieStore.get(EstimatedProject_Name_Cookie);
    $rootScope.ProjectId = $cookieStore.get(EstimatedProject_Id_Cookie);
    $rootScope.ProjectGuid = $cookieStore.get(EstimatedProjectGuid_Cookie);

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

    webApiService.getProjects()
    .success(function (data) {
        angular.forEach(data, function (data) {
            var item = webApiService.convertToCheckItem(data);
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

    });
});

projectModule.controller('modalProjectController', function ($scope, $uibModal) { //
    $scope.open = function (size) {
        $uibModal.open({
            templateUrl: 'ModalContent.html',
            controller: 'modalInstanceController',
            size: size
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
        .success(function (data) {
            if (data) {
                $scope.projectExisted = true;
            } else {
                $scope.projectExisted = false;
            }
        });
    };

    $scope.submit = function (vaild) {
        if (!vaild) {
            return;
        }
        
        $uibModalInstance.close();
        webApiService.newProject($scope.project)
        .success(function (data) {
            var item = webApiService.convertToCheckItem(data);
            $rootScope.items.push(item);
        });
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
    
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


projectModule.controller('logOutController', function ($state, $scope, $cookieStore) {
    $scope.logOut = function () {
        $cookieStore.remove(EstimatedProject_Id_Cookie);
        $cookieStore.remove(EstimatedProject_Name_Cookie);
        $cookieStore.remove(EstimatedProjectGuid_Cookie);
        $cookieStore.remove(User_Id_Cookie);
        $cookieStore.remove(User_Name_Cookie);
        $state.go('login');
        $injector.get('$templateCache').removeAll();
    };
});