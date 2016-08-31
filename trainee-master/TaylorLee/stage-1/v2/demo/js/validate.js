function validate_required(field, alerttext)
{
	with (field)
	{
		if (value==null||value=="") 
		{
			alert(alerttext);
			return false;
		}
		else
		{
			return true;
		}
	}
}

function validate_email(field, alerttext)
{
	with (field)
	{
		var regex = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
		if (!regex.test(value)) 
		{
			alert(alerttext);
			return	false;
		}
		else
		{
			return true;
		}
	}
}

function confirm_deleting()
{
	confirm("Delete it?");
}


function validate_form(thisform)
{
	with (thisform)
	{
		if (!validate_required(email, "Email must be filled out!")) 
		{
			email.focus();
			return false;
		}
		if (!validate_email(email, "Not a valid e-mail address!")) 
		{
			email.focus();
			return false;
		}
		if (!validate_required(password, "password must be filled out!"))
		{
			password.focus();
			return false;
		}
		if (!validate_required(firstName, "FirstName must be filled out!"))
		{
			firstName.focus();
			return false;
		}
		if (!validate_required(lastName, "LastName must be filled out!"))
		{
			lastName.focus();
			return false;
		}
		if (!validate_required(confirmPassword, "ConfirmPassword must be filled out!"))
		{
			confirmPassword.focus();
			return false;
		}
	}

}