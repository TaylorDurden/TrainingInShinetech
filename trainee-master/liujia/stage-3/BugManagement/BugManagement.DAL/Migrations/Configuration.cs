using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
