namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выодной модели переходов.
    /// </summary>
    public class TransitionOutput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Тип перехода.
        /// </summary>
        public string Type { get; set; }
    }
}
