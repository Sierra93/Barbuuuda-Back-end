using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникнет, если не удалось получить список статусов дял селекта.
    /// </summary>
    public class ErrorGetTaskStatusesException : Exception
    {
        public ErrorGetTaskStatusesException() : base("Ошибка получения списка статусов для селекта фильтрации")
        {

        }
    }
}
