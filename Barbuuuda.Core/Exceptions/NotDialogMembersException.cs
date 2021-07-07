namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если не было найдено участников диалога.
    /// </summary>
    public class NotDialogMembersException : UserMessageException
    {
        public NotDialogMembersException(long? dialogId) : base($"Не найдено участников для диалога с DialogId {dialogId}")
        {

        }
    }
}
