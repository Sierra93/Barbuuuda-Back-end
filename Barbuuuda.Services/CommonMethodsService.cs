using Barbuuuda.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Barbuuuda.Services {
    /// <summary>
    /// Сервис общих методов.
    /// </summary>
    public class CommonMethodsService<TEnum> {
        ApplicationDbContext _db;
        PostgreDbContext _postgre;

        public CommonMethodsService(ApplicationDbContext db, PostgreDbContext postgre) {
            _db = db;
            _postgre = postgre;
        }
    }
}
