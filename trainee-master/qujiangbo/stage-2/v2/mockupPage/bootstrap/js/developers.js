function developersCheck() {
  var firstname=$("#firstname").val();
  var lastname=$("#lastname").val();
  var email=$("#email").val();

  if(firstname=='') {
    $("#signinhint").text('pelease input firstname!');
    $("#firstname").focus();
    return false;
   }
   if(lastname=='')
    {
      $("#signinhint").text('pelease input lastname!');
      $("#lastname").focus();
      return false;
    }
   if(email=='') {  
      $("#signinhint").text('pelease input email!');
      $("#email").focus();
      return false;
     }
     else
     {
        var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
        $("#signinhint").text('Your email is wrong!');
        $("#email").focus();
        return false;
      }
    }
  $("#signinhint").text('Create new developer sucess!');
}

