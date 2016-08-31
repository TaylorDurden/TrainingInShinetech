using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.DAL.Repository;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Create(Project model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Create(model);
                unitWork.Commit();
            }                
        }

        public void Edit(Project model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Edit(model);
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

        public Project Get(int id)
        {
            return _projectRepository.Get(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.Query().ToList();
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

        public IEnumerable<Project> GetProjectByWhereCondition(string whereCondition)
        {
           return _projectRepository.Query().Where(n => string.IsNullOrEmpty(whereCondition) || n.ProjectName.Contains(whereCondition) || n.MainContact.Contains(whereCondition) || n.Description.Contains(whereCondition) || n.ContactEmail.Contains(whereCondition)).AsEnumerable();
        }
    }
}
