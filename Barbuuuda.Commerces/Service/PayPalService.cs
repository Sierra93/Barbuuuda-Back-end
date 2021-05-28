using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Barbuuuda.Commerces.Core;
using Barbuuuda.Commerces.Data;
using Barbuuuda.Commerces.Exceptions;
using Barbuuuda.Commerces.Models.PayPal.Output;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace Barbuuuda.Commerces.Service
{
    /// <summary>
    /// Сервис создает заказ на оплату.
    /// </summary>
    public sealed class PayPalService : IPayPalService
    {
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
        /// <returns>Данные от сбора транзакции.</returns>
        public async Task<HttpResponse> CaptureTransactionAsync(string orderId)
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
                return capture.Status.Equals("COMPLETED") ? response : throw new NotCaptureOrderByOrderId(orderId);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

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
                    BrandName = "EXAMPLE INC",
                    LandingPage = "BILLING",
                    UserAction = "CONTINUE",
                    ShippingPreference = "SET_PROVIDED_ADDRESS"
                },
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                  new PurchaseUnitRequest {
                    ReferenceId =  "PUHF",
                    Description = "Sporting Goods",
                    CustomId = "CUST-HighFashions",
                    SoftDescriptor = "HighFashions",
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                      CurrencyCode = "RUB",
                      Value = "230.00",
                      AmountBreakdown = new AmountBreakdown
                      {
                        ItemTotal = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "180.00"
                        },
                        Shipping = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "30.00"
                        },
                        Handling = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "10.00"
                        },
                        TaxTotal = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "20.00"
                        },
                        ShippingDiscount = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "10.00"
                        }
                      }
                    },
                    Items = new List<Item>
                    {
                      new Item
                      {
                        Name = "T-shirt",
                        Description = "Green XL",
                        Sku = "sku01",
                        UnitAmount = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "90.00"
                        },
                        Tax = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "10.00"
                        },
                        Quantity = "1",
                        Category = "PHYSICAL_GOODS"
                      },
                      new Item
                      {
                        Name = "Shoes",
                        Description = "Running, Size 10.5",
                        Sku = "sku02",
                        UnitAmount = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "45.00"
                        },
                        Tax = new Money
                        {
                          CurrencyCode = "RUB",
                          Value = "5.00"
                        },
                        Quantity = "2",
                        Category = "PHYSICAL_GOODS"
                      }
                    },
                    ShippingDetail = new ShippingDetail()
                    {
                      Name = new Name
                      {
                        FullName = "John Doe"
                      },
                      AddressPortable = new AddressPortable
                      {
                        AddressLine1 = "123 Townsend St",
                        AddressLine2 = "Floor 6",
                        AdminArea2 = "San Francisco",
                        AdminArea1 = "CA",
                        PostalCode = "94107",
                        CountryCode = "RU"
                      }
                    }
                  }
                }
            };

            return orderRequest;
        }
    }
}
