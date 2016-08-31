//javascript 面向对象编程：封装  主要介绍如何"封装"数据和方法，以及如何从原型对象生成实例

var cat = {
    name: '',
    color: ''
};

var cat1 = {};
cat1.name = "大毛";
cat1.color = "黑色";

function Cat(name, color) {
    this.name = name;
    this.color = color;    
}

Cat.prototype.type = "猫科动物";
Cat.prototype.eat = function () { alert("吃老鼠")};


var cat2 = new Cat("小毛","白色");
var cat3 = new Cat("三毛", "灰色");
//cat3.eat();

//alert(cat2.eat == cat3.eat);//false 指向的地址不同,所以为false

//alert(cat2.constructor == Cat);//true constructor属性指向它们的构造函数

//alert(cat2 instanceof Cat);//true instanceof运算符，验证原型对象与实例对象之间的关系

//alert(Cat.prototype.isPrototypeOf(cat3));//true isPrototypeOf判断某个proptotype对象和某个实例之间的关系

//alert(cat3.hasOwnProperty("name"));//true name是本地属性 
//alert(cat3.hasOwnProperty("type"));//false type是继承自prototype对象的属性   hasOwnProperty判断一个属性是不是本地属性

//alert("name" in cat3);//true
//alert("type" in cat3);//true in运算符判断某个实例是否包含该属性，不管该属性是不是本地属性

//for (var prop in cat3) {
//    alert(cat3[prop]);
//}//可以用in运算符遍历某个对象的所有属性 