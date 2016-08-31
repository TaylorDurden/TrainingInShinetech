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
    public class CauseBugDeveloperRepository : RepositoryBase<CauseBugDeveloper>, ICauseBugDeveloperRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<CauseBugDeveloper> DbSet => DataContext.Set<CauseBugDeveloper>();

        public CauseBugDeveloperRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}
