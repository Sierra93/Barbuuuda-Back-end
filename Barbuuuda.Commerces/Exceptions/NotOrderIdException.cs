using System;

namespace Barbuuuda.Commerces.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не передан OrderId.
    /// </summary>
    public class NotOrderIdException : Exception
    {
        public NotOrderIdException() : base($"OrderId не может быть пустым")
        {

        }
    }
}
