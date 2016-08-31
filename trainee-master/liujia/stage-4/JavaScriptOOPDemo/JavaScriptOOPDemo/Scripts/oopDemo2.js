//javascript 面向对象编程：构造函数的继承(五种方法)

function Animal() {
    this.species = "动物";
}

//构造函数绑定 使用call或apply方法，将父对象的构造函数绑定在子对象上
function Cat(name, color) {
    Animal.apply(this, arguments);
    this.name = name;
    this.color = color;
}
//prototype模式
Cat.prototype = new Animal();
Cat.prototype.constructor = Cat;
var cat1 = new Cat("大毛", "黑色");
alert(cat1.species);//动物
//直接继承prototype
function Animal1() {
    Animal1.prototype.species = "动物";
}
Cat.prototype = Animal1.prototype;
Cat.prototype.constructor = Cat;
var cat2 = new Cat("小毛", "灰色");
alert(cat2.species);//动物
//利用空对象作为中介
function extend(Child, Parent) {
    var F = function () { };
    F.prototype = Parent.prototype;
    Child.prototype = new F();
    Child.prototype.constructor = Child;
    Child.uber = Parent.prototype;
}

extend(Cat, Animal1);
var cat4 = new Cat("三毛", "白色");
alert(cat4.species);//动物
//拷贝继承
function Animal2() { }
Animal2.prototype.species = "动物";
function extend2(Child, Parent) {
    var p = Parent.prototype;
    var c = Child.prototype;
    for(var i in p){
        c[i] = p[i];
    }
    c.uber = p;
}

extend2(Cat, Animal2);
var cat5 = new Cat("五毛", "黄色");
alert(cat5.species);//动物