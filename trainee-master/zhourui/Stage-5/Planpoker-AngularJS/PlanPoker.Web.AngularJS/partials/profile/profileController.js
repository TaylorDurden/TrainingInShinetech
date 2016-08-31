app.controller("profileController", function($scope, $location, $cookieStore, userService, projectService) {
    checkCookie($cookieStore, $location);

    $scope.user = {};

    var originalPassword = "";

    userService.getUserById($cookieStore.get("Cookie_User_Id"))
        .success(function(returnObject) {
            $scope.user.username = returnObject.UserName;
            $scope.project.Owner = returnObject;
            $scope.user.email = returnObject.Email;
            $scope.user.image = returnObject.Image;
            originalPassword = returnObject.Password;
            console.log("Get user profile success!");
        })
        .error(function(errorMessage) { console.log(errorMessage); });

    $scope.changeImage = function() { $scope.user.image = $scope.image; };

    $scope.updateProfile = function() {
        if ($scope.oldpassword === originalPassword) {
            $scope.user.userid = $cookieStore.get("Cookie_User_Id");
            $scope.user.password = $scope.newpassword;
            if ($scope.image !== undefined) {
                $scope.user.image = $scope.image;
            }
            userService.updateUser($scope.user)
                .success(function(returnObject) {
                    if (returnObject.Message) {
                        $scope.updateErrorTips = returnObject.Message;
                        return;
                    }

                    $location.path("/dashboard");
                    console.log("Update user profile success!");
                })
                .error(function(errorMessage) { console.log(errorMessage); });
        } else {
            $scope.updateErrorTips = "the old password is wrong, please check it and input again.";
        }
    };

    $scope.project = {};
    

    $scope.selectedUserIds = [];
    $scope.selectedUserNames = [];
    $scope.selectedUsers = [{}];
    $scope.users = [{}];
    userService.getAllUser()
        .success(function(returnObject) {
            $scope.users = returnObject;
        });

    var updateSelected = function(action, id, name) {
        if (action === "add" && $scope.selectedUserIds.indexOf(id) === -1) {
            $scope.selectedUserIds.push(id);
            $scope.selectedUserNames.push(name);
        }
        if (action === "remove" && $scope.selectedUserIds.indexOf(id) !== -1) {
            var idx = $scope.selectedUserIds.indexOf(id);
            $scope.selectedUserIds.splice(idx, 1);
            $scope.selectedUserNames.splice(idx, 1);
        }
    }
    $scope.updateSelection = function($event, id) {
        var checkbox = $event.target;
        var action = (checkbox.checked ? "add" : "remove");
        updateSelected(action, id, checkbox.name);
    }
    $scope.isSelected = function(id) {
        return $scope.selectedUserIds.indexOf(id) >= 0;
    }

    $scope.createProject = function() {
        $scope.project.RelatedUsers = [];
        angular.forEach($scope.selectedUserNames, function(username) {
            angular.forEach($scope.users, function(user) {
                if (user.UserName === username) $scope.project.RelatedUsers.push(user);
            });
        });

        projectService.createProject($scope.project)
            .success(function(returnObject) {
                if (returnObject.Message) {
                    $scope.createProjectErrorTips = "project name already exists!";
                    return;
                }

                angular.element("#modalCreateProject").modal("hide");
                $location.path("/dashboard");
                console.log("Create project success!");
            })
            .error(function(errorMessage) { console.log(errorMessage); });
    };
});