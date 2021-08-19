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
        /// <returns>Данные пагинации.</returns>
        Task<IndexOutput> GetInitPaginationAuctionTasks(int pageIdx);

        /// <summary>
        /// Метод пагинации всех заданий аукциона.
        /// </summary>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="countRows">Кол-во строк.</param>
        /// <returns>Данные пагинации.</returns>
        Task<IndexOutput> GetPaginationAuction(int pageNumber, int countRows);

        /// <summary>
        /// Метод пагинации на ините страницы мои задания у заказчика.
        /// </summary>
        /// <param name="pageIdx">Номер страницы.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Данные пагинации.</returns>
        Task<IndexOutput> InitMyCustomerPaginationAsync(int pageIdx, string account);

        /// <summary>
        /// Метод пагинации страницы мои задания у заказчика.
        /// </summary>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="countRows">Кол-во строк.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Данные пагинации.</returns>
        Task<IndexOutput> GetMyCustomerPaginationAsync(int pageNumber, int countRows, string account);
    }
}
