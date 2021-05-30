namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Класс выходной модели счетов.
    /// </summary>
    public class InvoiceOutput
    {
        /// <summary>
        /// Id счета.
        /// </summary>
        public long ScoreId { get; set; }

        /// <summary>
        /// Сумма счета.
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Номер счета.
        /// </summary>
        public int? ScoreNumber { get; set; }

        /// <summary>
        /// Id пользователя, которому принадлежит счет.
        /// </summary>
        public string UserId { get; set; }
    }
}
