namespace Barbuuuda.Models.Task.Input
{
    /// <summary>
    /// Класс входной модели проверки оплаты задания заказчиком.
    /// </summary>
    public class CheckPayInput
    {
        /// <summary>
        /// Id задания, оплату которого нужно проверить.
        /// </summary>
        public long TaskId { get; set; }
    }
}
