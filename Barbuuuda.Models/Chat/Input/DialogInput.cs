namespace Barbuuuda.Models.Chat.Input
{
    /// <summary>
    /// Класс входной модели для диалога.
    /// </summary>
    public class DialogInput
    {
        /// <summary>
        /// Id диалога.
        /// </summary>
        public long? DialogId { get; set; }

        /// <summary>
        /// Флаг кнопки "Написать".
        /// </summary>
        public bool IsWriteBtn { get; set; }

        /// <summary>
        /// Id исполнителя, на ставку которого нажали.
        /// </summary>
        public string ExecutorId { get; set; }
    }
}
