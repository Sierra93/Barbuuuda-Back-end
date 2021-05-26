using System.Threading.Tasks;
using PayPalHttp;

namespace Barbuuuda.Commerces.Core
{
    /// <summary>
    /// Абстракция сервиса создания заказа на оплату.
    /// </summary>
    public interface IPayPalService
    {
        /// <summary>
        /// Метод создает заказ на оплату.
        /// </summary>
        /// <returns>Данные заказа на оплату.</returns>
        Task<HttpResponse> CreateOrderAsync();

        /// <summary>
        /// Метод собирает средства от транзакции после того, как покупатель одобряет транзакцию.
        /// </summary>
        /// <param name="orderId">Id заказа на оплату.</param>
        /// <returns>Данные от сбора транзакции.</returns>
        Task<HttpResponse> CaptureOrderAsync(string orderId);
    }
}
