using Barbuuuda.Core.Data;
using System;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис общих методов.
    /// </summary>
    public class CommonMethodsService
    {
        ApplicationDbContext _db;
        PostgreDbContext _postgre;

        public CommonMethodsService(ApplicationDbContext db, PostgreDbContext postgre)
        {
            _db = db;
            _postgre = postgre;
        }
    }
}
