﻿using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Emails
{
    /// <summary>
    /// Класс реализует методы Email-рассылок.
    /// </summary>
    public sealed class EmailService
    {
        private readonly IdentityDbContext _iden;

        public EmailService(IdentityDbContext iden)
        {
            _iden = iden;
        }

        /// <summary>
        /// Метод отправляет оповещение о подтверждении почты юзеру.
        /// </summary>
        /// <param name="email">Email-адрес, на который отправит подтверждение.</param>
        /// <param name="subject">Тема сообщения.</param>
        /// <param name="message">Сообщение.</param>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Администрация сервиса Barbuuuda", "sierra_93@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("sierra_93@mail.ru", "13467kvm");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
