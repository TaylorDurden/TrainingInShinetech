using System;
using System.Collections.Generic;
using System.Linq;
using PlanPoker.ILogic;
using PlanPoker.ILogic.LogicModel;
using PlanPoker.IRepository;
using RepositoryNhibernate.UnitOfWork;

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


        public void Create(ProjectLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var project = model.ConvertToProjectEntity();
                project.ProjectGuid = Guid.NewGuid();
                _projectRepository.Create(project);
                unitOfwork.Commit();
            }

        }

        public void Edit(ProjectLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var modelInSession = _projectRepository.Get(model.Id);
                modelInSession.Name = model.Name;
                _projectRepository.Edit(modelInSession);
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
            var projectLogicModel = project.ConvertToProjectLogicModel();

            return projectLogicModel;
        }

        public IEnumerable<ProjectLogicModel> GetAll()
        {
            var projects = _projectRepository.Query();
            var projectLogicModels = new List<ProjectLogicModel>();
            projectLogicModels.AddRange(projects.Select(project => project.ConvertToProjectLogicModel()));

            return projectLogicModels;
        }

        public IEnumerable<ProjectLogicModel> Get(string name)
        {
            var projects = _projectRepository.Query().Where(x => x.Name.Contains(name));
            var projectLogicModels = new List<ProjectLogicModel>();
            projectLogicModels.AddRange(projects.Select(project => project.ConvertToProjectLogicModel()));

            return projectLogicModels;
        }

        public bool CheckExist(string projectName)
        {
            return _projectRepository.Query().Any(x => x.Name == projectName);
        }
    }
}
