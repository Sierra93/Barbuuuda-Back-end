using System.Threading.Tasks;
using Barbuuuda.Commerces.Core;
using Barbuuuda.Commerces.Models.PayPal.Output;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// Метод настраивает транзакцию.
        /// </summary>
        /// <returns>Данные транзакции.</returns>
        [HttpPost, Route("create-order")]
        [ProducesResponseType(200, Type = typeof(SetupTransactionOutput))]
        public async Task<IActionResult> SetupTransactionAsync()
        {
            SetupTransactionOutput transaction = await _payPalService.SetupTransactionAsync();

            return Ok(transaction);
        }
    }
}
