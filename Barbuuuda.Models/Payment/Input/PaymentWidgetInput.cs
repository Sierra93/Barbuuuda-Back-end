namespace Barbuuuda.Models.Payment.Input
{
    /// <summary>
    /// Класс входной модели виджета оплаты.
    /// </summary>
    public class PaymentWidgetInput
    {
        /// <summary>
        /// Сумма.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public string Currency { get; set; }
    }
}
