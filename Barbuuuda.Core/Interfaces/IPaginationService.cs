using Barbuuuda.Models.Pagination.Output;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция пагинации.
    /// </summary>
    public interface IPaginationService
    {
        /// <summary>
        /// Метод пагинации.
        /// </summary>
        /// <param name="pageIdx">Номер страницы.</param>
        /// <param name="userName">Имя юзера.</param>
        /// <returns>Данные пагинации.</returns>
        Task<IndexOutput> GetPaginationTasks(int pageIdx, string userName);

        /// <summary>
        /// Метод пагинации аукциона.
        /// </summary>
        /// <param name="pageIdx"></param>
        /// <returns>Данные пагинации.</returns>
        Task<IndexOutput> GetPaginationAuction(int pageIdx);
    }
}
