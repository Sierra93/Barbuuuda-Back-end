using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если пользователь не найден по логину.
    /// </summary>
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(string userName) : base($"Не найдено UserId для пользователя {userName}")
        {

        }
    }
}
