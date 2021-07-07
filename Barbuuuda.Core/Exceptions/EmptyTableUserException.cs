using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если в таблице пользователей нет ни одной записи.
    /// </summary>
    public class EmptyTableUserException : Exception
    {
        public EmptyTableUserException() : base("В таблице пользователей не найдено записей")
        {

        }
    }
}
