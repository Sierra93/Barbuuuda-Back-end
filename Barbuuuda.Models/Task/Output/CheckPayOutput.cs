namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели проверки оплаты задания заказчиком.
    /// </summary>
    public class CheckPayOutput
    {
        /// <summary>
        /// Id задания, оплату которого нужно проверить.
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Флаг оплаты.
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// Id исполнителя, которого выбрали.
        /// </summary>
        public string ExecutorId { get; set; }
    }
}
