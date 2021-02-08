using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы по работе с исполнителями сервиса.
    /// </summary>
    public sealed class ExecutorService : IExecutor
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;

        public ExecutorService(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden)
        {
            _db = db;
            _postgre = postgre;
            _iden = iden;
        }

        /// <summary>
        /// Метод выгружает список исполнителей сервиса.
        /// </summary>
        /// <returns>Список исполнителей.</returns>
        public async Task<IEnumerable> GetExecutorListAsync()
        {
            try
            {
                return await (from users in _postgre.Users
                              where users.UserRole.Equals(UserRole.EXECUTOR)
                              select new
                              {
                                  users.UserName,
                                  dateRegister = string.Format("{0:f}", users.DateRegister),
                                  users.AboutInfo
                              })
                          .ToListAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
