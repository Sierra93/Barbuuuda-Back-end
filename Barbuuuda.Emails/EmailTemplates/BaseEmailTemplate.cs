using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Emails.EmailTemplates
{
    /// <summary>
    /// Класс представляет базовый шаблон почты.
    /// </summary>
    public class BaseEmailTemplate
    {
        public string EmailTitle { get; set; }

        public string EmailBody { get; set; }
    }
}
