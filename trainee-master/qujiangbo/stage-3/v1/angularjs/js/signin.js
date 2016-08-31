angular.module('app',[])
    .controller('MyCtrl',function($scope){
        $scope.msg="angular";
    })
    .controller('ThirdCtrl',function($scope){
        $scope.msg="";
        $scope.user={uname:'',pwd:''};
        $scope.errormsg="";
        $scope.reverse=function(){
            //return $scope.msg.split("").reverse().join();
            return $scope.msg.split("").reverse().join("");
        }
        $scope.login=function(){
            console.log($scope.user);
            if($scope.user.uname=='admin' && $scope.user.pwd=='123')
            {
                console.log("login success!");
            }
            else{
                $scope.errormsg="login error!"
            }
        }
    })
    .controller('AddressCtrl',function($scope){
        $scope.list=[
            {id:'1',address:'shanxixian'},
            {id:'2',address:'shanxichangan'},
            {id:'3',address:'shanxibeijing'},
            {id:'4',address:'shanxinajiang'},
            {id:'5',address:'shanxiyanjing'}
        ];
    })
    .controller('NormalCtrl',function($scope){
        $scope.user={
            isshow:false,
            showicon:true,
            icon:'../image/demo.jpg',
            uname:'zhangsan',
            pwd:'',
            zw:'1',
            sex:'0',
            aihao:[1,2]
        }
        $scope.isChecked=function(n){
            var isCheck=false;
            for(var i=0;i<$scope.user.aihao.length;i++){
                if(n==$scope.user.aihao[i]){
                    isCheck=true;
                    break;
                }
            }
            return isCheck;
        }
    })
    .controller('selectController', function ($scope) {
        $scope.mycity = '北京';
        $scope.Cities = [{ id: 1, name: '北京' }, { id: 2, name: '上海' }, { id: 3, name: '广州' }];
    })
    .controller('TestCtrl',function($scope){
        $scope.see=function(){
        //console.log($scope.test_form.$error);            
        console.log($scope.test_form);
        console.log($scope.test_form.name);
        }
    })
    /*.controller('myController', function($scope,$http)
    {
        var postData={PageIndex: 0,PageSize: 10};

        var req = {
         method: 'POST',
         url: 'http://123.57.252.60:8008/V1/Hotel/GetAllHouse',
         headers: {
           'Authorization':'basic weixin|363F55B8-203E-4BE4-BB76-AAF7F99FF369'
         },
         data: postData,
        }

        $http(req).success(function(data){
           console.log(data.Data.List[0].Title);
           $scope.Data1=data;
        })
        .error(function(data){
           console.log(data.List[0].Title);
           $scope.Data1=data;
        });
    }*/
     .controller('validationController', function($scope) {
         $scope.user = 'Kavlez';
         //$scope.email = '';
     })
     .controller('ExampleController', ['$scope', function ($scope) {

}])
     /*.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val()===$(firstPassword).val();
                    ctrl.$setValidity('pwmatch', v);
                });
            });
        }
    }
}])*/



     .controller("pageController",['$scope',function($scope,$http){
        var postData={PageIndex: 0,PageSize: 10};
        $http({
            method:'Get',
            url:'http://123.57.252.60:8008/V1/Hotel/GetAllHouse',
            headers:{'Authorization':'basic weixin|363F55B8-203E-4BE4-BB76-AAF7F99FF369'}
        }).success(function(data){
            $scope.list=data;
        });
        /*var req = {
         method: 'POST',
         url: 'http://123.57.252.60:8008/V1/Hotel/GetAllHouse',
         headers: {
           'Authorization':'basic weixin|363F55B8-203E-4BE4-BB76-AAF7F99FF369'
         }
         //,data: postData,
        }

        $http(req).success(function(data){
           //console.log(data.Data.List[0].Title);
           $scope.list=data;
        })
        .error(function(data){
            console.log('error');
            console.log(data.List[0].Title);
            $scope.list=data;
        });*/

        $scope.curPage=0;
        $scope.pageSize=10;
        $scope.list=messageData.rspPublicArgs.rspArgs;
        $scope.pageCount=Math.ceil($scope.list.length/$scope.pageSize)-1
    }])
    .filter('pageStartFrom',[function(){
        return function(input,start){
            start=+start;
            return input.slice(start);
        }
    }])
