using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Barbuuuda.Commerces.Models.PayPal.Output
{
    /// <summary>
    /// Класс выходной модели настройки транзакции.
    /// </summary>
    public class SetupTransactionOutput
    {
        /// <summary>
        /// Id транзакции.
        /// </summary>
        [Required(ErrorMessage = "Не заполнен Id"), JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
