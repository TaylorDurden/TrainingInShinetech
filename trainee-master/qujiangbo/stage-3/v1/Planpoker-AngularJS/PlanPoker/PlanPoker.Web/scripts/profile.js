planPokerApp().service('userLogin', function () {
    this.loginCheck = function () {
        if (Cookies(User_Name_Cookie) == undefined) {
            location.href = "Login.html";
        }
    }
})
.controller('updateController', function ($scope, $http, userLogin) {
    userLogin.loginCheck();
    var oldpassword = "";
    $scope.users = { userId: '', username: "", password: "", confirmPassword: "", email: "", image: "",isStatus:false,message:'' };
    $scope.users.userId = getQueryString(User_Id_Cookie);
    $http.get(apiPath() + "/api/user/" + $scope.users.userId).success(function (response) {
        $scope.users.username = response.UserName;
        $scope.users.email = response.Email;
        if (response.Image!=null) {
            $scope.users.image = response.Image;
        }
        else {
            $scope.users.image = '..\\upload\\user.png';
        }
        oldpassword = response.Password;
    })

    $('#userPicture').on('click', function () {
        $('#imgUpload').click();
    });
    $('#imgUpload').change(function () {
        $('#userPicture').attr('src', '../upload/user.png');
        $('#userPictureError').addClass('hide');
        $('#userPictureError').text('');
        readURL(this);
    });

    function readURL(input) {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|JPEG|PNG)$/.test(input.files[0].name)) {
            $('#userPictureError').addClass('show');
            $('#userPictureError').text('The style of picture must be in .gif,jpeg,jpg,png');
            return false;
        }
        if (input.files[0].size > 1024 * 600 || input.files[0].size < 1024 * 5) {
            $('#userPictureError').addClass('show');
            $('#userPictureError').text('The size of file between 5kb and 600kb!');
            return false;
        }
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var img = document.createElement('img');
                document.body.insertAdjacentElement('beforeEnd', img);
                img.style.visibility = 'hidden';
                img.src = e.target.result;
                var imgwidth = img.offsetWidth;
                var imgheight = img.offsetHeight;
                if (imgwidth > 400 || imgheight > 400) {
                    $('#userPictureError').addClass('show');
                    $('#userPictureError').text('The width or the heigth  must be not more than 400px!');
                    return false;
                }
                $('#userPicture').attr('src', e.target.result);
                $scope.users.image = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    $scope.updateSubmit = function () {
        if ($scope.users.oldpassword == oldpassword) {
            $http({
                method: 'Put',
                url: apiPath() + "/api/user",
                data: $scope.users
            }).success(function () {
                $scope.users.message = 'update sucess';
                $scope.users.isStatus = true;
            }).error(function () {
                console.log("error");
            })
        } else {
            $scope.users.message = 'old password is wrong';
            $scope.users.isStatus = true;
        }
    }

})
.controller('navController', function ($scope, $http) {
    $scope.removeCookie = function () {
        Cookies.remove(User_Name_Cookie);
        Cookies.remove(User_Id_Cookie);
        Cookies.remove(Project_Id_Cookie);
        Cookies.remove(Presenter_Cookie);
        location.href = "Login.html";
    }
})
.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var result = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('passwordmatch', result);
                });
            });
        }
    }
}])