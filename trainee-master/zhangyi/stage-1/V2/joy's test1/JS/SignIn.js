function SignInValidCheck(form) {

           if(form.email.value=='') {     		

                alert("Email can not be empty!");
                form.email.focus();
                return false;
           }
           else
           {
           		var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
           		if (!filter.test(form.email.value)) {
			 		alert('Your email format is wrong!');
          form.email.focus();
			 		return false;
		 		}
           }
           if(form.password.value=='')
	       {
	       		alert("Password can not be empty!");
	            form.password.focus();
	            return false;
	        }
	       alert("Login sucess!");
}
