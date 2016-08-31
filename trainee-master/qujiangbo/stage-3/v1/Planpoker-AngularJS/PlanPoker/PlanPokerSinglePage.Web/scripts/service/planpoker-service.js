appModule.service('userLogin', function ($location) {
    this.loginCheck = function () {
        if (Cookies(User_Name_Cookie) == undefined) {
            $location.path('/login');
        }
    }
});
