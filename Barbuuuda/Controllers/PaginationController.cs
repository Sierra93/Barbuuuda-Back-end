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
        public async Task<IActionResult> GetInitPaginationAuctionTasks([FromBody] PaginationInput paginationInput)
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
        public async Task<IActionResult> GetPaginationAuction([FromBody] PaginationInput paginationInput)
        {
            var paginationData = await _paginationService.GetPaginationAuction(paginationInput.PageNumber, paginationInput.CountRows);

            return Ok(paginationData);
        }
    }
}
