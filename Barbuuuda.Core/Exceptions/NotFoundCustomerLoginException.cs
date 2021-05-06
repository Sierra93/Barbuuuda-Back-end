using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если логин заказчика не найден.
    /// </summary>
    public class NotFoundCustomerLoginException : Exception
    {
        public NotFoundCustomerLoginException() : base($"Заказчика задания не найдено")
        {

        }
    }
}
