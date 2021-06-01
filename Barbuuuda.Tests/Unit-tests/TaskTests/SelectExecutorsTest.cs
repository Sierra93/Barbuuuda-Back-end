using System.Threading.Tasks;
using Barbuuuda.Controllers;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.Unit_tests.TaskTests
{
    /// <summary>
    /// Класс тестирует выбор заказчиком исполнителя на задание.
    /// </summary>
    [TestClass]
    public class SelectExecutorsTest : BaseServiceTest
    {
        [TestMethod]
        public async Task SelectExecutorTest()
        {
            Mock<ITaskService> mock = new Mock<ITaskService>();
            mock.Setup(a => a.SelectAsync(TASK_ID, EXECUTOR_ID)).Returns(Task.FromResult(true));
            TaskController controller = new TaskController(mock.Object);
            OkObjectResult viewResult = await controller.SelectAsync(new CheckPayInput()
            {
                TaskId = TASK_ID,
                ExecutorId = EXECUTOR_ID
            }) as OkObjectResult;
            bool result = viewResult?.Value != null && (bool)viewResult?.Value;
            
            Assert.IsTrue(result);
        }
    }
}
