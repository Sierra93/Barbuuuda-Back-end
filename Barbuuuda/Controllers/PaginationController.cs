using Barbuuuda.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Barbuuuda.Models.Pagination.Input;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер работы с пагинацией.
    /// </summary>
    [ApiController, Route("pagination")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaginationController : BaseController
    {
        /// <summary>
        /// Абстракция сервиса пагинации.
        /// </summary>
        private readonly IPaginationService _paginationService;

        public PaginationController(IPaginationService paginationService)
        {
            _paginationService = paginationService;
        }

        /// <summary>
        /// Метод пагинации для инита аукциона.
        /// </summary>
        /// <param name="pageIdx">Номер страницы. По дефолту 1.</param>
        /// <returns>Данные пагинации.</returns>
        [HttpPost, Route("init-auction")]
        public async Task<IActionResult> GetInitPaginationAuctionTasksAsync([FromBody] PaginationInput paginationInput)
        {
            var paginationData = await _paginationService.GetInitPaginationAuctionTasks(paginationInput.PageNumber);

            return Ok(paginationData);
        }

        /// <summary>
        /// Метод пагинации всех заданий аукциона.
        /// </summary>
        /// <param name="paginationInput">Входная модель.</param>
        /// <returns>Данные пагинации.</returns>
        [HttpPost, Route("auction")]
        public async Task<IActionResult> GetPaginationAuctionAsync([FromBody] PaginationInput paginationInput)
        {
            var paginationData = await _paginationService.GetPaginationAuction(paginationInput.PageNumber, paginationInput.CountRows);

            return Ok(paginationData);
        }

        /// <summary>
        /// Метод пагинации всех заданий в работе у исполнителя.
        /// </summary>
        /// <param name="paginationInput">Входная модель.</param>
        /// <returns>Данные пагинации.</returns>
        [HttpPost, Route("work")]
        public async Task<IActionResult> GetPaginationWorkAsync([FromBody] PaginationInput paginationInput)
        {
            var paginationData = await _paginationService.GetPaginationWorkAsync(paginationInput.PageNumber, paginationInput.CountRows, GetUserName());

            return Ok(paginationData);
        }

        /// <summary>
        /// Метод пагинации всех заданий заказчика.
        /// </summary>
        /// <param name="paginationInput">Входная модель.</param>
        /// <returns>Данные пагинации.</returns>
        [HttpPost, Route("customer")]
        public async Task<IActionResult> GetPaginationCustomerAsync([FromBody] PaginationInput paginationInput)
        {
            return Ok();
        }
    }
}
