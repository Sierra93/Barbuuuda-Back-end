using Barbuuuda.Core.Data;
using Barbuuuda.Models.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Tests.Unit_tests
{
    [TestClass]
    public class MainPageControllerTests
    {
        [TestMethod]
        public void GetLastTasksTest()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "GetLastTasksTest").Options;
            var postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "GetLastTasksTest").Options;
            var dbContext = new ApplicationDbContext(dbOptions);
            var postgreContext = new PostgreDbContext(postgreOptions);

            AddTask(postgreContext);

            var query = new GetDataQuery(dbContext, postgreContext);
            var result = query.GetLastTasks();

            Assert.AreEqual(5, result.Count);
        }

        /// <summary>
        /// Метод добавляет 7 тестовых заданий, но должен вернуть последние 5.
        /// </summary>
        /// <param name="context"></param>
        void AddTask(PostgreDbContext context)
        {
            TaskEntity oTask1 = new TaskEntity()
            {
                OwnerId = "1",
                TaskTitle = "test11",
                TaskDetail = "test111",
                TypeCode = "EJ6B3ABgoUuP6wT/rgOgqw==",
                CategoryCode = "U4MuMGKPiE251VPwtbtMgg==",
                SpecCode = "OVUW7v4Tu0WABpcJ3aN0rg=="
            };

            TaskEntity oTask2 = new TaskEntity()
            {
                OwnerId = "2",
                TaskTitle = "test11111",
                TaskDetail = "test11111",
                TypeCode = "EJ6B3ABxoUuP6wT/rgOgqw==",
                CategoryCode = "U4MuMGKPiEh51VPwtbtMgg==",
                SpecCode = "OVUW1m4Tu0WABpcJ3aN0rg=="
            };

            TaskEntity oTask3 = new TaskEntity()
            {
                OwnerId = "3",
                TaskTitle = "test11111",
                TaskDetail = "test11111",
                TypeCode = "EJ6B3ABxoUuP6wT/rgOgqw==",
                CategoryCode = "U4MuMGKPiEh51VPwtbtMgg==",
                SpecCode = "OVUW1m4Tu0WABpcJ3aN0rg=="
            };

            TaskEntity oTask4 = new TaskEntity()
            {
                OwnerId = "4",
                TaskTitle = "test11111",
                TaskDetail = "test11111",
                TypeCode = "EJ6B3ABxoUuP6wT/rgOgqw==",
                CategoryCode = "U4MuMGKPiEh51VPwtbtMgg==",
                SpecCode = "OVUW1m4Tu0WABpcJ3aN0rg=="
            };

            TaskEntity oTask5 = new TaskEntity()
            {
                OwnerId = "5",
                TaskTitle = "test11111",
                TaskDetail = "test11111",
                TypeCode = "EJ6B3ABxoUuP6wT/rgOgqw==",
                CategoryCode = "U4MuMGKPiEh51VPwtbtMgg==",
                SpecCode = "OVUW1m4Tu0WABpcJ3aN0rg=="
            };

            TaskEntity oTask6 = new TaskEntity()
            {
                OwnerId = "6",
                TaskTitle = "test11111",
                TaskDetail = "test11111",
                TypeCode = "EJ6B3ABxoUuP6wT/rgOgqw==",
                CategoryCode = "U4MuMGKPiEh51VPwtbtMgg==",
                SpecCode = "OVUW1m4Tu0WABpcJ3aN0rg=="
            };

            TaskEntity oTask7 = new TaskEntity()
            {
                OwnerId = "7",
                TaskTitle = "test11111",
                TaskDetail = "test11111",
                TypeCode = "EJ6B3ABxoUuP6wT/rgOgqw==",
                CategoryCode = "U4MuMGKPiEh51VPwtbtMgg==",
                SpecCode = "OVUW1m4Tu0WABpcJ3aN0rg=="
            };

            context.Tasks.AddRange(oTask1, oTask2,  oTask3, oTask4, oTask5, oTask6, oTask7);
            context.SaveChanges();
        }
    }
}
