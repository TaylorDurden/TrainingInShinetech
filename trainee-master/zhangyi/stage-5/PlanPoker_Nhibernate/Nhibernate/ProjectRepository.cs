using PlanPoker.Data.Models;
using PlanPoker.IRepository;
using RepositoryNhibernate.Dal;

namespace RepositoryNhibernate
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(IFluentNHibernateHelper fluentNHibernateHelper) : base(fluentNHibernateHelper)
        {
        }
    }
}
