$(document).ready(function () {
  $(".btn").on("click", function() {
        var email=$("#email").val();
        var password=$("#password").val();
        if(email==''){
          $("#signinhint").text('Pelease input email!');
          $("#email").focus();
          return false;
        }
        else{
          var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
          if (!filter.test(email)) {
            $("#signinhint").text('Your email is wrong!');
            $("#email").focus();
            return false;
          }
        }
        if(password=='')
         {
            $("#signinhint").text('Pelease input Password!');
            $("#password").focus();
            return false;
        }
        alert("Login sucess!");
    }); 
});