using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не было найдено Id исполнителя по его логину.
    /// </summary>
    public class NotFoundExecutorIdException : Exception
    {
        public NotFoundExecutorIdException(string account) : base($"Не удалось найти Id исполнителя с логином {account}")
        {

        }
    }
}
