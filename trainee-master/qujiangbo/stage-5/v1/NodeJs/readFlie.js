var iconv=require("iconv-lite");

var fs=require("fs");
/*
fs.readFile('inputFile.txt','utf-8',function(err,data){
	if(err){
		console.log(err.stack);
		return;
}
	console.log(data.toString());
});

	console.log("program over");
var data=fs.readFileSync('inputFile.txt','utf-8');
console.log(data);
*/
var data=fs.readFileSync('inputFile.txt',{encoding:'binary'});
console.log(data);
var buf=new Buffer(data,'binary');
console.log(buf);
var str=iconv.decode(buf,'GBK');

console.log(str);
console.log("program over");