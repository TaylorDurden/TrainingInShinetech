using BugManagement.Dao.IDao;
using FluentNHibernate.Utils;
using NHibernate;

namespace BugManagement.Dao.UnitOfWork
{
    public class NhibernateUnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;
        private readonly ISession _session;

        public NhibernateUnitOfWork(ISessionProvider sessionProvider)
        {
            _session = sessionProvider.GetCurrentSession();
            _transaction = _session.BeginTransaction();
        }

        public void Dispose()
        {
            if (!_session.IsConnected)
            {
                _transaction.Dispose();
                _session.Dispose();
            }
        }

        public void Commit()
        {
            _session.Flush();
            _transaction.Commit();
        }
    }
}
