using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barbuuuda.Commerces.Core;
using Barbuuuda.Commerces.Data;
using Barbuuuda.Commerces.Exceptions;
using Barbuuuda.Commerces.Models.PayPal.Output;
using Barbuuuda.Core.Interfaces;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace Barbuuuda.Commerces.Service
{
    /// <summary>
    /// Сервис создает заказ на оплату.
    /// </summary>
    public sealed class PayPalService : IPayPalService
    {
        private readonly IPaymentService _paymentService;

        public PayPalService(IPaymentService paymentServic)
        {
            _paymentService = paymentServic;
        }

        /// <summary>
        /// Метод настраивает транзакцию.
        /// </summary>
        /// <returns>Данные транзакции.</returns>
        public async Task<SetupTransactionOutput> SetupTransactionAsync()
        {
            try
            {
                // Формирует запрос с заголовками.
                OrdersCreateRequest request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(BuildRequestBody());

                // Настроит транзакцию.
                HttpResponse response = await ClientConfigure.Client().Execute(request);

                // Мапит к типу SetupTransactionOutput.
                MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Order, SetupTransactionOutput>());
                Mapper mapper = new Mapper(config);
                SetupTransactionOutput transaction = mapper.Map<SetupTransactionOutput>(response.Result<Order>());

                return !string.IsNullOrEmpty(transaction.Id) ? transaction : throw new NotCreateOrderException(transaction.Id);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        /// <summary>
        /// Метод собирает средства от транзакции после того, как покупатель одобряет транзакцию.
        /// </summary>
        /// <param name="orderId">Id заказа.</param>
        /// <param name="account">Логин пользователя, который пополняет свой счет.</param>
        /// <returns>Данные от сбора транзакции.</returns>
        public async Task<HttpResponse> CaptureTransactionAsync(string orderId, string account)
        {
            try
            {
                // Формирует запрос с заголовками.
                OrdersCaptureRequest request = new OrdersCaptureRequest(orderId);
                request.Prefer("return=representation");
                request.RequestBody(new OrderActionRequest());

                // Настроит транзакцию.
                HttpResponse response = await ClientConfigure.Client().Execute(request);
                Order capture = response.Result<Order>();

                // TODO: Вынести статусы в константы!
                // Если статус не успешно, то ошибка создания заказа на оплату.
                if (!capture.Status.Equals("COMPLETED"))
                {
                    throw new NotCaptureOrderByOrderId(orderId);
                }

                string scoreEmail = capture.Payer.Email;
                decimal amount = decimal.Parse(capture.PurchaseUnits.FirstOrDefault()?.AmountWithBreakdown.Value ?? string.Empty, CultureInfo.InvariantCulture);
                string currency = capture.PurchaseUnits.FirstOrDefault()?.AmountWithBreakdown.CurrencyCode;

                // Пополнит баланс счета пользователя сервиса.
                await _paymentService.RefillBalance(amount, currency, scoreEmail, account);

                return response;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// TODO: Доработать этот метод и тянуть с БД данные перед формированием транзакции!
        /// <summary>
        /// Метод формирует заказ на оплату.
        /// </summary>
        /// <returns>Данные задания.</returns>
        private OrderRequest BuildRequestBody()
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                // TODO: Вынести CAPTURE и AUTHORIZE в константы!
                // CAPTURE - зафиксировать платеж сразу.
                // AUTHORIZE - авторизовать платеж и приостановить перевод средств после того, как покупатель произведет платеж.
                CheckoutPaymentIntent = "CAPTURE",

                ApplicationContext = new ApplicationContext
                {
                    LandingPage = "BILLING",    // Тип целевой страницы, отображаемой на сайте PayPal для оплаты пользователем. Чтобы использовать целевую страницу, не относящуюся к учетной записи PayPal, установите значение Billing. Чтобы использовать целевую страницу входа в учетную запись PayPal, установите значение Login.
                    UserAction = "CONTINUE",
                    ShippingPreference = "NO_SHIPPING"  // Не показывать поля адреса при оплате через PayPal.
                },
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest {
                      AmountWithBreakdown = new AmountWithBreakdown
                    {
                      CurrencyCode = "RUB",
                      Value = "100"
                    },
                    ShippingDetail = new ShippingDetail()
                    {
                        Name = new Name
                      {
                          // Имя + Фамилия. Конкантенировать Имя и Фамилию (если с фронта пришло отдельными полями).
                          FullName = "John Doe"
                      },
                        AddressPortable = new AddressPortable
                      {
                        AddressLine1 = "123 Townsend St",
                        AddressLine2 = "Floor 6",
                        AdminArea2 = "San Francisco",
                        AdminArea1 = "CA",
                        PostalCode = "194107",
                        CountryCode = "RU"
                      }
                    }
                  }
                },
                Payer = new Payer()
                {
                    Email = "testmail@mail.ru",
                    PhoneWithType = new PhoneWithType()
                    {
                        // TODO: Вынести в константы тип телефона!
                        PhoneType = "MOBILE",
                        PhoneNumber = new Phone()
                        {
                            NationalNumber = "89856838046"
                        }
                    }
                }
            };

            return orderRequest;
        }
    }
}
