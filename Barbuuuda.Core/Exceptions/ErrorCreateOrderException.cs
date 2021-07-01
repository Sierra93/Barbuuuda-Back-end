using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если заказ не был создан.
    /// </summary>
    public class ErrorCreateOrderException : Exception
    {
        public ErrorCreateOrderException() : base("Ошибка создания заказа")
        {

        }
    }
}
