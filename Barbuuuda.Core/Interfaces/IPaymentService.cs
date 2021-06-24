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
        /// Метод получает сумму средств на балансе текущего пользователя.
        /// </summary>
        /// <param name="account">Логин текущего пользователя.</param>
        /// <returns>Сумма баланса.</returns>
        Task<decimal> GetBalanceAsync(string account);

        /// <summary>
        /// Метод инициализирует конфигурацию платежный виджет фронта данными.
        /// </summary>
        /// <param name="amount">Сумма.</param>
        /// <param name="taskId">Id задания.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Объект с данными конфигурации виджета.</returns>
        Task<PaymentWidgetOutput> InitPaymentAsync(decimal amount, long? taskId, string currency, string account);
    }
}
