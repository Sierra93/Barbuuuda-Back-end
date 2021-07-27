namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не был передан TaskId.
    /// </summary>
    public class NullTaskIdException : UserMessageException
    {
        public NullTaskIdException() : base("Не передан TaskId")
        {

        }
    }
}
