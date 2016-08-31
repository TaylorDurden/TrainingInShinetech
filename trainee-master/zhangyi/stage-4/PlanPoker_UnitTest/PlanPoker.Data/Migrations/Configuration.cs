using System.Data.Entity.Migrations;

namespace PlanPoker.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PlanPokerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PlanPokerContext context)
        {
        }
    }
}
