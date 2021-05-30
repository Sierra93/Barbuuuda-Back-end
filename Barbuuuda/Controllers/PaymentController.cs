using System.Threading.Tasks;
using Barbuuuda.Commerces.Core;
using Barbuuuda.Commerces.Models.PayPal.Input;
using Barbuuuda.Commerces.Models.PayPal.Output;
using Barbuuuda.Core.Interfaces;
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
        private readonly IPaymentService _paymentService;

        public PaymentController(IPayPalService payService, IPaymentService paymentService)
        {
            _payPalService = payService;
            _paymentService = paymentService;
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
        [ProducesResponseType(200, Type = typeof(HttpResponse))]
        public async Task<IActionResult> CaptureTransactionAsync([FromBody] CaptureTransactionInput captureInput)
        {
            HttpResponse capture = await _payPalService.CaptureTransactionAsync(captureInput.OrderId, GetUserName());

            return Ok(capture);
        }

        /// <summary>
        /// Метод получает сумму средств на балансе текущего пользователя.
        /// </summary>
        /// <returns>Сумма баланса.</returns>
        [HttpPost, Route("balance")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        public async Task<IActionResult> GetBalanceAsync()
        {
            decimal balanceAmount = await _paymentService.GetBalanceAsync(GetUserName());

            return Ok(balanceAmount);
        }
    }
}
