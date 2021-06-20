using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
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
    }
}
