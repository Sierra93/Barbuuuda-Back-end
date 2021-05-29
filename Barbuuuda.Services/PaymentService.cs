using System;
using System.Threading.Tasks;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
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
        /// <param name="scoreEmail">Адрес выставления счета.</param>
        /// <param name="account">Логин пользователя.</param>
        public async Task RefillBalance(decimal amount, string currency, string scoreEmail, string account)
        {
            try
            {
                // Проверка входных параметров перед пополнением баланса счета.
                if (amount < 0 || string.IsNullOrEmpty(currency))
                {
                    throw new EmptyInvoiceParameterException();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
