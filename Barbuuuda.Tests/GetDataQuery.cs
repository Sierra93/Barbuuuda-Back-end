using Barbuuuda.Core.Data;
using Barbuuuda.Models.Task;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barbuuuda.Tests {
    /// <summary>
    /// Класс получения тестовых данных.
    /// </summary>
    public class GetDataQuery {
        ApplicationDbContext _db;
        PostgreDbContext _postgre;

        public GetDataQuery(ApplicationDbContext db, PostgreDbContext postgre) {
            _db = db;
            _postgre = postgre;
        }

        /// <summary>
        /// Метод получает список тестовых заданий.
        /// </summary>
        /// <returns></returns>
        public IList GetTasksList() {
            return _postgre.Tasks.ToList();
        }
    }
}
