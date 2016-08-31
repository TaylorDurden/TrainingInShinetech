/*React.render(
        <h1>Hello world!</h1>,
        document.getElementById('example')
        );
*/
var CommentBox=React.createClass({
	render:function(){
		return (
			<div className="commentBox">
				Hellow, world!I am a CommentBox.
				<h1>Comments</h1>
				
				
				<CommentList data={this.props.data} />
				<CommentForm/>
			</div>
		);
	}
});
var CommentList=React.createClass(
{
	render:function(){
		return(
			<div className="commentList">
				Hello, world! I am a CommentList.
				<Comment author="Pete Hunt">This is one comment</Comment>
				<Comment author="Jordan Walke">This is *another* comment</Comment>
			</div>
		);
	}
});

var CommentForm=React.createClass({
	render:function(){
		return (
			<div className="commentForm">
				Hello, world! I am a CommentForm.
			</div>
		);
	}
});
//让我们转换评论文本为 Markdown 格式
var converter=new Showdown.converter();

var Comment=React.createClass({
	render:function(){
		var rawMarkup = converter.makeHtml(this.props.children.toString());
	    return (
	      <div className="comment">
	        <h2 className="commentAuthor">
	          {this.props.author}
	        </h2>
	        <span dangerouslySetInnerHTML={{__html: rawMarkup}} />
	      </div>
	    );
	}

});


var data = [
  {author: "Pete Hunt", text: "This is one comment"},
  {author: "Jordan Walke", text: "This is *another* comment"}
];
React.render(<CommentBox/>,document.getElementById('example'));