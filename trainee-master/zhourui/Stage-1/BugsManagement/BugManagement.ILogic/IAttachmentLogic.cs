using System.Collections.Generic;
using BugManagement.Data.Models;

namespace BugManagement.ILogic
{
    public interface IAttachmentLogic
    {
        void Create(Attachment model);
        void Delete(int id);
        void Edit(Attachment model);
        IEnumerable<Attachment> GetAll();
    }
}
