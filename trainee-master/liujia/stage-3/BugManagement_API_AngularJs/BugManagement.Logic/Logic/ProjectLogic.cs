using BugManagement.DAL.IRepository;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.Models;
using BugManagement.Logic.ModelExchange;

namespace BugManagement.Logic.Logic
{
    public class ProjectLogic: IProjectLogic
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
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Create(model.ConvertToProject());
                unitWork.Commit();
            }  
        }

        public void Edit(ProjectLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Edit(model.ConvertToProject());
                unitWork.Commit();
            }            
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Delete(id);
                unitWork.Commit();
            }            
        }

        public ProjectLogicModel Get(int id)
        {
            var project = _projectRepository.Get(id);
            return project.ConvertToProjectLogicModel();
        }

        public List<ProjectLogicModel> GetAll()
        {
            var model = _projectRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToProjectLogicModel()).ToList();
        }
        
        public bool CheckExist(string projectName)
        {
            if (_projectRepository.Query().Any())
            {
                return _projectRepository
                    .Query().Any(x => x.ProjectName.Equals(projectName));
            }
            return false;
        }

        public List<ProjectLogicModel> GetProjectByWhereCondition(string whereCondition)
        {
            var model =
                _projectRepository.Query()
                    .Where(
                        n =>
                            string.IsNullOrEmpty(whereCondition) || n.ProjectName.Contains(whereCondition) ||
                            n.MainContact.Contains(whereCondition) || n.Description.Contains(whereCondition) ||
                            n.ContactEmail.Contains(whereCondition));
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToProjectLogicModel()).ToList();
        }
    }
}
