namespace Barbuuuda.Models.Task.Input
{
    /// <summary>
    /// Класс входной модели задания.
    /// </summary>
    public class TaskInput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public long? TaskId { get; set; }

        /// <summary>
        /// Параметр получения заданий либо все либо одно.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Статус задания, задания которого нужно получить.
        /// </summary>
        public string Status { get; set; }
    }
}
