using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не найдено заказчика задания.
    /// </summary>
    public class NotFoundTaskCustomerIdException : Exception
    {
        public NotFoundTaskCustomerIdException(string customerId) : base($"Заказчика задания с TaskId {customerId} не найдено")
        {

        }
    }
}
