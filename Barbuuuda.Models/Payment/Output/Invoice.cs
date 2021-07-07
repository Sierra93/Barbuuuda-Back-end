namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Секция виджета invoice.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Описание услуги.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Сумма.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// ID магазина продавца.
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// TaskId задания.
        /// Если он не передан, значит идет пополнение кошелька.
        /// Если передан, значит идет оплата задания.
        /// </summary>
        public long? TaskId { get; set; }

        /// <summary>
        /// Логин пользователя сервиса.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Валюта. По дефолту RUB.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Флаг пополнения.
        /// </summary>
        public string Refill { get; set; }
    }
}
