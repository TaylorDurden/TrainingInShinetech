using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Data.Models.Mappings;
using NHibernate;

namespace BugManagement.Dao.Dao
{
    public class ProjectDao : DaoBase<Project>, IProjectDao
    {
        public ProjectDao(ISessionProvider sessionProvider) :base(sessionProvider)
        {
        }

        public Project GetProjectByName(string projectName)
        {
            var project =
                    Session.CreateQuery("from project c where c.ProjectName='" + projectName + "'")
                        .List<Project>()
                        .First();

            return project;
        }
    }
}