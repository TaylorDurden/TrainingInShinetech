using System.Collections.Generic;
using BugManagement.Data.Models;
using BugManagement.ILogic;
using BugManagement.IRepository;
using BugManagement.Repository.UnitOfWork;

namespace BugManagement.Logic
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

        public void Create(Developer model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Ins(model);
                unitOfWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Del(id);
                unitOfWork.Commit();
            }
        }

        public void Edit(Developer model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Upd(model);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<Developer> GetAll()
        {
            return _developerRepository.Query();
        }
    }
}
