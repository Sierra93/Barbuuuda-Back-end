namespace Barbuuuda.Models.Chat.Outpoot
{
    /// <summary>
    /// Класс выходной модели участников диалога.
    /// </summary>
    public class DialogMemberOutpoot
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Id участника диалога.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id диалога.
        /// </summary>
        public int DialogId { get; set; }
    }
}
