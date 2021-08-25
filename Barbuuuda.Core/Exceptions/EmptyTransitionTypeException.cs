namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникнет, если не был передан тип перехода.
    /// </summary>
    public class EmptyTransitionTypeException : UserMessageException
    {
        public EmptyTransitionTypeException(): base("Не передан тип перехода") { }
    }
}
