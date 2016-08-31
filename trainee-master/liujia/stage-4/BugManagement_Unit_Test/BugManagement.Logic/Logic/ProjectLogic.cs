using System;
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
        
        public bool CheckExist(string projectName,string projectId)
        {
            if (!_projectRepository.Query().Any())
            {
                return false;
            }
            
            return _projectRepository
                .Query()
                .Where(n => string.IsNullOrEmpty(projectId) || n.ProjectId.ToString() != projectId)
                .Any(x => x.ProjectName.Equals(projectName));
        }
        
        public int GetPageCountByCondition(string serchCondition)
        {
            return
                _projectRepository
                    .Query(
                        )
                    .Count(n => string.IsNullOrEmpty(serchCondition) || n.ProjectName.Contains(serchCondition) ||
                            n.MainContact.Contains(serchCondition) || n.Description.Contains(serchCondition) ||
                            n.ContactEmail.Contains(serchCondition));
        }

        public List<ProjectLogicModel> GetProjectLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count)
        {
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            var model = _projectRepository.Query()
                .Where(
                    n =>
                        string.IsNullOrEmpty(serchCondition) || n.ProjectName.Contains(serchCondition) ||
                        n.MainContact.Contains(serchCondition) || n.Description.Contains(serchCondition) ||
                        n.ContactEmail.Contains(serchCondition)).OrderBy(n => n.ProjectId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return model.ConvertToProjectLogicModels();
        }
    }
}
