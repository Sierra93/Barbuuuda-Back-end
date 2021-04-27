using System;
using System.Text.Json.Serialization;

namespace Barbuuuda.Models.Chat.Outpoot
{
    /// <summary>
    /// Класс выходной модели для сообщений диалога.
    /// </summary>
    public class MessageOutpoot
    {
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        [JsonPropertyName("Message")]
        public string Message { get; set; }

        /// <summary>
        /// Id диалога, к которому принадлежит сообщение.
        /// </summary>
        [JsonPropertyName("DialogId")]
        public long? DialogId { get; set; }

        /// <summary>
        /// Дата написания сообщения.
        /// </summary>
        [JsonPropertyName("Created")]
        public string Created { get; set; }
    }
}
