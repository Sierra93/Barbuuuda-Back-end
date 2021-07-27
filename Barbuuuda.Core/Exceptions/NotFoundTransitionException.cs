namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникнет, если не было найдено переода.
    /// </summary>
    public class NotFoundTransitionException : UserMessageException
    {
        public NotFoundTransitionException() : base("Переход не был найден")
        {

        }
    }
}
