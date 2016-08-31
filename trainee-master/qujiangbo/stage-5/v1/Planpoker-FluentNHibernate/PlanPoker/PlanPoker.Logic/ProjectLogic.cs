using System.Collections.Generic;
using System.Linq;
using PlanPoker.ILogic;
using PlanPoker.IRepository;
using PlanPoker.Repository;
using PlanPoker.ILogic.Models;

namespace PlanPoker.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectLogic(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        public void Create(ProjectLogicModel model)
        {
            var project = model.CreateConvert();
            
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _projectRepository.Create(project);
                    transaction.Commit();
                }
            }
        }

        public void Edit(ProjectLogicModel model)
        {
            var project = _projectRepository.Get(model.Id);
            project.Name = model.Name;
            project.ProjectGuid = model.ProjectGuid;

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _projectRepository.Edit(project);
                    transaction.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _projectRepository.Delete(id);
                    transaction.Commit();
                }
            }
        }

        public ProjectLogicModel Get(int id)
        {
            return _projectRepository.Get(id).GetConvert();             
        }

        public List<ProjectLogicModel> GetAll()
        {
            return _projectRepository.Query().ToList().GetAllConvert();
        }

        public List<ProjectLogicModel> Get(string name)
        {
            return _projectRepository.Query().Where(x => x.Name==name).ToList().GetConvert();
        }

        public bool CheckExist(string projectName)
        {
            return _projectRepository.Query().Where(x => x.Name == projectName).ToList().Count > 0;
        }
    }
}
