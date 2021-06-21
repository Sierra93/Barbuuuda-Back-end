namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Класс выходной модели конфигурации платежного виджета фронта.
    /// </summary>
    public class PaymentWidgetOutput
    {
        /// <summary>
        /// Название элемента.
        /// </summary>
        public string Element { get; set; }

        /// <summary>
        /// Идентификатор виджета.
        /// </summary>
        public int Widget { get; set; }

        /// <summary>
        /// Номер заказа.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Сумма.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
