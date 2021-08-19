using Barbuuuda.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Barbuuuda.Models.Pagination.Input;
using Barbuuuda.Models.Pagination.Output;

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
        [ProducesResponseType(200, Type = typeof(IndexOutput))]
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
        [ProducesResponseType(200, Type = typeof(IndexOutput))]
        public async Task<IActionResult> GetPaginationAuctionAsync([FromBody] PaginationInput paginationInput)
        {
            var paginationData = await _paginationService.GetPaginationAuction(paginationInput.PageNumber, paginationInput.CountRows);

            return Ok(paginationData);
        }

        /// <summary>
        /// Метод пагинации на ините страницы мои задания у заказчика.
        /// </summary>
        /// <param name="paginationInput">Входная модель.</param>
        /// <returns>Данные пагинации.</returns>
        [HttpPost, Route("init-my-customer")]
        [ProducesResponseType(200, Type = typeof(IndexOutput))]
        public async Task<IActionResult> InitMyCustomerPaginationAsync([FromBody] PaginationInput paginationInput)
        {
            var paginationData = await _paginationService.InitMyCustomerPaginationAsync(paginationInput.PageNumber, GetUserName());

            return Ok(paginationData);
        }

        /// <summary>
        /// Метод пагинации страницы мои задания у заказчика.
        /// </summary>
        /// <param name="paginationInput">Входная модель.</param>
        /// <returns>Данные пагинации.</returns>
        [HttpPost, Route("my-customer")]
        [ProducesResponseType(200, Type = typeof(IndexOutput))]
        public async Task<IActionResult> GetMyCustomerPaginationAsync([FromBody] PaginationInput paginationInput)
        {
            var paginationData = await _paginationService.GetMyCustomerPaginationAsync(paginationInput.PageNumber, paginationInput.CountRows, GetUserName());

            return Ok(paginationData);
        }
    }
}
