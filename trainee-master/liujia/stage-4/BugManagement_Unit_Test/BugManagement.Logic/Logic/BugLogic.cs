using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.Models;
using BugManagement.Logic.ModelExchange;

namespace BugManagement.Logic.Logic
{
    public class BugLogic : IBugLogic
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ICauseBugDeveloperRepository _causeBugDeveloperRepository;
        private readonly IDocumentRepository _documentRepository;

        public BugLogic(IBugRepository bugRepository, IUnitOfWorkFactory unitOfWorkFactory, ICauseBugDeveloperRepository causeBugDeveloperRepository, IDocumentRepository documentRepository)
        {
            _bugRepository = bugRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _causeBugDeveloperRepository = causeBugDeveloperRepository;
            _documentRepository = documentRepository;
        }

        public void Create(BugLogicModel model, List<DeveloperLogicModel> developerLogicModels, string strDocuments)
        {
            var bug = model.ConvertToBug();

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Create(bug);

                if (developerLogicModels != null && developerLogicModels.Any())
                {
                    var causeBugDevelopers = developerLogicModels.Select(developer => new CauseBugDeveloper
                    {
                        DeveloperId = Convert.ToInt32(developer.DeveloperId),
                        BugId = bug.BugId
                    }).ToList();
                    bug.CauseBugDevelopers = causeBugDevelopers;
                }
                
                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    var documents = strArrayDocuments.Select(str => new Document
                    {
                        Path = str,
                        BugId = bug.BugId
                    }).ToList();
                    bug.Documents = documents;
                }

                unitOfwork.Commit();
            }
        }

        public void Edit(BugLogicModel model, List<DeveloperLogicModel> developerLogicModels, string strDocuments)
        {
            var bug = model.ConvertToBug();
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Edit(bug);

                var causeBugDeveloperDBs = _causeBugDeveloperRepository.Query().Where(n => n.BugId == model.BugId);
                if (causeBugDeveloperDBs.Any())
                {
                    foreach (var d in causeBugDeveloperDBs)
                    {
                        _causeBugDeveloperRepository.Delete(d.Id);
                    }
                }

                if (developerLogicModels != null && developerLogicModels.Any())
                {
                    var causeBugDevelopers = developerLogicModels.Select(developer => new CauseBugDeveloper
                    {
                        DeveloperId = Convert.ToInt32(developer.DeveloperId),
                        BugId = bug.BugId
                    }).ToList();
                    bug.CauseBugDevelopers = causeBugDevelopers;
                }

                var documentDBs = _documentRepository.Query().Where(n => n.BugId == model.BugId);
                if (documentDBs.Any())
                {
                    foreach (var d in documentDBs)
                    {
                        _documentRepository.Delete(d.DocumentId);
                    }
                }

                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    var documents = strArrayDocuments.Select(str => new Document
                    {
                        Path = str,
                        BugId = bug.BugId
                    }).ToList();
                    bug.Documents = documents;
                }

                unitOfwork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Delete(id);
                unitOfwork.Commit();
            }
        }

        public BugLogicModel Get(int id)
        {
            var bug = _bugRepository.Get(id);
            return bug.ConvertToBugLogicModel();
        }

        public List<BugLogicModel> GetAll()
        {
            var model = _bugRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToBugLogicModel()).ToList();
        }

        public List<BugLogicModel> GetBugLogicModelsBySerchCondition(string serchCondition)
        {
            var model = _bugRepository.Query().Where(n => string.IsNullOrEmpty(serchCondition) || n.Title.Contains(serchCondition) || n.Smmary.Contains(serchCondition) || n.Status.Contains(serchCondition) || n.Project.ProjectName.Contains(serchCondition));
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToBugLogicModel()).ToList();
        }

        public int GetPageCountByCondition(string serchCondition)
        {
            return
                _bugRepository
                    .Query()
                    .Count(n => string.IsNullOrEmpty(serchCondition) || n.Title.Contains(serchCondition) || n.Smmary.Contains(serchCondition) || n.Status.Contains(serchCondition) || n.Project.ProjectName.Contains(serchCondition));
        }

        public List<BugLogicModel> GetBugLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count)
        {
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            var model = _bugRepository.Query()
                .Where(
                    n => string.IsNullOrEmpty(serchCondition) || n.Title.Contains(serchCondition) || n.Smmary.Contains(serchCondition) || n.Status.Contains(serchCondition) || n.Project.ProjectName.Contains(serchCondition)).OrderBy(n => n.BugId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return model.Select(n => n.ConvertToBugLogicModel()).ToList();
        }

        public void UpdateBugStatus(int bugId, string stauts)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var dbBug = _bugRepository.Get(bugId);
                dbBug.Status = stauts;
                _bugRepository.Edit(dbBug);
                unitWork.Commit();
            }
        }
    }
}
