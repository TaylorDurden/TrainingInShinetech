using System;
using System.Collections.Generic;
using System.Linq;

namespace BugManagemnet.WebAPI.Models
{
    public class PagingInfo<T> where T : class
    {
        public IEnumerable<T> DataSource { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public bool HasPrev => PageIndex > 1;
        public bool HasNext => PageIndex < PageCount;

        public PagingInfo(int pageSize, IEnumerable<T> dataSource)
        {
            PageSize = pageSize > 1 ? pageSize : 1;
            var source = dataSource as T[] ?? dataSource.ToArray();
            DataSource = source;
            PageCount = (int)Math.Ceiling(source.Count() / (double)pageSize);
        }

        public List<T> GetPagingData()
        {
            return DataSource.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}