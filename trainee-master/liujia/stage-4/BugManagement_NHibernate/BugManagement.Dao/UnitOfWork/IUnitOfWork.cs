using System;

namespace BugManagement.Dao.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}