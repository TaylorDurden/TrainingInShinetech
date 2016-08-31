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

        public void Create(BugLogicModel model, string strDeveloperIds, string strDocuments)
        {
            var bug = model.ConvertToBug();

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Create(bug);

                unitOfwork.Commit();

                if (!string.IsNullOrEmpty(strDeveloperIds))
                {
                    var strArrayDevelopers = strDeveloperIds.Split(',');
                    foreach (var causeBugDeveloper in strArrayDevelopers.Select(str => new CauseBugDeveloper
                    {
                        DeveloperId = Convert.ToInt32(str),
                        BugId = bug.BugId
                    }))
                    {
                        _causeBugDeveloperRepository.Create(causeBugDeveloper);
                    }
                }

                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    foreach (var document in strArrayDocuments.Select(str => new Document
                    {
                        BugId = bug.BugId,
                        Path = str
                    }))
                    {
                        _documentRepository.Create(document);
                    }
                }

                unitOfwork.Commit();
            }
        }

        public void Edit(BugLogicModel model, string strDeveloperIds, string strDocuments)
        {
            var bug = model.ConvertToBug();
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Edit(bug);

                var causeBugDevelopers = _causeBugDeveloperRepository.Query().Where(n => n.BugId == model.BugId);
                if (causeBugDevelopers.Any())
                {
                    foreach (var d in causeBugDevelopers)
                    {
                        _causeBugDeveloperRepository.Delete(d.Id);
                    }
                }

                if (!string.IsNullOrEmpty(strDeveloperIds))
                {
                    var strArrayDevelopers = strDeveloperIds.Split(',');
                    foreach (var causeBugDeveloper in strArrayDevelopers.Select(str => new CauseBugDeveloper
                    {
                        DeveloperId = Convert.ToInt32(str),
                        BugId = model.BugId
                    }))
                    {
                        _causeBugDeveloperRepository.Create(causeBugDeveloper);
                    }
                }

                var documents = _documentRepository.Query().Where(n => n.BugId == model.BugId);
                if (documents.Any())
                {
                    foreach (var d in documents)
                    {
                        _documentRepository.Delete(d.DocumentId);
                    }
                }

                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    foreach (var str in strArrayDocuments)
                    {
                        var document = new Document
                        {
                            BugId = model.BugId,
                            Path = str
                        };
                        _documentRepository.Create(document);
                    }
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
            var bug = _bugRepository.Query().FirstOrDefault(n => n.BugId == id);
            return bug.ConvertToBugLogicModel();
        }

        public List<BugLogicModel> GetAll()
        {
            var model = _bugRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToBugLogicModel()).ToList();
        }

        public List<BugLogicModel> GetBugByWhereCondition(string whereCondition)
        {
            var model = _bugRepository.Query().Where(n => string.IsNullOrEmpty(whereCondition) || n.Title.Contains(whereCondition) || n.Smmary.Contains(whereCondition) || n.Status.Contains(whereCondition) || n.Project.ProjectName.Contains(whereCondition));
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToBugLogicModel()).ToList();
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
