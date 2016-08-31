using BugManagement.DAL.IRepository;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DeveloperLogic(IDeveloperRepository developerRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _developerRepository = developerRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(DeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Create(model.ConvertToDeveloper());
                unitWork.Commit();
            }
        }

        public void Edit(DeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Edit(model.ConvertToDeveloper());
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Delete(id);
                unitWork.Commit();
            }
        }

        public DeveloperLogicModel Get(int id)
        {
            var developer = _developerRepository.Get(id);
            return developer.ConvertToDeveloperLogicModel();
        }

        public List<DeveloperLogicModel> GetAll()
        {
            var model = _developerRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToDeveloperLogicModel()).ToList();
        }

        public List<DeveloperLogicModel> GetDeveloperByWhereCondition(string whereCondition)
        {
            var model =
                _developerRepository.Query()
                    .Where(
                        n =>
                            string.IsNullOrEmpty(whereCondition) || n.FristName.Contains(whereCondition) ||
                            n.LastName.Contains(whereCondition) || n.Email.Contains(whereCondition) ||
                            n.Status.Contains(whereCondition));
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToDeveloperLogicModel()).ToList();
        }
    }
}
