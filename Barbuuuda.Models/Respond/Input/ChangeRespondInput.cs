namespace Barbuuuda.Models.Respond.Input
{
    /// <summary>
    /// Класс входной модели
    /// </summary>
    public class ChangeRespondInput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Id ставки.
        /// </summary>
        public long RespondId { get; set; }

        /// <summary>
        /// Цена, за которую исполнитель готов выполнить задание.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }
    }
}
