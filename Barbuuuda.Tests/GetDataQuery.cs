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

        /// <summary>
        /// Метод получает всю информацию профиля.
        /// </summary>
        /// <returns></returns>
        public object GetProfileInfo() {
            int userId = 1;

            return _postgre.Users
                    .Where(u => u.UserId == userId)
                    .Select(up => new {
                        up.UserLogin,
                        up.UserEmail,
                        up.UserPhone,
                        up.LastName,
                        up.FirstName,
                        up.Patronymic,
                        up.UserIcon,
                        up.Rating,
                        dateRegister = string.Format("{0:f}", up.DateRegister),
                        scoreMoney = string.Format("{0:0,0}", up.Score),
                        up.AboutInfo,
                        up.Plan,
                        up.City,
                        up.Age
                    })
                    .FirstOrDefault();
        }
    }
}
