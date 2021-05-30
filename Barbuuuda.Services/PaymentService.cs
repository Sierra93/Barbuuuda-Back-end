using System;
using System.Linq;
using System.Threading.Tasks;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Entities.Payment;
using Microsoft.EntityFrameworkCore;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы платежной системы.
    /// </summary>
    public sealed class PaymentService : IPaymentService
    {
        private readonly PostgreDbContext _postgre;
        private readonly IUserService _user;

        public PaymentService(PostgreDbContext postgre, IUserService user)
        {
            _postgre = postgre;
            _user = user;
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

                // Найдет UserId.
                string userId = await _user.GetUserIdByLogin(account);

                if (!string.IsNullOrEmpty(userId))
                {
                    // Ищет счет пользователя по UserId.
                    InvoiceEntity invoice = await _postgre.Invoices
                        .Where(i => i.UserId
                        .Equals(userId) && i.Currency
                        .Equals(currency))
                        .FirstOrDefaultAsync();

                    // Если счет пользователя в текущей валюте найден, то запишет средства на этот счет в этой валюте.
                    if (invoice != null)
                    {
                        invoice.InvoiceAmount += amount;
                        await _postgre.SaveChangesAsync();
                    }

                    // Счета у пользователя в этой валюте не найдено, значит создаст счет в этой валюте.
                    else
                    {
                        await _postgre.Invoices.AddAsync(new InvoiceEntity()
                        {
                            UserId = userId,
                            InvoiceAmount = 0,
                            Currency = currency,
                            ScoreNumber = null,
                            ScoreEmail = string.Empty
                        });
                        await _postgre.SaveChangesAsync();
                    }
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
