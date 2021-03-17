using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если пользователь передал недопустимый аргумент метода
    /// </summary>
    public class NotParameterException : Exception
    {
        public NotParameterException(string param)
            : base($"Передан недопустимый аргумент метода {param}. Нужно передать параметр All или Single.")
        {

        }
    }
}
