using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
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
        /// <param name="username">Login юзера.</param>
        public async Task ConfirmEmailAsync(string username)
        {

        }

        /// <summary>
        /// Метод меняет флаг в БД EmailConfirmed на true.
        /// </summary>
        /// <param name="username">Login юзера.</param>
        public async Task AcceptConfirmEmailAsync(string username)
        {
            //UserDto oUser = await _iden.AspNetUsers
            //    .Where(u => u.UserName
            //    .Equals(username))
            //    .FirstOrDefaultAsync();

            //oUser.EmailConfirmed = true;

            //await _iden.SaveChangesAsync();
        }
    }
}
