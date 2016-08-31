using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugManagement.Models
{
    public class PagingInfo<T> where T : class
    {
        public IEnumerable<T> DataSource { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int PageCount { get; set; }


        public bool HasPrev { get { return PageIndex > 1; } }

        public bool HasNext { get { return PageIndex < PageCount; } }

        public PagingInfo(int pageSize, IEnumerable<T> dataSource)
        {
            this.PageSize = pageSize > 1 ? pageSize : 1;
            this.DataSource = dataSource;
            PageCount = (int)Math.Ceiling(dataSource.Count() / (double)pageSize);
        }

        public IEnumerable<T> GetPagingData()
        {
            return DataSource.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        }
        
    }
}