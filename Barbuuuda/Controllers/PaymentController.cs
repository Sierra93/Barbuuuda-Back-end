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
        /// TODO: Подумать, нужен ли метод RefillBalanceAsync ведь этот можно доработать!!! Но это не точно!!!
        /// Метод инициализирует конфигурацию платежный виджет фронта данными.
        /// </summary>
        /// <returns>Объект с данными конфигурации виджета.</returns>
        [HttpPost, Route("init")]
        [ProducesResponseType(200, Type = typeof(PaymentWidgetOutput))]
        public async Task<IActionResult> InitPaymentAsync()
        {
            PaymentWidgetOutput result = await _paymentService.InitPaymentAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод пополнит счет пользователя на сервисе в переданной валюте, либо создаст новый счет в этой валюте.
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Флаг успеха пополнения счета.</returns>
        [HttpPost, Route("refill")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> RefillBalanceAsync([FromBody] PaymentWidgetInput input)
        {
            bool result = await _paymentService.RefillBalanceAsync(input.Amount, input.Currency, GetUserName());

            return Ok(result);
        }
    }
}
