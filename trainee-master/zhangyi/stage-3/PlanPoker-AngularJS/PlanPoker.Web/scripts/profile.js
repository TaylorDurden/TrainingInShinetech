mainApp.controller('profileController', ['$scope', '$http', 'checkUserProvider', 'userService', function ($scope, $http, checkUserProvider, userService) {
    checkUserProvider.checkUser();
    var userId = Cookies(User_Id_Cookie);
    $scope.user = {
        UserName: '',
        Email: '',
        Image: '',
        Password: '',
        UserId: userId
    };
    
    $scope.onProfileInit = function () {
        userService.userProfileInit($scope,userId);
    };

    $scope.onmodify = function () {
        userService.userModify($scope);
    }

    $scope.redirectToDashboard= function() {
        redirectToDashboard();
    }

}]);

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