using System.Text.Json.Serialization;

namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели статусов.
    /// </summary>
    public class TaskStatusOutput
    {
        /// <summary>
        /// Код статуса.
        /// </summary>
        [JsonPropertyName("status_code")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Название статуса.
        /// </summary>
        [JsonPropertyName("status_name")]
        public string StatusName { get; set; }
    }
}
