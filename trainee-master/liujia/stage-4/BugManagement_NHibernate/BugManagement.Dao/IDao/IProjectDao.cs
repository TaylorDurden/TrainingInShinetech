using BugManagement.Data.Models.Mappings;

namespace BugManagement.Dao.IDao
{
    public interface IProjectDao : IDaoBase<Project>
    {
        Project GetProjectByName(string projectName);
    }
}