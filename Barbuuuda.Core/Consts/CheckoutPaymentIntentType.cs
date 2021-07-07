namespace Barbuuuda.Core.Consts
{
    /// <summary>
    /// Класс констант типов действий при создании заказов. 
    /// </summary>
    public class CheckoutPaymentIntentType
    {
        /// <summary>
        /// Авторизовать платеж и приостановить перевод средств после того, как покупатель произведет платеж.
        /// </summary>
        public const string TYPE_AUTHORIZE = "AUTHORIZE";

        /// <summary>
        /// Зафиксировать платеж сразу.
        /// </summary>
        public const string TYPE_CAPTURE = "CAPTURE";
    }
}
