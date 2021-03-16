namespace Barbuuuda.Models.Executor.Input
{
    /// <summary>
    /// Входная модель задания.
    /// </summary>
    public class TaskInput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Сообщение к ставке исполнителя.
        /// </summary>
        public string RespondMessage { get; set; }

        /// <summary>
        /// Запомнить сообщение как шаблон.
        /// </summary>
        public bool IsTemplate { get; set; }
    }
}
