using System.Threading.Tasks;
using Barbuuuda.Models.Payment.Output;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция платежной системы.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Метод пополнит счет пользователя на сервисе в переданной валюте, либо создаст новый счет в этой валюте.
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="scoreEmail">Адрес выставления счета.</param>
        /// <param name="account">Логин пользователя.</param>
        Task RefillBalance(decimal amount, string currency, string scoreEmail, string account);

        /// <summary>
        /// Метод получает сумму средств на балансе текущего пользователя.
        /// </summary>
        /// <param name="account">Логин текущего пользователя.</param>
        /// <returns>Сумма баланса.</returns>
        Task<decimal> GetBalanceAsync(string account);

        /// <summary>
        /// Метод инициализирует конфигурацию платежный виджет фронта данными.
        /// </summary>
        /// <returns>Объект с данными конфигурации виджета.</returns>
        Task<PaymentWidgetOutput> InitPaymentAsync();
    }
}
