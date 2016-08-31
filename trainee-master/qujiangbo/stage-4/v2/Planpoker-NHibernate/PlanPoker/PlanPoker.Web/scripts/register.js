planPokerApp().directive('pwCheck', [function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    console.log($(firstPassword).val()+'------'+elem.val());
                    var result = elem.val()===$(firstPassword).val();
                    ctrl.$setValidity('passwordmatch', result);
                });
            });
        }
    }
}])
    .controller('signinupCtrl', function ($scope, $http) {
        $scope.users = { username: "", password: "", confirmPassword: "", email: "", image: "", isexist:false,message:''};
        $('#userPicture').on('click', function () {
            $('#imgUpload').click();
        });
        $('#imgUpload').change(function () {
            $('#userPicture').attr('src', '../upload/user.png');
            $('#userPictureError').addClass('hide');
            $('#userPictureError').text('');
            readUrl(this);
        });

        function readUrl(input) {
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
                    return false;
                }
                reader.readAsDataURL(input.files[0]);
            }
            return false;
        }
        $scope.signinupSubmit = function () {
            $http({
                method: 'POST',
                url: apiPath() + "/api/user",
                data: $scope.users,
                headers: { 'Content-Type': 'application/json' }
            }).success(function(response) {
                console.log(response);
                if (response === '' || response == null) {
                    console.log("ok");
                    location.href = 'Login.html';
                } else {
                    $scope.users.message = response;
                    $scope.users.isexist = true;
                }
            }).error(function() {
                console.log("error");
            });
        }
    })



