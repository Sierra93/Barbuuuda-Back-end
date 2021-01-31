using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Emails.EmailTemplates
{
    /// <summary>
    /// Класс описывает шаблон подтверждения почты юзера.
    /// </summary>
    public class EmailConfirmTemplate : BaseEmailTemplate
    {
        public string RedirectUrl { get; set; } // Url для перехода после подтверждения почты юзером.
    }
}
