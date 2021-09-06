using System;

namespace Barbuuuda.Core.Exceptions
{
    public class ErrorGetTaskTypesException : Exception
    {
        public ErrorGetTaskTypesException() : base("Ошибка получения списка типов заданий для селекта фильтрации")
        {

        }
    }
}
