namespace Barbuuuda.Models.Task.Input
{
    /// <summary>
    /// Класс водной модели задания.
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
    }
}
