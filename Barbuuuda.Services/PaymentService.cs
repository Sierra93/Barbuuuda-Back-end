using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Entities.Customer;
using Barbuuuda.Models.Entities.Payment;
using Barbuuuda.Models.Payment.Output;
using Microsoft.EntityFrameworkCore;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы платежной системы.
    /// </summary>
    public sealed class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IUserService _user;

        public PaymentService(ApplicationDbContext db, PostgreDbContext postgre, IUserService user)
        {
            _db = db;
            _postgre = postgre;
            _user = user;
        }

        /// <summary>
        /// Метод пополнит счет пользователя на сервисе в переданной валюте, либо создаст новый счет в этой валюте.
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Флаг успеха пополнения счета.</returns>
        private async Task<bool> RefillBalanceAsync(decimal amount, string currency, string userId)
        {
            try
            {

                // Проверка входных параметров перед пополнением баланса счета.
                if (amount < 0 || string.IsNullOrEmpty(currency))
                {
                    throw new EmptyInvoiceParameterException();
                }

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
                    await _postgre.Invoices.AddAsync(new InvoiceEntity
                    {
                        UserId = userId,
                        InvoiceAmount = 0,
                        Currency = currency,
                        ScoreNumber = null,
                        ScoreEmail = string.Empty
                    });
                    await _postgre.SaveChangesAsync();
                }

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message, ex.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод получает сумму средств на балансе текущего пользователя.
        /// </summary>
        /// <param name="account">Логин текущего пользователя.</param>
        /// <returns>Сумма баланса.</returns>
        public async Task<decimal> GetBalanceAsync(string account)
        {
            try
            {
                string userId = await _user.GetUserIdByLogin(account);
                decimal balanceAmount = await _postgre.Invoices
                    .Where(a => a.UserId
                    .Equals(userId))
                    .Select(res => res.InvoiceAmount)
                    .FirstOrDefaultAsync();

                return balanceAmount;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger logger = new Logger(_db, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод инициализирует конфигурацию платежный виджет фронта данными.
        /// </summary>
        /// <param name="amount">Сумма.</param>
        /// <param name="taskId">Id задания.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Объект с данными конфигурации виджета.</returns>
        public async Task<PaymentWidgetOutput> InitPaymentAsync(decimal amount, long? taskId, string currency, string account)
        {
            try
            {
                // Найдет Id пользователя по его логину.
                string userId = await _user.GetUserIdByLogin(account);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new NotFoundUserException(account);
                }

                if (amount <= 0)
                {
                    throw new EmptyAmountException();
                }

                // Если валюта не передана, значит выставить ее по дефолту в RUB.
                if (string.IsNullOrEmpty(currency))
                {
                    currency = CurrencyType.CURRENCY_RUB;
                }

                OrderEntity order;
                PaymentWidgetOutput paymentWidget;

                // Если передан Id задания, значит идет оплата задания.
                if (taskId != null && taskId > 0)
                {
                    order = new OrderEntity
                    {
                        Id = userId,
                        Amount = amount,
                        TaskId = taskId,
                        Currency = currency
                    };

                    // Сформировать шаблон виджета для оплаты задания на сервисе.
                    paymentWidget = new PaymentWidgetOutput
                    {
                        Version = "1.0",

                        Invoice = new Invoice
                        {
                            Description = "Оплата задания на сервисе Barbuuuda",
                            Amount = amount.ToString(CultureInfo.CurrentCulture),
                            MerchantId = "02d2f902-e614-43d0-a0b6-2026b9923932",
                            TaskId = taskId,
                            Account = account,
                            Currency = currency,
                            Refill = "false"
                        },

                        PaymentForm = new PaymentForm
                        {
                            Title = "Данные об оплате. Информация об оплате будет выслана на вашу почту.",

                            Value = new Value
                            {
                                Description = new Description
                                {
                                    Type = "textarea",
                                    Label = "Наименование услуги"
                                },

                                Amount = new Amount
                                {
                                    Type = "input",
                                    Label = "Сумма оплаты",
                                    Placeholder = string.Empty,
                                    Access = "readonly"
                                },

                                MerchantId = new MerchantId
                                {
                                    Type = "input"
                                },

                                TaskId = new TaskId
                                {
                                    Type = "number",
                                    Label = "ID задания",
                                    Placeholder = string.Empty,
                                    Access = "readonly"
                                },

                                Account = new Account
                                {
                                    Type = "textarea",
                                    Label = "Login",
                                    Placeholder = string.Empty,
                                    Access = "hidden"
                                },

                                Currency = new Currency
                                {
                                    Type = "textarea",
                                    Label = "Currency",
                                    Placeholder = string.Empty,
                                    Access = "hidden"
                                },

                                Refill = new Refill
                                {
                                    Type = "textarea",
                                    Label = "Флаг пополнения счета",
                                    Placeholder = string.Empty,
                                    Access = "hidden"
                                }
                            }
                        },

                        Payment = new Payment
                        {
                            Title = null,
                            SubmitText = "Оплатить",
                            AllowExternal = false,
                            Methods = new List<string> { "test" }
                        }
                    };
                }

                // Если не передан Id задания, значит идет пополнение счета.
                else
                {
                    order = new OrderEntity
                    {
                        Id = userId,
                        Amount = amount,
                        Currency = currency
                    };

                    // Сформировать шаблон виджета для пополнения счета на сервисе.
                    paymentWidget = new PaymentWidgetOutput
                    {
                        Version = "1.0",

                        Invoice = new Invoice
                        {
                            Description = "Пополнение счета на сервисе Barbuuuda",
                            Amount = amount.ToString(CultureInfo.CurrentCulture),
                            MerchantId = "02d2f902-e614-43d0-a0b6-2026b9923932",
                            TaskId = 1,
                            Account = account,
                            Currency = currency,
                            Refill = "true"
                        },

                        PaymentForm = new PaymentForm
                        {
                            Title = "Данные о пополнении счета. Информация о пополнении счета будет выслана на вашу почту.",

                            Value = new Value
                            {
                                Description = new Description
                                {
                                    Type = "textarea",
                                    Label = "Наименование услуги"
                                },

                                Amount = new Amount
                                {
                                    Type = "input",
                                    Label = "Сумма пополнения",
                                    Placeholder = string.Empty,
                                    Access = "readonly"
                                },

                                MerchantId = new MerchantId
                                {
                                    Type = "input"
                                },

                                TaskId = new TaskId
                                {
                                    Type = "number",
                                    Label = "ID задания",
                                    Placeholder = string.Empty,
                                    Access = "hidden"
                                },

                                Account = new Account
                                {
                                    Type = "textarea",
                                    Label = "Login",
                                    Placeholder = string.Empty,
                                    Access = "hidden"
                                },

                                Currency = new Currency
                                {
                                    Type = "textarea",
                                    Label = "Currency",
                                    Placeholder = string.Empty,
                                    Access = "hidden"
                                },

                                Refill = new Refill
                                {
                                    Type = "textarea",
                                    Label = "Флаг пополнения счета",
                                    Placeholder = string.Empty,
                                    Access = "hidden"
                                }
                            }
                        },

                        Payment = new Payment
                        {
                            Title = null,
                            SubmitText = "Пополнить",
                            AllowExternal = false,
                            Methods = new List<string> { "test" }
                        }
                    };
                }

                // Запишет заказ пользователя в таблицу заказов.
                await _postgre.Orders.AddAsync(order);
                await _postgre.SaveChangesAsync();

                // Найдет последний заказ и возьмет его OrderId.
                //long orderId = await _postgre.Orders.MaxAsync(x => x.OrderId);

                // Пополнит счет пользователя на сервисе.
                //bool isRefill = await RefillBalanceAsync(amount, currency, userId);

                //// Если пополнение счета не прошло.
                //if (!isRefill)
                //{
                //    throw new ErrorRefillScoreException();
                //}

                // Вернет конфигурацию виджета оплаты и продолжит процесс оплаты на фронте.
                // TODO: По хорошему бы все работы по оплате должны быть на бэке, но пока не понятно как брать статус выполнения после оплаты через виджет на фронте.
                //PaymentWidgetOutput result = new PaymentWidgetOutput
                //{
                //    Element = "app-widget",
                //    Widget = 8036,
                //    Destination = orderId.ToString(),
                //    Amount = amount 
                //};

                return paymentWidget;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger logger = new Logger(_db, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }
    }
}
