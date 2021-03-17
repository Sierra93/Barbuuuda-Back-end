using Barbuuuda.Models.Pagination.Outpoot;
using System.Collections;

namespace Barbuuuda.Models.Pagination.Outpoot
{
    /// <summary>
    /// Класс с информацией о пагинации.
    /// </summary>
    public class IndexOutpoot
    {
        public IEnumerable Tasks { get; set; }

        public PaginationOutpoot PageData { get; set; }
    }
}
