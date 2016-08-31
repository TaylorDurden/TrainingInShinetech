planPokerApp().service('userLogin', function () {
    this.loginCheck = function () {
        if (Cookies(User_Name_Cookie) == undefined) {
            location.href = "Login.html";
        }
    }
})
    .controller('dashboardController', function ($scope, $http, userLogin) {
        userLogin.loginCheck();
        var bindData = function () {
            $http.get(apiPath() + "/api/project").success(function (response) {
                $scope.projectId = Cookies(Project_Id_Cookie);
                $scope.projects = response;
            });
        }
        bindData();
        $scope.projectsName = { name: '', href: '' };
        $scope.selectChange = function ($scope) {
            Cookies(Project_Id_Cookie, $("#projectName").val(), { expires: 1 });
            $http.get(apiPath() + "/api/getprojecturl/" + Cookies(Project_Id_Cookie)).success(function (response) {
                $("#projectGuid").text("View result");
                $("#projectGuid").attr("href", response);
            }).error(function () {
                $("#projectGuid").text("");
                $("#projectGuid").attr("href", "#");
            })
        }

        $scope.selectClick = function (response) {
            $scope.estimate = {
                ProjectId: '',
                UserId: '',
                SelectedPoker: ''
            };

            $scope.estimate.ProjectId = getQueryString(Project_Id_Cookie);
            $scope.estimate.UserId = getQueryString(User_Id_Cookie);
            $scope.estimate.SelectedPoker = response;
            var selectGuid=$("#projectName option:selected").attr("data-guid");

            if ($scope.estimate.SelectedPoker == '') {
                return false;
            }
            if ($scope.estimate.ProjectId == '-1' || $scope.estimate.ProjectId == null || $scope.estimate.ProjectId == undefined || $scope.estimate.ProjectId == '' || selectGuid=='') {
                console.log("There is no project name,please input project name!");
                return false;
            }

            $http({
                method: "POST",
                url: apiPath() + "/api/estimate",
                data: $scope.estimate,
            }).success(function () {
                console.log("ok");
                Cookies(Project_Id_Cookie, $scope.estimate.ProjectId, { expires: 1 });
                location.href = "ProjectEstimate.html?projectGuid=" + selectGuid;
            }).error(function () {
                console.log("error");
            })
        }

        $scope.project = { Name: "" };
        $scope.isOk = false;
        $scope.textChange = function () {
            $http({
                method: "Get",
                url: apiPath() + "/api/projectcheck?projectName=" + $scope.project.Name
            }).success(function (response) {
                if (response) {
                    $scope.isOk = response;
                }
            }).error(function (e) {
                console.log('error');
            });
        }
        $scope.saveSubmit = function () {
            return $http({
                method: 'POST',
                url: apiPath() + "/api/project",
                data: $scope.project,
                headers: { 'Content-Type': 'application/json' }
            }).success(function () {
                console.log("ok");
                bindData();
                $('.close')[0].click();
            }).error(function () {
                console.log("error");
            })
        }

        $scope.removeCookie = function () {
            Cookies.remove(User_Name_Cookie);
            Cookies.remove(User_Id_Cookie);
            Cookies.remove(Project_Id_Cookie);
            Cookies.remove(Presenter_Cookie);
            location.href = "Login.html";
        }
})

