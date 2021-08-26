namespace Barbuuuda.Models.Respond.Output
{
    /// <summary>
    /// Класс выходной модели для изменения ставки.
    /// </summary>
    public class ChangeRespondOutput
    {
        /// <summary>
        /// Цена, за которую исполнитель готов выполнить задание.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }
    }
}
