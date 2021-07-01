using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если счет пользователя не был найден.
    /// </summary>
    public class NotFoundInvoiceException : Exception
    {
        public NotFoundInvoiceException() : base("Счет пользователя не найден")
        {

        }
    }
}
