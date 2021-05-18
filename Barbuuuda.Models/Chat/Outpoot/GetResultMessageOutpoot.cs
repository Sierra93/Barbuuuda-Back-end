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
        public long Count => Messages.Count;

        /// <summary>
        /// Состояние диалога.
        /// </summary>
        public string DialogState { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
