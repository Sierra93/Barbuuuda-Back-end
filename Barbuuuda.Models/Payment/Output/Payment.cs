using System.Collections.Generic;

namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Класс секции виджета payment.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст кнопки оплаты.
        /// </summary>
        public string SubmitText { get; set; }

        /// <summary>
        /// Внешние разрешения.
        /// </summary>
        public bool AllowExternal { get; set; }  

        /// <summary>
        /// Массив с методами.
        /// </summary>
        public List<string> Methods { get; set; } = new List<string>();
    }
}
