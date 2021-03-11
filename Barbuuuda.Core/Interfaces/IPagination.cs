using Barbuuuda.Models.Outpoot;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция пагинации.
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// Метод пагинации.
        /// </summary>
        /// <param name="pageIdx">Номер страницы.</param>
        /// <param name="userName">Имя юзера.</param>
        /// <returns>Данные пагинации.</returns>
        Task<IndexOutpoot> GetPaginationTasks(int pageIdx, string userName);
    }
}
