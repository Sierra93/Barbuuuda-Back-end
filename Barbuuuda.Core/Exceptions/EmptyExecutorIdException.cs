using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не передан Id исполнителя при выборе исполнителем задания.
    /// </summary>
    public class EmptyExecutorIdException : Exception
    {
        public EmptyExecutorIdException() : base ("Не передан ExecutorId исполнителя, которого нужно назначить исполнителем задания")
        {

        }
    }
}
