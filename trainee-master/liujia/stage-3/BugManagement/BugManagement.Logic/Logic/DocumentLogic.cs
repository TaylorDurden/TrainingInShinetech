using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;

namespace BugManagement.Logic.Logic
{
    public class DocumentLogic : IDocumentLogic
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IDocumentRepository _documentRepository;

        public DocumentLogic(IUnitOfWorkFactory unitOfWorkFactory, IDocumentRepository documentRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _documentRepository = documentRepository;
        }

        public IEnumerable<Document> GetDocumentByBugId(int bugId)
        {
            return _documentRepository.Query().Where(n => n.BugId == bugId).AsEnumerable();
        }
    }
}
