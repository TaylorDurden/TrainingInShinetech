app.controller("userController", function($scope, userService) {
    $scope.user = {
        userid: 0,
        username: "",
        password: "",
        email: "",
        image: ""
    };
    $scope.image = "../../upload/user.png";

    $scope.login = function() {
        var promiseLogin = userService.login($scope.user.username, $scope.user.password);
        promiseLogin.then(
            function() {
                console.log("Success!");
                location.href = "Dashboard.html";
            },
            function() { console.log("Error!"); }
        );
    };

    $scope.clearControls = function() {
        $scope.User.clear();
    };
    $scope.create = function() {
        var promisePost = userService.createUser($scope.user);
        promisePost.then(function(ret) {
            $scope.user = ret.data;
            $scope.clearControls();
        }, function(err) {
            console.log(err);
        });
    };
    $scope.delete = function() {
        var promiseGetSingle = userService.getUserByName($scope.user.username);

        promiseGetSingle.then(function(ret) {
                var promiseDelete = userService.deleteUser(ret.data.UserId);
                promiseDelete.then(function(ret) {
                    $scope.Message = "User Deleted Successfuly";
                    clearControls();
                }, function(err) {
                    console.log(err);
                });
            },
            function(err) {
                console.log(err);
            });
    };
    $scope.update = function() {
        var promiseGetSingle = userService.getUserByName($scope.user.username);

        promiseGetSingle.then(function(ret) {
                $scope.user.userid = ret.data.UserId;
                var promiseUpdate = userService.updateUser($scope.user);
                promiseUpdate.then(function(ret) {
                    $scope.Message = "User Updated Successfuly";
                    clearControls();
                }, function(err) {
                    console.log(err);
                });
            },
            function(err) {
                console.log(err);
            });
    };
    $scope.getByUserId = function(userid) {
        var promiseGetSingle = userService.getUserById(userid);

        promiseGetSingle.then(function(ret) {
                $scope.user = ret.data;
            },
            function(err) {
                console.log(err);
            });
    };
    $scope.getByUsername = function() {
        var promiseGetSingle = userService.getUserByName($scope.user.username);

        promiseGetSingle.then(function(ret) {
                $scope.user = ret.data;
                $scope.user.userid = ret.data.UserId;
                $scope.user.username = ret.data.UserName;
                $scope.user.password = ret.data.Password;
                $scope.user.email = ret.data.Email;
                $scope.user.image = ret.data.Image;
            },
            function(err) {
                console.log(err);
            });
    };
});