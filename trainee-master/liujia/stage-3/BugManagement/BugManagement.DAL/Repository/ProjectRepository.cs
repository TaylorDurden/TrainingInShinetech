using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.DAL.Repository
{
    public class ProjectRepository:RepositoryBase<Project>, IProjectRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<Project> DbSet => DataContext.Set<Project>();

        public ProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Project Get(string projectName)
        {
            return DbSet.FirstOrDefault(p => p.ProjectName == projectName);
        }
    }
}
