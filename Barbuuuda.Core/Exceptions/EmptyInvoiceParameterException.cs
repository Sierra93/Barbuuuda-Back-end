using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если заполнены не все необходимые поля для пополнения баланса счета.
    /// </summary>
    public class EmptyInvoiceParameterException : Exception
    {
        public EmptyInvoiceParameterException() : base ($"Не все обязательные поля заполнены")
        {

        }
    }
}
