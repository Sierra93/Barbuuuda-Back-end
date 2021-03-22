using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Pagination.Outpoot;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер работы с пагинацией.
    /// </summary>
    [ApiController, Route("pagination")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaginationController : BaseController
    {        
        public static string Module => "Barbuuuda.Pagination";

        /// <summary>
        /// Абстракция сервиса пагинации.
        /// </summary>
        private readonly IPagination _pagination;

        public PaginationController(IPagination pagination) : base(Module)
        {
            _pagination = pagination;
        }

        /// <summary>
        /// Метод пагинации.
        /// </summary>
        /// <param name="pageIdx">Номер страницы. По дефолту 1.</param>
        /// <returns>Данные пагинации.</returns>
        [HttpGet, Route("page")]
        public async Task<IActionResult> GetPaginationTasks(int pageIdx = 1)
        {
            IndexOutpoot paginationData = await _pagination.GetPaginationTasks(pageIdx, GetUserName());

            return Ok(paginationData);
        }

        /// <summary>
        /// Метод пагинации аукциона.
        /// </summary>
        /// <param name="pageIdx"></param>
        /// <returns>Данные пагинации.</returns>
        [HttpPost, Route("auction")]
        public async Task<IActionResult> GetPaginationAuction([FromQuery] int pageIdx)
        {
            var paginationData = await _pagination.GetPaginationAuction(pageIdx);

            return Ok(paginationData);
        }
    }
}
