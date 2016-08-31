using BugManagement.DAL.Model;

namespace BugManagement.DAL.IRepository
{
    public interface IProjectRepository:IRepositoryBase<Project>
    {
        Project Get(string projectName);
    }
}
