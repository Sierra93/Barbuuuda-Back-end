using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Payment.Input;
using Barbuuuda.Models.Payment.Output;
using Barbuuuda.Models.User.Input;
using Barbuuuda.Models.User.Output;
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
        [ProducesResponseType(200, Type = typeof(UserOutput))]
        public async Task<IActionResult> GetBalanceAsync([FromBody] UserInput user)
        {
            var balanceAmount = await _paymentService.GetBalanceAsync(user.UserName);

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

        /// <summary>
        /// Метод действий после успешной оплаты.
        /// Запишет данные транзакции в БД в таблицу заказов или таблицу счетов.
        /// </summary>
        /// <param name="paymentSuccessInput">Данные транзакции, которые вернула платежная система.</param>
        /// <returns>Вернет роут по success url.</returns>
        [AllowAnonymous]
        [HttpPost, Route("check")]
        public async Task<IActionResult> CheckPaymentTestAsync([FromForm] PaymentSuccessInput paymentSuccessInput)
        {
            await _paymentService.RefillBalanceAsync(paymentSuccessInput);

            // TODO: Менять при тесте на http://localhost:8080/home или для прода https://barbuuuda.ru
            return new RedirectResult("http://localhost:8080/home");
        }
    }
}
