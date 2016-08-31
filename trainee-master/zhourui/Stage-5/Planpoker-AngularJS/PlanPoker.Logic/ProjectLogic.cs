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

            if (_projectRepository.Get(projectLogicModel.Name) != null)
            {
                projectLogicModel.Message = "the project name exist, please input a new project name.";
                return projectLogicModel;
            }

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Create(projectLogicModel.ConvertToProjectEntity());
                unitOfwork.Commit();
            }

            return projectLogicModel;
        }

        public ProjectLogicModel Edit(ProjectLogicModel projectLogicModel)
        {
            if (projectLogicModel == null) return null;

            if (_projectRepository.Get(projectLogicModel.Name) != null)
            {
                projectLogicModel.Message = "the projectname exist, please select a new projectname to update.";
                return projectLogicModel;
            }

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Edit(projectLogicModel.ConvertToProjectEntity());
                unitOfwork.Commit();
            }

            return projectLogicModel;
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
            IList<Project> projects = _projectRepository.Query().ToList();
            return projects.Select(project => project.ConvertToProjectLogicModel("")).ToList();
        }

        public ProjectLogicModel Get(int id)
        {
            return _projectRepository.Get(id).ConvertToProjectLogicModel(
                id > 0
                    ? ""
                    : "project id should greater than zero.");
        }

        public ProjectLogicModel Get(string name)
        {
            return _projectRepository.Get(name).ConvertToProjectLogicModel(
                !string.IsNullOrEmpty(name)
                    ? ""
                    : "project name cannot be empty.");
        }

        public bool CheckIfProjectnameExists(string name)
        {
            return _projectRepository.Query().Any(project => project.Name == name);
        }
    }
}