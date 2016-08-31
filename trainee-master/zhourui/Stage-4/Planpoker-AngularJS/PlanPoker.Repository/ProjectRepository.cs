using System.Linq;
using NHibernate.Linq;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.SessionHelper;

namespace PlanPoker.Repository
{
    public class ProjectNHiberanteRepository : NHiberanteRepositoryBase<Project>, IProjectRepository
    {
        public Project GetByName(string name)
        {
            return Session.Query<Project>().FirstOrDefault(x => x.Name == name);
        }

        public ProjectNHiberanteRepository(ISessionProvider sessionProvider) : base(sessionProvider)
        {
        }
    }
}