using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugManagement.DAL.Model;
using System.Data.Entity;
using BugManagement.DAL.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BugManagement.DAL
{
    public class BugManagementContext: DbContext
    {
        public BugManagementContext() : base("name=BugManagementContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BugManagementContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<BugType> BugTypes { get; set; }
        public DbSet<CauseBugDeveloper> CauseBugDevelopers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }    
}
