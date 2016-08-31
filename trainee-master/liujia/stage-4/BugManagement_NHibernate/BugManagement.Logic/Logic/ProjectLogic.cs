using System;
using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Dao.UnitOfWork;
using BugManagement.Logic.Models;
using BugManagement.Logic.ModelExchange;

namespace BugManagement.Logic.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectDao _projectDao;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProjectLogic(IProjectDao projectDao, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _projectDao = projectDao;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(ProjectLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectDao.Create(model.ConvertToProject());
                unitWork.Commit();
            }
        }

        public void Edit(ProjectLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var projectDb = _projectDao.Get(model.ProjectId);
                model.ConvertProjectLogicModelToDbProject(projectDb);
                _projectDao.Edit(projectDb);
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectDao.Delete(id);
                unitWork.Commit();
            }
        }

        public ProjectLogicModel Get(int id)
        {
            var project = _projectDao.Get(id);
            return project.ConvertToProjectLogicModel();
        }

        public List<ProjectLogicModel> GetAll()
        {
            var model = _projectDao.LoadAll();
            return model != null && model.Any() ? model.ToList().Select(m => m.ConvertToProjectLogicModel()).ToList() : null;
        }

        public bool CheckExist(string projectName, string projectId)
        {
            if (_projectDao.LoadAll().Any())
            {
                return _projectDao
                    .LoadAll()
                    .Where(n => string.IsNullOrEmpty(projectId) || n.ProjectId.ToString() != projectId)
                   .Any(x => x.ProjectName.Equals(projectName));
            }
            return false;
        }

        public int GetPageCountByCondition(string serchCondition)
        {
            return
                    _projectDao
                        .LoadAll()
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

            var model = _projectDao.LoadAll()
                .Where(
                    n =>
                        string.IsNullOrEmpty(serchCondition) || n.ProjectName.Contains(serchCondition) ||
                        n.MainContact.Contains(serchCondition) || n.Description.Contains(serchCondition) ||
                        n.ContactEmail.Contains(serchCondition))
                .OrderBy(n => n.ProjectId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return model.ConvertToProjectLogicModels();
        }
    }
}
