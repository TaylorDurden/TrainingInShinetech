using System.Data.Entity;
using System.Linq;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        private readonly IDbFactory _dbFactory;

        public ProjectRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }

        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<Project> DbSet => DataContext.Set<Project>();

        public Project Get(string projectName)
        {
            return DbSet.FirstOrDefault(p => p.Name == projectName);
        }
    }
}