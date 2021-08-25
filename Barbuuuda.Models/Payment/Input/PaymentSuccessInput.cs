using System;

namespace Barbuuuda.Models.Payment.Input
{
    /// <summary>
    /// Класс входной модели, которая заполнится при успешном ответе платежной системы после оплаты.
    /// </summary>
    public class PaymentSuccessInput
    {
        public string LMI_MERCHANT_ID { get; set; }

        public string LMI_CURRENCY { get; set; }

        public string LMI_PAYMENT_AMOUNT { get; set; }

        public DateTime LMI_SYS_PAYMENT_DATE { get; set; }

        public long LMI_SYS_PAYMENT_ID { get; set; }

        public long TaskId { get; set; }

        public string Account { get; set; }

        public bool Refill { get; set; }
    }
}
