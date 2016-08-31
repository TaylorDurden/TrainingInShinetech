using BugManagement.Data;
using BugManagement.Data.Models;
using BugManagement.IRepository;

namespace BugManagement.Repository
{
    public class AttachmentRepository:RepositoryBase<Attachment>,IAttachmentRepository
    {
        private readonly IDbFactory _dbFactory;

        public AttachmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}
