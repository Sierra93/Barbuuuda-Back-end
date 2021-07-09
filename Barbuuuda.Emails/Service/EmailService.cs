using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Logger;

namespace Barbuuuda.Emails.Service
{
    /// <summary>
    /// Класс реализует методы Email-рассылок.
    /// </summary>
    public class EmailService
    {
        private readonly ApplicationDbContext _db;

        public EmailService(ApplicationDbContext db)
        {
            _db = db;
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
                emailMessage.From.Add(new MailboxAddress("Администрация сервиса Barbuuuda", "info.barbuuuda@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };
                
                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("info.barbuuuda@gmail.com", "13467982DdddDdddDddd");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger logger = new Logger(_db, e.GetType().FullName, e.Message, e.StackTrace);
                _ = logger.LogCritical();
                throw;
            }
        }
    }
}
