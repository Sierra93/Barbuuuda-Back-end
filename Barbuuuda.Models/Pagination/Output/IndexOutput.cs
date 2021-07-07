using Barbuuuda.Models.Pagination.Output;
using System.Collections;

namespace Barbuuuda.Models.Pagination.Output
{
    /// <summary>
    /// Класс с информацией о пагинации.
    /// </summary>
    public class IndexOutput
    {
        public IEnumerable Tasks { get; set; }

        public PaginationOutput PageData { get; set; }
    }
}
