using System.Collections.Generic;
using BugManagement.Data.Models;
using BugManagement.ILogic;
using BugManagement.IRepository;
using BugManagement.Repository.UnitOfWork;

namespace BugManagement.Logic
{
    public class BugTypeLogic : IBugTypeLogic
    {
        private readonly IBugTypeRepository _bugTypeRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public BugTypeLogic(IBugTypeRepository bugTypeRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _bugTypeRepository = bugTypeRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(BugType model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugTypeRepository.Ins(model);
                unitOfWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugTypeRepository.Del(id);
                unitOfWork.Commit();
            }
        }

        public void Edit(BugType model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugTypeRepository.Upd(model);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<BugType> GetAll()
        {
            return _bugTypeRepository.Query();
        }
    }
}
