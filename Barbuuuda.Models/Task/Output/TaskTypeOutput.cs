using System.Text.Json.Serialization;

namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели типов заданий.
    /// </summary>
    public class TaskTypeOutput
    {
        /// <summary>
        /// Тип задания для про или дял всех.
        /// </summary>
        [JsonPropertyName("type_name")]
        public string TypeName { get; set; }

        /// <summary>
        /// Код типа задания.
        /// </summary>
        [JsonPropertyName("type_code")]
        public string TypeCode { get; set; }
    }
}
