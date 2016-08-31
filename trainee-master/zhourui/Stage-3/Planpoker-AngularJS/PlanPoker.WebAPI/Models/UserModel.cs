namespace PlanPoker.WebAPI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

        public string Status { get; set; }
        public string Message { get; set; }
    }
}