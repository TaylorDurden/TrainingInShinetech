using NHibernate;
using RepositoryNhibernate.Dal;

namespace RepositoryNhibernate.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;
        private bool _isCommitted;

        public NHibernateUnitOfWork(IFluentNHibernateHelper fluentNHibernateHelper)
        {
            _transaction = fluentNHibernateHelper.GetSession().BeginTransaction();
        }

        public void Dispose()
        {
            if (!_isCommitted)
                Commit();
        }

        public void Commit()
        {
            _transaction.Commit();
            _isCommitted = true;
        }
    }
}