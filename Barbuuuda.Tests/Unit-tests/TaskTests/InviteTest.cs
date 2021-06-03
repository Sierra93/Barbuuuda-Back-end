using System.Collections.Generic;
using System.Threading.Tasks;
using Barbuuuda.Controllers;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.Unit_tests.TaskTests
{
    /// <summary>
    /// Класс тестирует получение списка заданий, в которых был выбран исполнитель.
    /// </summary>
    [TestClass]
    public class InviteTest : BaseServiceTest
    {
        [TestMethod]
        public async Task InviteAsyncTest()
        {
            Mock<IExecutorService> mock = new Mock<IExecutorService>();
            mock.Setup(a => a.InviteAsync(EXECUTOR_LOGIN)).Returns(Task.FromResult(GetInvities()));
            ExecutorController controller = new ExecutorController(mock.Object);
            var result = await controller.InviteAsync();

            Assert.AreEqual(1, GetInvities().Count);
        }

        /// <summary>
        /// Метод тестовых приглашений.
        /// </summary>
        /// <returns></returns>
        private GetResultTask GetInvities()
        {
            GetResultTask invities = new GetResultTask()
            {
                Invities = new List<ResultTaskOutput>()
                {
                    new ResultTaskOutput()
                    {
                        TaskId = 1
                    }
                }
            };

            return invities;
        }
    }
}
