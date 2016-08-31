/**
 * Created by 0.0 on 2015/12/21.
 */
 var app=angular.module('app',[]);

 app.directive('myautofocus',function(){
            return {
                link: function(scope,elements,attrs,controller){
                    console.log(elements[0]);
                    elements[0].focus();
                }
            }
        });

app.config(function($httpProvider){
    console.log($httpProvider.defaults.headers.common);
});

app.value('realname','zhangsan')
    .value('realname','wangwu')//ÊÇ¿ÉÒÔ¸Ä±äÖµµÄ
    .constant('http','www.baidu.com')
    .constant('http','www.jikexueyuan.com')//ÖµÊÇ²»±äµÄ
    .factory('Model',function(){
        return{
            msg:'hello',
        
            setMsg:function(){
                this.msg='hello world.'
            }
        }
    })
    .service('User',function(){
        this.firstName="iPhone";
        this.lastName="6s Plus"
        this.getName=function(){
            return this.firstName+' '+this.lastName;
        }
    })
    .controller('MyCtrl',function($scope){
        $scope.msg="angular";
    })
    .controller('NextCtrl',function($scope){
        $scope.msg="Hello Angular";
    })
    .controller('FirstCtrl',function($scope){
        $scope.msg="Hello FirstCtrol";
    })
    .controller('SecondCtrl',function($scope){
        $scope.msg="Hello SecondCtrl";
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
            console.log($scope.user);//¿ØÖÆÌ¨´òÓ¡ÊäÈë¶ÔÏóÐÅÏ¢
            if($scope.user.uname=='admin' && $scope.user.pwd=='123')
            {
                alert("login success!");
            }
            else{
                $scope.errormsg="login error!"
            }
        }
    })
    //AngularJS ½ø½×- Services ËÄÖÐ´´½¨Ä£Ê½ ÓëÖ¸ÁîµÄÊ¹ÓÃ
    .controller('ServicesCtrl',function($scope,realname,http,Model,User){
        $scope.realname=realname;
        $scope.http=http;
        $scope.model=Model;
        Model.setMsg();
        $scope.user=User.getName();
    })
    //AngularJS ½ø½×- ng-repeat Ö¸ÁîµÄÊ¹ÓÃ
    .controller('AddressCtrl',function($scope){
        $scope.list=[
            {id:'1',address:'shanxixian'},
            {id:'2',address:'shanxichangan'},
            {id:'3',address:'shanxibeijing'},
            {id:'4',address:'shanxinajiang'},
            {id:'5',address:'shanxiyanjing'}
        ];
    })
    //AngularJS ½ø½×- ³£ÓÃÖ¸ÁîµÄÊ¹ÓÃ
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
                //alert($scope.user.aihao.length+','+n+','+$scope.user.aihao[i]);
                if(n==$scope.user.aihao[i]){
                    isCheck=true;
                    break;
                }
            }
            return isCheck;
        }
    })
     .controller('validationController', function($scope) {
         $scope.user = 'Kavlez';
         //$scope.email = '';
     })




     .controller('ExampleController', ['$scope', function ($scope) {

}])
.directive('even',function(){
    return {
        restrict:"A",
        require:"ngModel",
        link:function(scope,ele,attrs,ngModelController){
            ngModelController.$parsers.push(function(viewValue){
                if(viewValue % 2 === 0){
                    ngModelController.$setValidity('even',true);
                }else{
                    ngModelController.$setValidity('even',false);
                }
                return viewValue;
            });
        }
    }
})
.directive('ensureUnique', ['$http','$timeout','$window',function($http,$timeout,$window) {
    return {
        restrict:"A",
        require: 'ngModel',
        link: function(scope, ele, attrs, ngModelController) {
            scope.$watch(attrs.ngModel, function(n) {
                if (!n) return;
                $timeout.cancel($window.timer);
                $window.timer = $timeout(function(){
                    $http({
                        method: 'get',
                        url: '/api/checkusername/', //根据换成自己的url
                        params:{
                            "username":n
                        }
                    }).success(function(data) {
                        ngModelController.$setValidity('unique', data.isUnique); //这个取决于你返回的，其实就是返回一个是否正确的字段，具体的这块可以自己修改根据自己的项目
                    }).error(function(data) {
                        ngModelController.$setValidity('unique', false);
                    });
                },500);
            });
        }
    };
}])
.controller('bindValCtrl',function($scope){
    $scope.students={name:'',link:''}
    $scope.students.name='hehe123';
    $scope.students.link='http://wwww.baidu.com';

})
.controller('factorialResult',function($scope){
    $scope.factorial={};
    $scope.factorial.number=0;
    $scope.factorial.result=1;
    $scope.factorialNum=function(num){
        if(num==0){
            return 1;
        }
        else{
            return num*$scope.factorialNum(--num);
        }
    }
    $scope.compute=function(){
        //console.log($scope.factorial.number);
        $scope.factorial.result=$scope.factorialNum($scope.factorial.number);
        //console.log($scope.factorial.result);
    }
    $scope.$watch('factorial.number',$scope.compute);
    $scope.reset=function(){
        $scope.factorial.number=0;
        $scope.factorial.result=1;
    }
})
.controller('NavController',function($scope){
    $scope.dosomething=function(){
        console.log('aaa');
    }
})
.controller('ContentController',function($scope){
    $scope.dosomething=function(){
        console.log('bbb');
    }
})
.controller('StudentController',function($scope){
    $scope.students=[{"name":"code_bunny","score":"100","id":"001"},{"name":"white_bunny","score":"90","id":"002"},{"name":"black_bunny","score":"80","id":"003"}];
    $scope.insertDog=function(){
        $scope.students.splice($scope.students.length,0,{"name":"code_dog","score":"101","id":"004"});
    };
    $scope.del=function(){
        $scope.students.splice(-1,1);
    };
    $scope.choose = function(i){
        $scope.selRow = i
    };
})
.controller("focusController",function($scope){
    $scope.text="没有点击任何按钮";

    $scope.nofocus=function(){
        $scope.text="没有点击任何按钮";
    };

    $scope.hasfocus=function(){
        $scope.text="点击了聚焦按钮";
    }

})