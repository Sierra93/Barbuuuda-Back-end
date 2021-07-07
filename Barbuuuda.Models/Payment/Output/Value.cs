namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Секция виджета value.
    /// </summary>
    public class Value
    {
        /// <summary>
        /// Вложенная секция description.
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Вложенная секция amount.
        /// </summary>
        public Amount Amount { get; set; }

        /// <summary>
        /// Вложенная секция merchantId.
        /// </summary>
        public MerchantId MerchantId { get; set; }

        /// <summary>
        /// Вложенная секция taskId.
        /// </summary>
        public TaskId TaskId { get; set; }

        /// <summary>
        /// Вложенная секция account.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Вложенная секция currency.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Вложенная секция refill.
        /// </summary>
        public Refill Refill { get; set; }
    }
}
