using System.Threading.Tasks;
using Barbuuuda.Commerces.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPalHttp;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер платежной системы.
    /// </summary>
    [ApiController, Route("payment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : BaseController
    {
        private readonly IPayPalService _payPalService;

        public PaymentController(IPayPalService payService)
        {
            _payPalService = payService;
        }

        /// <summary>
        /// Метод собирает данные транзакции.
        /// </summary>
        /// <returns>Данные транзакции.</returns>
        [HttpPost, Route("create-order")]
        [ProducesResponseType(200, Type = typeof(HttpResponse))]
        public async Task<IActionResult> CreateOrderAsync()
        {
            HttpResponse result = await _payPalService.CreateOrderAsync();

            return Ok(result);
        }
    }
}
