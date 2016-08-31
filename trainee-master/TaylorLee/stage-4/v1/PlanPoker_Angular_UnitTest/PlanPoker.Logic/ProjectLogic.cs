using System.Collections.Generic;
using System.Linq;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public ProjectLogic(IProjectRepository projectRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _projectRepository = projectRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public ProjectLogicModel Create(ProjectLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var entity = _projectRepository.Create(model.ConvertToModel());
                unitOfwork.Commit();
                return entity.ConvertToLogicModel();
            }
        }

        public void Edit(ProjectLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Edit(model.ConvertToModel());
                unitOfwork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Delete(id);
                unitOfwork.Commit();
            }
        }

        public ProjectLogicModel Get(int id)
        {
            var project = _projectRepository.Get(id);
            ProjectLogicModel projectLogicModel = project.ConvertToLogicModel();
            return projectLogicModel;
        }

        public IEnumerable<ProjectLogicModel> GetAll()
        {
            var projects = _projectRepository.Query().ToList();
            List<ProjectLogicModel> projectLogicModels = new List<ProjectLogicModel>();
            foreach (var project in projects)
            {
                ProjectLogicModel projectLogicModel = project.ConvertToLogicModel();
                projectLogicModels.Add(projectLogicModel);
            }
            return projectLogicModels;
        }

        public IEnumerable<ProjectLogicModel> Get(string name)
        {
            var projects = _projectRepository.Query().Where(x => x.Name.Contains(name)).ToList();
            List<ProjectLogicModel> projectLogicModels = new List<ProjectLogicModel>();
            foreach (var project in projects)
            {
                ProjectLogicModel projectLogicModel = project.ConvertToLogicModel();
                projectLogicModels.Add(projectLogicModel);
            }
            return projectLogicModels;
        }

        public bool CheckExist(string projectName)
        {
            if (_projectRepository.Query().Any())
            {
                return _projectRepository
                    .Query().Any(x => x.Name.Equals(projectName));
            }
            return false;
        }
    }
}
