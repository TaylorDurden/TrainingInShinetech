using NHibernate;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Dao
{
    public class ProjectDao : DaoBase<Project>, IProjectRepository
    {
        private readonly IDbFactory _dbFactory;
        private ISession SessionContext => _dbFactory.GetISessionContext();
        public ProjectDao(IDbFactory dbfactory, IDbFactory dbFactory) : base(dbfactory)
        {
            _dbFactory = dbFactory;
        }
    }
}
