using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Payment.Input;
using Barbuuuda.Models.Payment.Output;
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
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
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

        /// <summary>
        /// Метод инициализирует конфигурацию платежный виджет фронта данными.
        /// </summary>
        /// <returns>Объект с данными конфигурации виджета.</returns>
        [HttpPost, Route("init")]
        [ProducesResponseType(200, Type = typeof(PaymentWidgetOutput))]
        public async Task<IActionResult> InitPaymentAsync([FromBody] PaymentWidgetInput input)
        {
            PaymentWidgetOutput result = await _paymentService.InitPaymentAsync(input.Amount, input.TaskId, input.Currency, GetUserName());

            return Ok(result);
        }
    }
}
