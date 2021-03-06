﻿using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Barbuuuda.Tests.Unit_tests
{
    /// <summary>
    /// Класс тестирует методы контроллера ExecutorController.
    /// </summary>
    [TestClass]
    public class ExecutorControllerTests
    {
        /// <summary>
        /// Метод тестирует добавление специализаций исполнителю.
        /// </summary>
        [TestMethod]
        public void AddSpecializationsTest()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "GetTasksTest").Options;
            var postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "GetTasksTest").Options;
            var postgreContext = new PostgreDbContext(postgreOptions);

            AddSpecializations(postgreContext);
        }

        /// <summary>
        /// Метод добавляет тестовые специализации исполнителя.
        /// </summary>
        /// <param name="pdbc">Контекст.</param>
        private void AddSpecializations(PostgreDbContext pdbc)
        {
            string executorName = AddExecutor(pdbc);
            int count = AddSpec(pdbc, executorName);

            Assert.AreEqual(2, count);
        }

        /// <summary>
        /// Метод добавляет тестового исполнителя.
        /// </summary>
        /// <param name="pdbc">Контекст.</param>
        private string AddExecutor(PostgreDbContext pdbc)
        {
            UserEntity oExecutor = new UserEntity
            {
                UserName = "testexecutor",
                UserPassword = "12345!",
                Email = "testexecutor@mail.ru",
                UserRole = "E",
                PhoneNumber = "8(985)-435-65-78",
                Age = 23
            };

            pdbc.Users.Add(oExecutor);
            pdbc.SaveChanges();

            return oExecutor.UserName;
        }

        /// <summary>
        /// Метод добавляет тестовые специализации.
        /// </summary>
        /// <param name="pdbc">Контекст.</param>
        /// <param name="name">Логин исполнителя.</param>
        private int AddSpec(PostgreDbContext pdbc, string name)
        {
            ExecutorSpecialization[] aSpecies = new ExecutorSpecialization[] {
                new ExecutorSpecialization() { SpecName = "Специализация1" },
                new ExecutorSpecialization() { SpecName = "Специализация2" }
            };        
            UserEntity oExecutor = pdbc.Users
                .Where(e => e.UserName
                .Equals(name))
                .FirstOrDefault();

            if (oExecutor.Specializations == null)
            {
                oExecutor.Specializations = aSpecies;
            }

            pdbc.SaveChanges();

            UserEntity getExecutor = pdbc.Users
                .Where(e => e.UserName
                .Equals(name))
                .FirstOrDefault();

            return getExecutor.Specializations.Count();
        }
    }
}
