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
    public class DeveloperLogic: IDeveloperLogic
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
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Create(model);
                unitWork.Commit();
            }                
        }

        public void Edit(Developer model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Edit(model);
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

        public Developer Get(int id)
        {
            return _developerRepository.Get(id);
        }

        public IEnumerable<Developer> GetAll()
        {
            return _developerRepository.Query().ToList();
        }

        public IEnumerable<Developer> GetDeveloperByWhereCondition(string whereCondition)
        {
            return _developerRepository.Query().Where(n => string.IsNullOrEmpty(whereCondition) || n.FristName.Contains(whereCondition) || n.LastName.Contains(whereCondition) || n.Email.Contains(whereCondition) || n.Status.Contains(whereCondition)).AsEnumerable();
        }
    }
}
