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
    public class CauseBugDeveloperLogic: ICauseBugDeveloperLogic
    {
        private readonly ICauseBugDeveloperRepository _causeBugDeveloperRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CauseBugDeveloperLogic(ICauseBugDeveloperRepository causeBugDeveloperRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _causeBugDeveloperRepository = causeBugDeveloperRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(CauseBugDeveloper model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _causeBugDeveloperRepository.Create(model);
                unitOfwork.Commit();
            }                
        }

        public void Edit(CauseBugDeveloper model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _causeBugDeveloperRepository.Edit(model);
                unitOfwork.Commit();
            }            
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _causeBugDeveloperRepository.Delete(id);
                unitOfwork.Commit();
            }            
        }

        public CauseBugDeveloper Get(int id)
        {
            return _causeBugDeveloperRepository.Get(id);
        }

        public IEnumerable<CauseBugDeveloper> GetAll()
        {
            return _causeBugDeveloperRepository.Query().AsEnumerable();
        }

        public IEnumerable<CauseBugDeveloper> GetCauseBugDeveloperByBugId(int bugId)
        {
            return _causeBugDeveloperRepository.Query().Where(n => n.BugId == bugId).AsEnumerable();
        }
    }
}
