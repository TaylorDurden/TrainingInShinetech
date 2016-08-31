using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
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


        public string Create(Project model)
        {
            var message = "";
            if (!CheckIfExist(model.Name))
            {
                using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
                {
                    _projectRepository.Create(model);
                    unitOfwork.Commit();
                }
            }
            else
            {
                message = "the project name exists, please input a new project name.";
            }
            return message;
        }

        public string Edit(Project model)
        {
            var message = "";
            if (!CheckIfExist(model.Name))
            {
                using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
                {
                    _projectRepository.Edit(model);
                    unitOfwork.Commit();
                }
            }
            else
            {
                message = "the project name exists, please input a new project name.";
            }
            return message;
        }

        public string Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Delete(id);
                unitOfwork.Commit();
            }
            return "delete success.";
        }

        public Project Get(int id)
        {
            return _projectRepository.Get(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.Query().ToList();
        }

        public Project Get(string name)
        {
            return _projectRepository.Get(name);
        }

        public string GetGuidUrl(int id)
        {
            if (id <= 0) return "";
            var url = ConfigurationManager.AppSettings["WebApplicationPath"] +
                      "/views/ProjectEstimate.html?presenter=1&projectGuid=";
            return url + _projectRepository.Get(id).ProjectGuid;
        }
        
        public bool CheckIfExist(string name)
        {
            try
            {
                return _projectRepository.Query().Where(x => x.Name == name).ToList().Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}