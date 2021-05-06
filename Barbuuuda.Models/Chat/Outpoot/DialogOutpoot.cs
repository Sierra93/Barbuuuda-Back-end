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
        public long DialogId { get; set; }

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
        /// Логин собеседника.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя собеседника.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия собеседника.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Фото профиля.
        /// </summary>
        public string UserIcon { get; set; }

        /// <summary>
        /// Вычисляемое время для диалогов.
        /// </summary>
        public string CalcTime { get; set; }

        /// <summary>
        /// Вычисляемая дата для диалогов.
        /// </summary>
        public string CalcShortDate { get; set; }

        /// <summary>
        /// Полная дата.
        /// </summary>
        public string Created { get; set; }
    }
}
