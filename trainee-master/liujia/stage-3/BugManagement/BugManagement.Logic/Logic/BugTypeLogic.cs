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
    public class BugTypeLogic: IBugTypeLogic
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
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugTypeRepository.Create(model);
                unitOfwork.Commit();
            }                
        }

        public void Edit(BugType model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugTypeRepository.Edit(model);
                unitOfwork.Commit();
            }            
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugTypeRepository.Delete(id);
                unitOfwork.Commit();
            }            
        }

        public BugType Get(int id)
        {
            return _bugTypeRepository.Get(id);
        }

        public IEnumerable<BugType> GetAll()
        {
            return _bugTypeRepository.Query().AsEnumerable();
        }
    }
}
