using System.Collections.Generic;
using BugManagement.Data.Models;
using BugManagement.ILogic;
using BugManagement.IRepository;
using BugManagement.Repository.UnitOfWork;

namespace BugManagement.Logic
{
    public class BugLogic : IBugLogic
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public BugLogic(IBugRepository bugRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _bugRepository = bugRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(Bug model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Ins(model);
                unitOfWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Del(id);
                unitOfWork.Commit();
            }
        }

        public void Edit(Bug model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Upd(model);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<Bug> GetAll()
        {
            return _bugRepository.Query();
        }
    }
}
