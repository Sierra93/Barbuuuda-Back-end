namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Класс выходной модели для шаблона виджета при оплате задания.
    /// </summary>
    public class PaymentWidgetOutput
    {
        /// <summary>
        /// Версия.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Вложенная секция invoice.
        /// </summary>
        public Invoice Invoice { get; set; }

        /// <summary>
        /// Вложенная секция paymentForm.
        /// </summary>
        public PaymentForm PaymentForm { get; set; }

        /// <summary>
        /// Вложенная секция payment.
        /// </summary>
        public Payment Payment { get; set; }
    }
}
