namespace PlanPoker.ILogic.Models
{
    public class UserLogicModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
