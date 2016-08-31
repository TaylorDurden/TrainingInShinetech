function bugsCheck() {
  var titles=$("#titles").val();
  var summary=$("#summary").val();
  var description=$("#description").val();
  if(titles=='') {
    $("#signinhint").text('pelease input titles!');
    $("#titles").focus();
    return false;
   }
   if(summary==''){
      $("#signinhint").text('pelease input summary!');
      $("#summary").focus();
      return false;
    }  
   if(description=='')
    {
      $("#signinhint").text('pelease input description!');
      $("#description").focus();
      return false;
    } 
    $("#signinhint").text('Create bug success!');    
}
