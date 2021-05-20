using System;

namespace Barbuuuda.Models.Pagination.Output
{
    /// <summary>
    /// Класс пагинации.
    /// </summary>
    public class PaginationOutput
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PaginationOutput(int count, int pageNumber, int pageSize)
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
