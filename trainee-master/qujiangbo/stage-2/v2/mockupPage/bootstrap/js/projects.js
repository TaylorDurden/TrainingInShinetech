function projectsCheck() {
  var name=$("#name").val();
  var description=$("#description").val();
  var maincontact=$("#maincontact").val();
  var email=$("#email").val();
  var starttime=$("#starttime").val();

  if(name=='') {
    $("#signinhint").text('pelease input name!');
    $("#name").focus();
    return false;
   }
   if(description=='')
    {
      $("#signinhint").text('pelease input description!');
      $("#description").focus();
      return false;
    }
   if(maincontact==''){
      $("#signinhint").text('pelease input maincontact!');
      $("#maincontact").focus();
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
 if(starttime=='')
  {
      $("#signinhint").text('pelease input Start Time!');
      $("#starttime").focus();
      return false;
  }
  $("#signinhint").text('Create project success!');
}
