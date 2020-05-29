using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public class PagedList<T>:List<T>
    {

        public int CurrentPage { get; private set; }


        public int TotalPages { get; private set; }


        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPrevious => (CurrentPage > 1);


        public bool HasNext => (CurrentPage < TotalPages);


        public PagedList(List<T>item,int count,int pageNo, int pageSize)
        {
            TotalCount = count;
            this.PageSize = pageSize;
            this.CurrentPage = pageNo;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(item);

        }


        public static PagedList<T> Create(IQueryable<T>source, int pageNo, int PageSize)
        {
            var count = source.Count();
            var item = source.Skip((pageNo - 1) * PageSize).Take(PageSize).ToList();

            return new PagedList<T>(item, count, pageNo, PageSize);
        }
    }
}
