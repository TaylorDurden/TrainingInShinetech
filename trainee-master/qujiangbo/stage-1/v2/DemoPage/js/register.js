function registerCheck(form) {
          if(form.Firstname.value=='') {
                alert("pelease input Firstname!");
                form.Firstname.focus();
                return false;
           }
           if(form.Lastname.value=='') {
                alert("pelease input Lastname!");
                form.Lastname.focus();
                return false;
           }
           if(form.Email.value=='') {     		

                alert("pelease input Email!");
                form.Email.focus();
                return false;
           }
           else
           {
           		var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
           		if (!filter.test(form.Email.value)) {
			 		alert('Your email is wrong!');
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
	       		alert("pelease input Password!");
	            form.Password.focus();
	            return false;
	        }

	       if(form.RepartPassword.value==''){
	            alert("pelease input RepartPassword!");
	            form.RepartPassword.focus();
	            return false;
	        }
	        if(form.Password.value!='' && form.RepartPassword.value!='')
	        {
	        	if(form.Password.value!=form.RepartPassword.value)
	        	{
	        		alert("Password and RepartPassword is different!");
	                form.RepartPassword.focus();
	            	return false;
	        	}
	        }
	        alert("Create sucess!");
}
