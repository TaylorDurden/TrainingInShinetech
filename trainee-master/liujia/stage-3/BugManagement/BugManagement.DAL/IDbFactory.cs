using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.DAL
{
    public interface IDbFactory
    {
        DbContext GetContext();
    }
}
