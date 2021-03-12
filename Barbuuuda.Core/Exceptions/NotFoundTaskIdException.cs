using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если задание не удалось найти.
    /// </summary>
    public class NotFoundTaskIdException : Exception
    {
        public NotFoundTaskIdException(int taskId) : base($"Задания с TaskId {taskId} не найдено")
        {

        }
    }
}
