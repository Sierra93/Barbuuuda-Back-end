using System.Threading.Tasks;
using Barbuuuda.Controllers;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Executor.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.Unit_tests.ExecutorTests
{
    [TestClass]
    public class AcceptWorkTaskTest 
    {
        [TestMethod]
        public async Task AcceptWorkTaskTestAsync()
        {
            Mock<IExecutorService> mock = new Mock<IExecutorService>();
            mock.Setup(a => a.AcceptTaskAsync(1000001, "executor1")).Returns(Task.FromResult(true));
            ExecutorController controller = new ExecutorController(mock.Object);
            var result = await controller.AcceptTaskAsync(new AcceptOrCancelWorkTaskInput() {TaskId = 1000001 }) as OkObjectResult; 

            Assert.AreEqual(result?.Value, true);
        }
    }
}
