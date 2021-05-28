using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Barbuuuda.Commerces.Models.PayPal.Input
{
    /// <summary>
    /// Класс входной модели при захвате транзакции.
    /// </summary>
    public class CaptureTransactionInput
    {
        /// <summary>
        /// Id заказа.
        /// </summary>
        [Required, JsonPropertyName("orderID")]
        public string OrderId { get; set; }
    }
}
