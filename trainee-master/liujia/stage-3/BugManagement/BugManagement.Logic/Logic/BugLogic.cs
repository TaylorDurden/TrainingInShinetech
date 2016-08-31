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
    public class BugLogic:IBugLogic
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

        public void Create(Bug model, string strDeveloperIds, string strDocuments)
        {
            CauseBugDeveloper causeBugDeveloper;
            Document document;
            
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Create(model);

                unitOfwork.Commit();

                if (!string.IsNullOrEmpty(strDeveloperIds))
                {
                    var strArrayDevelopers = strDeveloperIds.Split(',');
                    foreach (var str in strArrayDevelopers)
                    {
                        causeBugDeveloper = new CauseBugDeveloper();
                        causeBugDeveloper.DeveloperId = Convert.ToInt32(str);
                        causeBugDeveloper.BugId = model.BugId;
                        _causeBugDeveloperRepository.Create(causeBugDeveloper);
                    }
                }

                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    foreach (var str in strArrayDocuments)
                    {
                        document = new Document();
                        document.BugId = model.BugId;
                        document.Path = str;
                        _documentRepository.Create(document);
                    }
                }
                
                unitOfwork.Commit();
            }            
        }

        public void Edit(Bug model, string strDeveloperIds, string strDocuments)
        {
            IEnumerable<CauseBugDeveloper> causeBugDevelopers;
            IEnumerable<Document> documents;
            CauseBugDeveloper causeBugDeveloper;
            Document document;
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _bugRepository.Edit(model);

                causeBugDevelopers = _causeBugDeveloperRepository.Query().Where(n => n.BugId == model.BugId);
                if (causeBugDevelopers != null && causeBugDevelopers.Count() > 0)
                {
                    foreach (var d in causeBugDevelopers)
                    {
                        _causeBugDeveloperRepository.Delete(d.Id);
                    }
                }

                if (!string.IsNullOrEmpty(strDeveloperIds))
                {
                    var strArrayDevelopers = strDeveloperIds.Split(',');
                    foreach (var str in strArrayDevelopers)
                    {
                        causeBugDeveloper = new CauseBugDeveloper();
                        causeBugDeveloper.DeveloperId = Convert.ToInt32(str);
                        causeBugDeveloper.BugId = model.BugId;
                        _causeBugDeveloperRepository.Create(causeBugDeveloper);
                    }
                }

                documents = _documentRepository.Query().Where(n => n.BugId == model.BugId).AsEnumerable();
                if (documents != null && documents.Count() > 0)
                {
                    foreach (var d in documents.ToList())
                    {
                        _documentRepository.Delete(d.DocumentId);
                    }
                }
                
                if (!string.IsNullOrEmpty(strDocuments))
                {
                    var strArrayDocuments = strDocuments.Split(',');
                    foreach (var str in strArrayDocuments)
                    {
                        document = new Document();
                        document.BugId = model.BugId;
                        document.Path = str;
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

        public Bug Get(int id)
        {            
            return _bugRepository.Query().Where(n => n.BugId == id).FirstOrDefault();
        }

        public IEnumerable<Bug> GetAll()
        {
            return _bugRepository.Query().ToList();
        }

        public IEnumerable<Bug> GetBugByWhereCondition(string whereCondition)
        {            
            return _bugRepository.Query().Where(n => string.IsNullOrEmpty(whereCondition) || n.Title.Contains(whereCondition) || n.Smmary.Contains(whereCondition) || n.Status.Contains(whereCondition) || n.Project.ProjectName.Contains(whereCondition)).AsEnumerable();
        }

        public IEnumerable<Bug> GetBugBywhereConditionAndStatus(string whereCondition,string status)
        {
            return _bugRepository.Query().Where(n => (string.IsNullOrEmpty(whereCondition) || n.Title.Contains(whereCondition) || n.Smmary.Contains(whereCondition) || n.Status.Contains(whereCondition) || n.Project.ProjectName.Contains(whereCondition)) && n.Status == status).AsEnumerable();
        }

        public void UpdateBugStatus(int bugId,string stauts)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var dbBug = _bugRepository.Get(bugId);
                dbBug.Status = stauts;
                _bugRepository.Edit(dbBug);
            }
        }
    }
}
