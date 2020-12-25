﻿using Barbuuuda.Core.Data;
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
    public class CommonMethodsService<T> {
        ApplicationDbContext _db;

        public CommonMethodsService(ApplicationDbContext db) {
            _db = db;
        }       
    }
}
