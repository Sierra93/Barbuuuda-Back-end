using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Barbuuuda.Commerces.Core;
using Barbuuuda.Commerces.Data;
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
        /// Метод создает заказ на оплату.
        /// </summary>
        /// <returns>Данные заказа на оплату.</returns>
        public async Task<HttpResponse> CreateOrderAsync()
        {
            try
            {
                // Формирует запрос с заголовками.
                OrdersCreateRequest request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(BuildRequestBody());

                // Настроит транзакцию.
                HttpResponse response = await ClientConfigure.Client().Execute(request);

                return response;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<HttpResponse> CaptureOrderAsync(string orderId)
        {
            try
            {
                // Формирует запрос с заголовками.
                OrdersCaptureRequest request = new OrdersCaptureRequest(orderId);
                request.Prefer("return=representation");
                request.RequestBody(new OrderActionRequest());

                // Настроит транзакцию.
                HttpResponse response = await ClientConfigure.Client().Execute(request);

                return response;
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
                      CurrencyCode = "USD",
                      Value = "230.00",
                      AmountBreakdown = new AmountBreakdown
                      {
                        ItemTotal = new Money
                        {
                          CurrencyCode = "USD",
                          Value = "180.00"
                        },
                        Shipping = new Money
                        {
                          CurrencyCode = "USD",
                          Value = "30.00"
                        },
                        Handling = new Money
                        {
                          CurrencyCode = "USD",
                          Value = "10.00"
                        },
                        TaxTotal = new Money
                        {
                          CurrencyCode = "USD",
                          Value = "20.00"
                        },
                        ShippingDiscount = new Money
                        {
                          CurrencyCode = "USD",
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
                          CurrencyCode = "USD",
                          Value = "90.00"
                        },
                        Tax = new Money
                        {
                          CurrencyCode = "USD",
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
                          CurrencyCode = "USD",
                          Value = "45.00"
                        },
                        Tax = new Money
                        {
                          CurrencyCode = "USD",
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
                        CountryCode = "US"
                      }
                    }
                  }
                }
            };

            return orderRequest;
        }
    }
}
