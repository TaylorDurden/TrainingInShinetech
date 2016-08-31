using System;
using System.Data.Entity;
using PlanPoker.Data;

namespace PlanPoker.Repository.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _factory;
        private bool _disposed;

        public EntityFrameworkUnitOfWork(IDbFactory factory)
        {
            _factory = factory;
        }

        private DbContext DataContext => _factory.GetContext();

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