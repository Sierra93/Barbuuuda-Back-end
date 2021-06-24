using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает при ошибке при пополнении счета сервиса.
    /// </summary>
    public class ErrorRefillScoreException : Exception
    {
        public ErrorRefillScoreException() : base("Произошла ошибка при пополнении счета на сервисе")
        {

        }
    }
}
