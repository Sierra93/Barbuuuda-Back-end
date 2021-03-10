namespace Barbuuuda.Core.Exceptions
{
    public class ErrorRangeAnswerException : UserMessageException
    {
        public ErrorRangeAnswerException(int numberAsnwer) : base($"Передан недопустимый номер вопроса {numberAsnwer}")
        {

        }
    }
}
