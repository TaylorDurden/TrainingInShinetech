
namespace BugManagemnet.WebAPI.Models
{
    public class RegisterViewModel
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepartPassword { get; set; }
        public bool ReadAccept { get; set; }
    }
}