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
    public class BugTypeRepository: RepositoryBase<BugType>,IBugTypeRepository
    {
        private readonly IDbFactory _dbFactory;

        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<BugType> DbSet => DataContext.Set<BugType>();

        public BugTypeRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }
    }
}
