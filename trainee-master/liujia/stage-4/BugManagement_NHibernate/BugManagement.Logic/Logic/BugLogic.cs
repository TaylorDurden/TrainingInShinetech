using BugManagement.Logic.ILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Dao.UnitOfWork;
using BugManagement.Data.Models.Mappings;
using BugManagement.Logic.Models;
using BugManagement.Logic.ModelExchange;

namespace BugManagement.Logic.Logic
{
    public class BugLogic : IBugLogic
    {
        private readonly IBugDao _bugDao;
        private readonly ICauseBugDeveloperDao _causeBugDeveloperDao;
        private readonly IDocumentDao _documentDao;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IProjectDao _projectDao;

        public BugLogic(IBugDao bugDao, ICauseBugDeveloperDao causeBugDeveloperDao, IDocumentDao documentDao,IUnitOfWorkFactory unitOfWorkFactory,IProjectDao projectDao)
        {
            _bugDao = bugDao;
            _causeBugDeveloperDao = causeBugDeveloperDao;
            _documentDao = documentDao;
            _unitOfWorkFactory = unitOfWorkFactory;
            _projectDao = projectDao;
        }

        public void Create(BugLogicModel model, List<DeveloperLogicModel> developerLogicModels, string strDocuments)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var bug = model.ConvertToBug();

                bug.Project = _projectDao.Get(model.ProjectId);


                if (developerLogicModels != null && developerLogicModels.Any())
                {
                    var causeBugDevelopers = developerLogicModels.Select(developer => new CauseBugDeveloper
                    {
                        Developer = developer.ConvertToDeveloper(),
                        Bug = bug
                    }).ToList();
                    bug.CauseBugDevelopers = causeBugDevelopers;
                }

                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    var documents = strArrayDocuments.Select(str => new Document
                    {
                        Path = str,
                        Bug = bug
                    }).ToList();
                    bug.Documents = documents;
                }

                _bugDao.Create(bug);
                unitWork.Commit();
            }
        }

        public void Edit(BugLogicModel model, List<DeveloperLogicModel> developerLogicModels, string strDocuments)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var bugDb = _bugDao.Get(model.BugId);
                model.ConvertBugLogicModelToDbBug(bugDb);

                var causeBugDeveloperDBs = _causeBugDeveloperDao.GetCauseBugDevelopersByBugId(model.BugId);
                if (causeBugDeveloperDBs.Any())
                {
                    foreach (var d in causeBugDeveloperDBs)
                    {
                        _causeBugDeveloperDao.Delete(d.Id);
                    }
                }
                
                if (developerLogicModels != null && developerLogicModels.Any())
                {
                    var causeBugDevelopers = developerLogicModels.Select(developer => new CauseBugDeveloper
                    {
                        Developer=developer.ConvertToDeveloper(),
                        Bug = bugDb
                    }).ToList();
                    bugDb.CauseBugDevelopers = causeBugDevelopers;
                }
                else
                {
                    bugDb.CauseBugDevelopers = null;
                }

                var documentDBs = _documentDao.GetDocumentsByBugId(model.BugId);
                if (documentDBs.Any())
                {
                    foreach (var d in documentDBs)
                    {
                        _documentDao.Delete(d.DocumentId);
                    }
                }

                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    var documents = strArrayDocuments.Select(str => new Document
                    {
                        Path = str,
                        Bug = bugDb
                    }).ToList();
                    bugDb.Documents = documents;
                }
                else
                {
                    bugDb.Documents = null;
                }

                _bugDao.Edit(bugDb);
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugDao.Delete(id);
                unitWork.Commit();
            }
        }

        public BugLogicModel Get(int id)
        {
            var bug = _bugDao.Get(id);
            return bug.ConvertToBugLogicModel();
        }

        public List<BugLogicModel> GetAll()
        {
            var model = _bugDao.LoadAll();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToBugLogicModel()).ToList();
        }

        public List<BugLogicModel> GetBugLogicModelsBySerchCondition(string serchCondition)
        {
            var model = _bugDao.LoadAll().Where(n => string.IsNullOrEmpty(serchCondition) || n.Title.Contains(serchCondition) || n.Smmary.Contains(serchCondition) || n.Status.Contains(serchCondition) || n.Project.ProjectName.Contains(serchCondition)).ToList();
            return !model.Any() ? null : model.Select(m => m.ConvertToBugLogicModel()).ToList();
        }

        public int GetPageCountByCondition(string serchCondition)
        {
            return
                _bugDao
                    .LoadAll()
                    .Count(n => string.IsNullOrEmpty(serchCondition) || n.Title.Contains(serchCondition) || n.Smmary.Contains(serchCondition) || n.Status.Contains(serchCondition) || n.Project.ProjectName.Contains(serchCondition));
        }

        public List<BugLogicModel> GetBugLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count)
        {
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            
            var model = _bugDao.LoadAll()
                .Where(
                    n => string.IsNullOrEmpty(serchCondition) || n.Title.Contains(serchCondition) || n.Smmary.Contains(serchCondition) || n.Status.Contains(serchCondition) || n.Project.ProjectName.Contains(serchCondition)).OrderBy(n => n.BugId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return model.Select(n => n.ConvertToBugLogicModel()).ToList();
        }

        public void UpdateBugStatus(int bugId, string stauts)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var dbBug = _bugDao.Get(bugId);
                dbBug.Status = stauts;
                _bugDao.Edit(dbBug);
                
                unitWork.Commit();
            }
        }
    }
}
