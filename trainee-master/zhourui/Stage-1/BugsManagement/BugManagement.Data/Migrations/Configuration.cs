using System.Data.Entity.Migrations;
using BugManagement.Data.Models;

namespace BugManagement.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<BugManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BugManagementContext context)
        {
            //base.Seed(context);
            //var initUser = new User
            //{
            //    FirstName = "Zhou",
            //    LastName = "Rui",
            //    Email = "zhourui@shinetechchina.com",
            //    Type = User.UserType.Admin,
            //    Password = "123",
            //    Status = User.UserStatus.Checked
            //};
            //context.Users.Add(initUser);
        }
    }
}
