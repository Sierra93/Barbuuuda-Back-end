using System.Threading.Tasks;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы платежной системы.
    /// </summary>
    public sealed class PaymentService : IPaymentService
    {
        private readonly PostgreDbContext _postgre;

        public PaymentService(PostgreDbContext postgre)
        {
            _postgre = postgre;
        }

        /// <summary>
        /// Метод полполнит счет на сервисе.
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="account">Логин пользователя.</param>
        public Task RefillBalance(decimal amount, string currency, string account)
        {
            throw new System.NotImplementedException();
        }
    }
}
