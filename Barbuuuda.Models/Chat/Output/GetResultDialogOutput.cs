using System.Collections.Generic;

namespace Barbuuuda.Models.Chat.Output
{
    /// <summary>
    /// Класс списка диалогов для выходной модели диалога.
    /// </summary>
    public class GetResultDialogOutput
    {
        /// <summary>
        /// Список диалогов.
        /// </summary>
        public List<DialogOutput> Dialogs { get; set; } = new List<DialogOutput>();

        /// <summary>
        /// Кол-во диалогов.
        /// </summary>
        public long Count => Dialogs.Count;
    }
}
