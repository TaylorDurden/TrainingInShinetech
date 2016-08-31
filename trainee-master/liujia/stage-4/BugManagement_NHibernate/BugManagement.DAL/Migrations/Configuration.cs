using System.Data.Entity.Migrations;

namespace BugManagement.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BugManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BugManagementContext context)
        {
        }
    }
}
