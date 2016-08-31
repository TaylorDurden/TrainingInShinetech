using BugManagement.Data;
using BugManagement.Data.Models;
using BugManagement.IRepository;

namespace BugManagement.Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        private readonly IDbFactory _dbFactory;

        public ProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}
