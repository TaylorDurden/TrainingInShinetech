using System;
using System.Data.Entity;
using PlanPoker.Data;

namespace PlanPoker.Repository.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly IDbFactory _factory;
        private DbContext DataContext => _factory.GetContext();

        public EntityFrameworkUnitOfWork(IDbFactory factory)
        {
            _factory = factory;
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DataContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
