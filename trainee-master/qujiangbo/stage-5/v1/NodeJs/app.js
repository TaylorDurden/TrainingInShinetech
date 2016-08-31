var http=require("http");
//var url=require("url");

var server=http.createServer(function(req,res){
	res.writeHead(200,{"content-type":"text/html;charset=utf-8"});

	res.write("<h1>Hello NodeJs,code 0.0 node demo</h1>");

	res.end("hello everyone ÄãºÃ \n");
});
server.listen(80);