using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ResourceParameters
{
    public class AuthorResourceParameters
    {
        const int maxPageSize = 10;
        public string mainCategory { get; set; }

        public string searchQuery { get; set; }

        public int pageNo { get; set; } = 1;


        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }

            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

    }
}
