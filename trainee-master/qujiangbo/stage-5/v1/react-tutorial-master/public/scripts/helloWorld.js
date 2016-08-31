      var Hello=React.createClass(
      {

          getInitialState:function()
          {
              //alert('init');
              return {
                  opacity:1.0,
                  color:'red',
                  fontSize:"12px"
              };
          },

          render:function()
          {
              /*return <div style={{color:'red'}} >Hello {this.props.title} {this.props.name}</div>;*/
              return <div style={{opacity:this.state.opacity,color:this.state.color,fontSize:this.state.fontSize}}>Hello {this.props.title} {this.props.name}
            </div>;
          },


          componentWillMount:function()
          {
              //alert('will');
          },

          componentDidMount:function()
          {
              //alert('did');
              window.setTimeout(function(){
                  this.setState({
                      opacity:0.5,
                      color:'green',
                      fontSize:'54px'
                  });
              }.bind(this),1000);
          },

      });
      React.render(<Hello name="World" title="Mr"/>, document.getElementById('containerHello'));
