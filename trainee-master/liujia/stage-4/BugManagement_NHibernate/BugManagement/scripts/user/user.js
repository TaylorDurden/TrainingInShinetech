app.controller("userController", function ($scope, $rootScope, $location, $http, $cookieStore, userService) {

    //checkCookie($cookieStore, $location);

    $scope.serchCondition = "";
    $scope.currentPage = PageInfo_CurrentPage;

    userService.getUserTypes().then(function (response) {
        $scope.types = response.data;
    });

    userService.getUserStatuss().then(function (response) {
        $scope.statuss = response.data;
    });
    
    var callback = function () {
        $("#myCreateModal").modal("hide");
        $("#myDeleteModal").modal("hide");
        $scope.getData();
    };

    var serchUser = function (pageIndex) {
        userService.getUserListViewModelByCondition($scope.serchCondition, pageIndex).then(function (respone) {
            if (respone.data != null) {
                $scope.pages = respone.data.Pages;
                if (respone.data.Pages == null) {
                    $scope.totalPage = 1;
                } else {
                    $scope.totalPage = respone.data.Pages.length;
                }
                $scope.list = respone.data.Models;
            }
        });
    }

    $scope.EmailChange = function () {
        if ($scope.user.Email !== "") {
            userService.checkUserEmailIsExist($scope.user.Email, $scope.user.UserId).then(
                function (response) {
                    $scope.isExist = response.data;
                });
        }
    }

    $scope.serch = function () {
        $scope.currentPage = 1;
        serchUser(1);
    }

    $scope.createUserModal = function () {
        $scope.user = {
            UserId: 0,
            FristName: "",
            LastName: "",
            Email: "",
            Password: "",
            RegisterTime: "",
            LastLoginTime: "",
            Type: "",
            Status: ""
        };
    }

    $scope.editUserModal = function (userId) {
        userService.getUserById(userId).then(function (response) {
            $scope.user = response.data;
        });
    }

    $scope.saveUser = function () {
        if ($scope.user.UserId === 0) {
            userService.createUser($scope.user, callback);
        } else {
            userService.updateUser($scope.user, callback);
        }
    };

    $scope.deleteUserModal = function (userId) {
        $scope.userId = userId;
    }

    $scope.deleteUser = function () {
        userService.deleteUser($scope.userId, callback);
    }

    $scope.load = function (pageIndex) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        serchUser(pageIndex);
    };
});

