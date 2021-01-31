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
        public string EmailSendTo { get; set; } // Кому отправит подтверждение.        
    }
}
