using System.Collections.Generic;
using BugManagement.Data.Models;
using BugManagement.ILogic;
using BugManagement.IRepository;
using BugManagement.Repository.UnitOfWork;

namespace BugManagement.Logic
{
    public class AttachmentLogic : IAttachmentLogic
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public AttachmentLogic(IAttachmentRepository attachmentRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _attachmentRepository = attachmentRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(Attachment model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _attachmentRepository.Ins(model);
                unitOfWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _attachmentRepository.Del(id);
                unitOfWork.Commit();
            }
        }

        public void Edit(Attachment model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _attachmentRepository.Upd(model);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<Attachment> GetAll()
        {
            return _attachmentRepository.Query();
        }
    }
}
