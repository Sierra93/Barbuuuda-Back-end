using System;

namespace Barbuuuda.Commerces.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не удалось вернуть Id заказа.
    /// </summary>
    public class NotCreateOrderException : Exception
    {
        public NotCreateOrderException(string id) : base($"Не удалось вернуть Id заказа {id}")
        {

        }
    }
}
