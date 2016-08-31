using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugManagement.DAL.IRepository;

namespace BugManagement.DAL.Repository
{
    public class BugRepository : RepositoryBase<Bug>,IBugRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<Bug> DbSet => DataContext.Set<Bug>();

        public BugRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }
        
        //public override IQueryable<Bug> Query()
        //{
        //    return DbSet.Include("Project").AsQueryable();
        //}

    }
}
