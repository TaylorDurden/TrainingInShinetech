$(document).ready(function () {
  $(".btn").on("click", function() {
        var firstName=$("#firstName").val();
        var lastName=$("#lastName").val();
        var email=$("#email").val();
        var password=$("#password").val();
        var repartPassword=$("#repartPassword").val();       


        if(firstName=='') {
        	$("#signinhint").text('pelease input firstname!');
          	$("#firstName").focus();
          	return false;
        }
       if(lastName=='') {
       		$("#signinhint").text('pelease input lastname!');
          	$("#lastName").focus();
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

       if(password==''){
       		$("#signinhint").text('pelease input password!');
       		$("#password").focus();
       		return false;
       }
       if(repartPassword==''){
            $("#signinhint").text('pelease input repartpassword!');
            $("#repartPassword").focus();
       		return false;
        }
        if(password!='' && repartPassword!='')
        {
        	if(password!=repartPassword)
        	{
        		$("#signinhint").text('Password and repartpassword is different!');
        		$("#repartPassword").focus();
            	return false;
        	}
        }
        alert("Create sucess!");
    }); 
});