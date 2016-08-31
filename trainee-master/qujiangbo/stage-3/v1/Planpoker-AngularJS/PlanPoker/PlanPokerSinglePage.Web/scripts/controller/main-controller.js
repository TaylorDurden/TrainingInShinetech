// controller for the index page
appModule.controller('mainController', function ($scope, $location) {
    $scope.removeCookie = function () {
        Cookies.remove(User_Name_Cookie);
        Cookies.remove(User_Id_Cookie);
        Cookies.remove(Project_Id_Cookie);
        Cookies.remove(Presenter_Cookie);
        $location.path('/login');
    }
})