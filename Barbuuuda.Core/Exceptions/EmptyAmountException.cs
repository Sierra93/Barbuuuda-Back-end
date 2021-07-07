using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не была передана сумма для пополнения или оплаты.
    /// </summary>
    public class EmptyAmountException : Exception
    {
        public EmptyAmountException() : base("Не передана сумма для пополнения или оплаты")
        {

        }
    }
}
