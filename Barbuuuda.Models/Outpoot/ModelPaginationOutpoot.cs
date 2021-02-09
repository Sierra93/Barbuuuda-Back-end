using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Models.Outpoot
{
    /// <summary>
    /// Класс пагинации.
    /// </summary>
    public class ModelPaginationOutpoot
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public ModelPaginationOutpoot(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
