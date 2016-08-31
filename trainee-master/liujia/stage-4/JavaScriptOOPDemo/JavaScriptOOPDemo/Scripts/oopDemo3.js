//非构造函数的继承
//object()方法
var Chinese = { nation: '中国',birthPlaces : ['北京','上海','香港'] }
var Doctor = { career: '医生' }

function object(o) {
    function F() { }
    F.prototype = o;
    return F;
}

var doctor = object(Chinese);
doctor.career = '医生';
alert(doctor.prototype.nation);
//浅拷贝 拷贝基本类型的数据
function extendCopy(p) {
    var c = {};
    for (var i in p) {
        c[i] = p[i];
    }
    c.uber = p;
    return c;
}
var doctor1 = extendCopy(Chinese);
doctor1.career = '医生';
alert(doctor1.nation);

doctor1.birthPlaces.push('厦门');

alert(doctor1.birthPlaces);
alert(Chinese.birthPlaces);
//深拷贝 能够实现真正意义上的数组和对象的拷贝
function deepCopy(p,c) {
    var c = c || {};
    for (var i in p) {
        if (typeof p[i] === 'object') {
            c[i] = (p[i].constructor === Array) ? [] : {};
            deepCopy(p[i], c[i]);
        } else {
            c[i] = p[i];
        }
    }

    return c;
}

Chinese.birthPlaces = ['北京', '上海', '香港'];
var doctor2 = deepCopy(Chinese);

doctor2.birthPlaces.push('厦门');

alert(doctor2.birthPlaces);//北京, 上海, 香港, 厦门
alert(Chinese.birthPlaces);//北京, 上海, 香港