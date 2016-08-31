var myApp=angular.module('app',['ngCookies']);
myApp.controller('DemoController',function($cookies, $scope){

    $cookieStore.put("name", "my name");
    $cookieStore.get("name") == "my name";
    $cookieStore.remove("name");

    $cookieStore.put("persion", {
        name: "my name",
        age: 18
    });

    $scope.person = $cookieStore.get("persion");
})
