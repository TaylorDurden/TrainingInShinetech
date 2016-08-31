$(document).ready(function () {
    $('#userPicture').on('click', function () {
        $('#imgUpload').click();
    });
    $('#imgUpload').change(function () {
        $('#userPicture').attr('src', '../upload/user.png');
        $('#userPictureError').addClass('hide');
        $('#userPictureError').text('');
        readURL(this);
    });
});

mainApp.controller('registerController', ['$scope', '$http', 'userService', function ($scope, $http, userService) {

    $scope.user = {
        isExist: false
    };

    $scope.oncreateUser = function () {
        userService.createUser($scope);
    }

    $scope.onUserNamechange = function () {
        userService.userNameChange($scope);
    }

}]);

