using System.ComponentModel.DataAnnotations;

namespace Barbuuuda.Models.Chat.Input
{
    /// <summary>
    /// Класс входной модели для диалога.
    /// </summary>
    public class DialogInput
    {
        /// <summary>
        /// Id пользователя, которому пишут.
        /// </summary>
        //public string UserId { get; set; }

        /// <summary>
        /// Id диалога.
        /// </summary>
        public long? DialogId { get; set; }
    }
}
