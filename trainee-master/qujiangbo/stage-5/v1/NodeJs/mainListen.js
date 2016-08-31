var http=require("http");
var events = require('events');
var eventEmitter = new events.EventEmitter();


var server=http.createServer(function(req,res){
	res.writeHead(200,{"content-type":"text/html;charset=utf-8"});
	res.end();
});
server.listen(80);



// ������ #1
var listener1 = function listener1() {
   console.log('������ listener1 ִ�С�');
}

// ������ #2
var listener2 = function listener2() {
  console.log('������ listener2 ִ�С�');
}

// �� connection �¼���������Ϊ listener1 
eventEmitter.addListener('connection', listener1);

// �� connection �¼���������Ϊ listener2
eventEmitter.on('connection', listener2);

var eventListeners = require('events').EventEmitter.listenerCount(eventEmitter,'connection');
console.log(eventListeners + " �����������������¼���");

// ���� connection �¼� 
eventEmitter.emit('connection');

// �Ƴ���󶨵� listener1 ����
eventEmitter.removeListener('connection', listener1);
console.log("listener1 �����ܼ�����");

// ���������¼�
eventEmitter.emit('connection');

eventListeners = require('events').EventEmitter.listenerCount(eventEmitter,'connection');
console.log(eventListeners + " �����������������¼���");

console.log("����ִ����ϡ�");