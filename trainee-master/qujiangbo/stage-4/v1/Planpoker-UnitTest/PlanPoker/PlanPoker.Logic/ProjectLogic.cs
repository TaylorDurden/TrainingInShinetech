using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;
using PlanPoker.ILogic.Models;

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
            var project = model.CreateConvert();
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Create(project);
                unitOfwork.Commit();
            }
        }

        public void Edit(ProjectLogicModel model)
        {
            var project = model.EditConvert();
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Edit(project);
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
