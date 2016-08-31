function registerValidCheck(form) {
          if(form.FirstName.value=='') {
                alert("FirstName can not be empty!");
                form.FirstName.focus();
                return false;
           }
           if(form.LastName.value=='') {
                alert("LirstName can not be empty!");
                form.LastName.focus();
                return false;
           }
           if(form.Email.value=='') {     		

                alert("Email can not be empty!");
                form.Email.focus();
                return false;
           }
           else
           {
           		var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
           		if (!filter.test(form.Email.value)) {
			 		alert('Your email format is wrong!!');
			 		form.Email.focus();
			 		return false;
		 		}
			 	/*if (filter.test(form.Email.value)) 
			 		return true;
			 	else {
			 		alert('Your email is wrong!');
			 		return false;
		 		}*/
           }

	       if(form.Password.value=='')
	       {
	       		alert("Password can not be empty!");
	            form.Password.focus();
	            return false;
	        }

	       if(form.RepartPassword.value==''){
	            alert("RepartPassword can not be empty!");
	            form.RepartPassword.focus();
	            return false;
	        }
	        if(form.Password.value!='' && form.RepartPassword.value!='')
	        {
	        	if(form.Password.value!=form.RepartPassword.value)
	        	{
	        		alert("Password is different from RepartPassword!");
	                form.RepartPassword.focus();
	            	return false;
	        	}
	        }
	        alert("Create sucess!");
}
