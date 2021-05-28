using System;

namespace Barbuuuda.Commerces.Exceptions
{
    /// <summary>
    /// Исключение возникает, когда не удается создать заказ.
    /// </summary>
    public class NotCaptureOrderByOrderId : Exception
    {
        public NotCaptureOrderByOrderId(string orderId) : base($"Не удалось создать заказ с OrderId: {orderId}")
        {

        }
    }
}
