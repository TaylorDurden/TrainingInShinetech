using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using System.Data.Entity;

namespace BugManagement.DAL.Repository
{
    public class BugTypeRepository: RepositoryBase<BugType>,IBugTypeRepository
    {
        private readonly IDbFactory _dbFactory;

        private DbContext DataContext => _dbFactory.GetContext();
        public IDbSet<BugType> DbSet => DataContext.Set<BugType>();

        public BugTypeRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }
    }
}
