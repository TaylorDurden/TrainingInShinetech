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


