using System.Collections.Generic;

namespace Barbuuuda.Models.Chat.Outpoot
{
    /// <summary>
    /// Класс списка диалогов для выходной модели диалога.
    /// </summary>
    public class GetResultDialogOutpoot
    {
        /// <summary>
        /// Список диалогов.
        /// </summary>
        public List<DialogOutpoot> Dialogs { get; set; } = new List<DialogOutpoot>();
    }
}
