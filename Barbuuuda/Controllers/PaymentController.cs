using System.Threading.Tasks;
using Barbuuuda.Commerces.Core;
using Barbuuuda.Commerces.Models.PayPal.Input;
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
        [HttpPost, Route("setup-transaction")]
        [ProducesResponseType(200, Type = typeof(SetupTransactionOutput))]
        public async Task<IActionResult> SetupTransactionAsync()
        {
            SetupTransactionOutput transaction = await _payPalService.SetupTransactionAsync();

            return Ok(transaction);
        }

        /// <summary>
        /// Метод собирает средства от транзакции после того, как покупатель одобряет транзакцию.
        /// </summary>
        /// <param name="captureInput">Входная модель.</param>
        /// <returns>Данные от сбора транзакции.</returns>
        [HttpPost, Route("capture-transaction")]
        public async Task<IActionResult> CaptureTransactionAsync([FromBody] CaptureTransactionInput captureInput)
        {
            var capture = await _payPalService.CaptureTransactionAsync(captureInput.OrderId);

            return Ok(capture);
        }
    }
}
