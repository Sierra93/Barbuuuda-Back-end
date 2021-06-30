namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Секция виджета paymentForm.
    /// </summary>
    public class PaymentForm
    {
        /// <summary>
        /// Заголовок формы.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Вложенная секция value.
        /// </summary>
        public Value Value { get; set; }
    }
}
