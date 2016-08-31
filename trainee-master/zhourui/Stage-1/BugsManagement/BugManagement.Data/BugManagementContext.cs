using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BugManagement.Data.Migrations;
using BugManagement.Data.Models;

namespace BugManagement.Data
{
    public class BugManagementContext : DbContext
    {
        public BugManagementContext() : base("name=BugManagementDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BugManagementContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<BugType> BugTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }
    }
}
