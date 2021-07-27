namespace Barbuuuda.Models.Task.Input
{
    /// <summary>
    /// Класс входной модели переода.
    /// </summary>
    public class TransitionInput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Параметр получения заданий либо все либо одно.
        /// </summary>
        public string Type { get; set; }
    }
}
