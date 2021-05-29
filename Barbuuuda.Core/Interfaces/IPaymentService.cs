using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция платежной системы.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Метод полполнит счет на сервисе.
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="account">Логин пользователя.</param>
        Task RefillBalance(decimal amount, string currency, string account);
    }
}
