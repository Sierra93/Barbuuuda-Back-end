using System.Collections.Generic;

namespace Barbuuuda.Models.Chat.Outpoot
{
    /// <summary>
    /// Класс выходной модели списка сообщений диалога.
    /// </summary>
    public class GetResultMessageOutpoot
    {
        /// <summary>
        /// Список сообщений.
        /// </summary>
        public List<MessageOutpoot> Messages { get; set; } = new List<MessageOutpoot>();

        /// <summary>
        /// Кол-во сообщений.
        /// </summary>
        public long Count { get { return Messages.Count; } set { } }

        /// <summary>
        /// Состояние диалога.
        /// </summary>
        public string DialogState { get; set; }
    }
}
