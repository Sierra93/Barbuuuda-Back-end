using Barbuuuda.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Barbuuuda.Tests
{
    /// <summary>
    /// Тесты контроллера заданий.
    /// </summary>
    [TestClass]
    public class TaskControllerTests
    {
        /// <summary>
        /// Метод проверяет получение тестовых заданий.
        /// </summary>
        [TestMethod]
        public void GetTasksTest()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "GetTasksTest").Options;
            var postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "GetTasksTest").Options;
            var dbContext = new ApplicationDbContext(dbOptions);
            var postgreContext = new PostgreDbContext(postgreOptions);

            AddTask(postgreContext);

            var query = new GetDataQuery(dbContext, postgreContext);
            var result = query.GetTasksList();

            Assert.AreEqual(2, result.Count);
        }


        /// <summary>
        /// Метод добавляет тестовые задания.
        /// </summary>
        void AddTask(PostgreDbContext context)
        {
            //TaskDto oTask1 = new TaskDto() {
            //    OwnerId = 1,
            //    TaskTitle = "test11",
            //    TaskDetail = "test111",
            //    TypeCode = "EJ6B3ABgoUuP6wT/rgOgqw==",
            //    CategoryCode = "U4MuMGKPiE251VPwtbtMgg==",
            //    SpecCode = "OVUW7v4Tu0WABpcJ3aN0rg=="
            //};

            //TaskDto oTask2 = new TaskDto() {
            //    OwnerId = 2,
            //    TaskTitle = "test11111",
            //    TaskDetail = "test11111",
            //    TypeCode = "EJ6B3ABxoUuP6wT/rgOgqw==",
            //    CategoryCode = "U4MuMGKPiEh51VPwtbtMgg==",
            //    SpecCode = "OVUW1m4Tu0WABpcJ3aN0rg=="
            //};

            //context.Tasks.AddRange(oTask1, oTask2);
            context.SaveChanges();
        }
    }
}
