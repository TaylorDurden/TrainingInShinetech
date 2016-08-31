/**
 * This file provided by Facebook is for non-commercial testing and evaluation
 * purposes only. Facebook reserves all rights not expressly granted.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * FACEBOOK BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
 * ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
        //内容组件
         var Content = React.createClass({

           getInitialState:function(){
             return {
               inputText:'',
             };
           },

           handleChange:function(event){
             this.setState({inputText:event.target.value});
           },

           handleClick:function(){
             console.log("props name is " + this.props.selectName + " \n and inputText is "  + this.state.inputText);
           },

           render:function(){
             return (<div>
               <textarea onChange = {this.handleChange} placeholder = "please input something!"></textarea>
               <button onClick={this.handleClick}>sumbit</button>
             </div>);
           }
         });

         //评论组件
         var Comment = React.createClass({

           getInitialState:function(){
            return {
               names:["Tom","Axiba","daomul"],
               selectName:'',
             };
           },

           handleSelect:function(){
             this.setState(
                 {selectName : event.target.value}
               );
           },

           render:function(){
             var options = [];

             for (var option in this.state.names) {
               options.push(<option value={this.state.names[option]}> {this.state.names[option]}  </option>);
             };

             return <div>
               <Content selectName = {this.state.selectName}>
               </Content>
               <select onChange = {this.handleSelect}>
                 {options}
               </select>
             </div>;
           }
         });

         //start render
         React.render(<Comment />,document.getElementById('containerComment'));
