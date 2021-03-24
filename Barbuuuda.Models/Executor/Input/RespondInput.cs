using Barbuuuda.Models.Entities.Respond;

namespace Barbuuuda.Models.Executor.Input
{
    /// <summary>
    /// Входная модель ставки к заданию.
    /// </summary>
    public class RespondInput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public int? TaskId { get; set; }

        /// <summary>
        /// Цена, за которую исполнитель готов выполнить задание.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Запомнить сообщение как шаблон.
        /// </summary>
        public bool IsTemplate { get; set; }

        /// <summary>
        /// Объект шаблона к ставке.
        /// </summary>
        public RespondTemplateEntity TaskTemplate { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }
    }
}
