using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Tests {
    /// <summary>
    /// Тесты контроллера пользователя.
    /// </summary>
    [TestClass]
    public class UserControllerTests {
        /// <summary>
        /// Метод тестирует получение информации для профиля юзера.
        /// </summary>
        [TestMethod]
        public void GetProfileInfoTest() {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "GetProfileInfoTest").Options;
            var postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "GetProfileInfoTest").Options;
            var dbContext = new ApplicationDbContext(dbOptions);
            var postgreContext = new PostgreDbContext(postgreOptions);

            AddProfileInfo(postgreContext);

            var query = new GetDataQuery(dbContext, postgreContext);
            var result = query.GetProfileInfo();

            Assert.IsTrue(result != null);
        }

        /// <summary>
        /// Добавляет тестовую информацию профиля юзера.
        /// </summary>
        /// <param name="context"></param>
        void AddProfileInfo(PostgreDbContext context) {
            UserDto oUser = new UserDto {
                //UserLogin = "Olyaleya",
                //UserPassword = "12345!",
                //UserEmail = "olyaleya@mail.ru",
                //UserType = "Заказчик",
                //UserPhone = "8(985)-435-65-78",
                //Age = 23
            };

            context.Users.Add(oUser);
            context.SaveChanges();
        }      
    }
}
