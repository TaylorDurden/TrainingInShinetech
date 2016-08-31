using System.Linq;
using NHibernate.Linq;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public Project Get(string projectName)
        {
            var session = NHibernateHelper.OpenSession();
            return session.Query<Project>().FirstOrDefault(x => x.Name == projectName);
        }
    }
}
