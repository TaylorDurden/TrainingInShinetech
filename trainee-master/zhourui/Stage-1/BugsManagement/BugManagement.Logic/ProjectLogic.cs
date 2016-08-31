using System.Collections.Generic;
using BugManagement.Data.Models;
using BugManagement.ILogic;
using BugManagement.IRepository;
using BugManagement.Repository.UnitOfWork;

namespace BugManagement.Logic
{
    public class ProjectLogic:IProjectLogic
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
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Ins(model);
                unitOfWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWOrk = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Del(id);
                unitOfWOrk.Commit();
            }
        }

        public void Edit(Project model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Upd(model);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.Query();
        }
    }
}
