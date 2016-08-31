function signinCheck(form) {

           if(form.email.value=='') {     		

                alert("pelease input email!");
                form.email.focus();
                return false;
           }
           else
           {
           		var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
           		if (!filter.test(form.email.value)) {
			 		alert('Your email is wrong!');
			 		return false;
		 		}
           }
           if(form.password.value=='')
	       {
	       		alert("pelease input Password!");
	            form.password.focus();
	            return false;
	        }
	       alert("Login sucess!");
}
