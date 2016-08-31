using NHibernate;
using PlanPoker.Data.SessionHelper;

namespace PlanPoker.Repository.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;
        private bool _isCommitted;

        public NHibernateUnitOfWork(ISessionProvider sessionProvider)
        {
            _transaction = sessionProvider.GetCurrentSession().BeginTransaction();
        }

        public void Dispose()
        {
            if (!_isCommitted)
            {
                Commit();
            }
        }

        public void Commit()
        {
            _transaction.Commit();
            _isCommitted = true;
        }
    }
}
