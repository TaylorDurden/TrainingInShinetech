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
    public class DeveloperRepository : RepositoryBase<Developer>, IDeveloperRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<Developer> DbSet => DataContext.Set<Developer>();

        public DeveloperRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}
