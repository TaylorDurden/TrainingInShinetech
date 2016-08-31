using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.LogicModel
{
    public class UserLogicModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

        //public PlanPokerUser ChangeToUserEntity()
        //{
        //    PlanPokerUser user = new PlanPokerUser
        //    {
        //        UserId = UserId,
        //        UserName = UserName,
        //        Password = Password,
        //        Email = Email,
        //        Image = Image
        //    };
        //    return user;
        //}

        //public void ChangeToUserLogicModel(PlanPokerUser user)
        //{
        //    UserId = user.UserId;
        //    UserName = user.UserName;
        //    Password = user.Password;
        //    Email = user.Email;
        //    Image = user.Image;
        //}
    }
}