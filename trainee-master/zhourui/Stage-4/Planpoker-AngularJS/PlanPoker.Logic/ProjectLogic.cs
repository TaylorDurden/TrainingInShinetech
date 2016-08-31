using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
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

        public ProjectLogicModel Create(ProjectLogicModel projectLogicModel)
        {
            if (projectLogicModel == null) return null;

            if (_projectRepository.GetByName(projectLogicModel.Name) != null)
            {
                projectLogicModel.Message = "the project name exist, please input a new project name.";
                projectLogicModel.Status = false;
                return projectLogicModel;
            }

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var project = _projectRepository.Create(projectLogicModel.ConvertToProjectEntity());
                unitOfwork.Commit();

                return project.ConvertToProjectLogicModel(string.Empty, true);
            }
        }

        public ProjectLogicModel Edit(ProjectLogicModel projectLogicModel)
        {
            if (projectLogicModel == null) return null;

            if (_projectRepository.GetByName(projectLogicModel.Name) != null)
            {
                projectLogicModel.Message = "the projectname exist, please select a new projectname to update.";
                projectLogicModel.Status = false;
                return projectLogicModel;
            }

            var project = _projectRepository.Get(projectLogicModel.Id);
            project.Name = projectLogicModel.Name;

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Edit(project);
                unitOfwork.Commit();

                return project.ConvertToProjectLogicModel(string.Empty, true);
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

        public IEnumerable<ProjectLogicModel> GetAll()
        {
            return _projectRepository.LoadAll()
                .Select(project => project.ConvertToProjectLogicModel(string.Empty, true));
        }

        public ProjectLogicModel Get(int id)
        {
            return id > 0
                ? _projectRepository.Get(id).ConvertToProjectLogicModel(string.Empty, true)
                : _projectRepository.Get(id).ConvertToProjectLogicModel("project id should greater than zero.", false);
        }

        public ProjectLogicModel Get(string name)
        {
            return !string.IsNullOrEmpty(name)
                ? _projectRepository.GetByName(name).ConvertToProjectLogicModel(string.Empty, true)
                : _projectRepository.GetByName(name).ConvertToProjectLogicModel("project name cannot be empty.", false);
        }

        public bool CheckIfProjectnameExists(string name)
        {
            return _projectRepository.LoadAll().Any(project => project.Name == name);
        }
    }
}