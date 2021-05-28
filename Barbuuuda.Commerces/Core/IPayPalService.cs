using System.Threading.Tasks;
using Barbuuuda.Commerces.Models.PayPal.Output;
using PayPalHttp;

namespace Barbuuuda.Commerces.Core
{
    /// <summary>
    /// Абстракция сервиса создания заказа на оплату.
    /// </summary>
    public interface IPayPalService
    {
        /// <summary>
        /// Метод настраивает транзакцию.
        /// </summary>
        /// <returns>Данные транзакции.</returns>
        Task<SetupTransactionOutput> SetupTransactionAsync();

        /// <summary>
        /// Метод собирает средства от транзакции после того, как покупатель одобряет транзакцию.
        /// </summary>
        /// <param name="orderId">Id заказа на оплату.</param>
        /// <returns>Данные от сбора транзакции.</returns>
        Task<HttpResponse> CaptureTransactionAsync(string orderId);
    }
}
