namespace Barbuuuda.Models.Executor.Input
{
    /// <summary>
    /// Класс входной модели принятия или отказа выполнения задания.
    /// </summary>
    public class AcceptOrCancelWorkTaskInput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public long TaskId { get; set; }
    }
}
