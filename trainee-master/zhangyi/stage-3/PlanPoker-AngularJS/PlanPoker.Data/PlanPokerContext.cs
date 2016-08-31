using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PlanPoker.Data.Migrations;
using PlanPoker.Data.Models;

namespace PlanPoker.Data
{
    public class PlanPokerContext : DbContext
    {
        public PlanPokerContext() : base("name=PlanPokerContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PlanPokerContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
