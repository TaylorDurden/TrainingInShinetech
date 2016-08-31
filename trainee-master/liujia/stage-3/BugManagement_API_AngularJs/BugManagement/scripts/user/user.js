app.controller("userController", function ($scope, $rootScope, $location, $http, $cookieStore, userService) {

    checkCookie($cookieStore, $location);

    $scope.whereCondition = "";
    $scope.types = userTypes();
    $scope.statuss = userStatus();

    $scope.currentPage = PageInfo_CurrentPage;
    $scope.pageSize = PageInfo_PageSize;
    $scope.totalPage = PageInfo_TotalPage;
    $scope.pages = PageInfo_Pages;

    var callback = function () {
        $("#myCreateModal").modal("hide");
        $("#myDeleteModal").modal("hide");
        $scope.getData();
    };

    $scope.EmailChange = function () {
        if ($scope.user.Email !== "" && $scope.HdEmail !== $scope.user.Email) {
            userService.checkUserEmailIsExist($scope.user.Email).then(
                function (response) {
                    $scope.isExist = response.data;
                });
        }
    }

    $scope.serch = function () {
        $scope.getData(1);
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

        $scope.HdEmail = "";
    }

    $scope.editUserModal = function (userId) {
        userService.getUserById(userId).then(function (response) {
            $scope.user = response.data;
            $scope.HdEmail = $scope.user.Email;
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

    $scope.load = function (condition, pageIndex, pageSize, callGetPageBack) {
        if (pageIndex == undefined) {
            pageIndex = 1;
        }
        userService.getUserByCondition(condition, pageIndex, pageSize).then(function (respone) {
            var totalPage = Math.ceil(respone.data.ModelCount / pageSize);
            callGetPageBack && callGetPageBack(respone.data.ModelList, totalPage);
        });
    };
});

