using System.ComponentModel.DataAnnotations;

namespace Barbuuuda.Models.Chat.Outpoot
{
    /// <summary>
    /// Класс выходной модели для диалога.
    /// </summary>
    public class DialogOutpoot
    {
        /// <summary>
        /// Id диалога.
        /// </summary>
        public int DialogId { get; set; }

        /// <summary>
        /// Название диалога (фамилия и имя с кем ведется переписка, либо логин пользователя).
        /// </summary>
        public string DialogName { get; set; }

        /// <summary>
        /// Последнее сообщение в кратком виде для отображения в сокращенном виде диалога.
        /// </summary>
        [MaxLength(40)]
        public string LastMessage { get; set; }

        /// <summary>
        /// Цена к ставке.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
