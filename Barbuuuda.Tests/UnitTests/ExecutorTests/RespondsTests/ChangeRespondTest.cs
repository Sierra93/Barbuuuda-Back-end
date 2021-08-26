using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.ExecutorTests.RespondsTests
{
    /// <summary>
    /// Класс тестирует изменение ставки исполнителя.
    /// </summary>
    [TestClass]
    public class ChangeRespondTest : BaseServiceTest
    {
        [TestMethod]
        public async Task ChangeRespondTestAsyncTest()
        {
            var mock = new Mock<IExecutorService>();
            mock.Setup(a => a.ChangeRespondAsync(1000003, 100, "Спроектирую и создам базу данных", 8, "executor1")).Returns(Task.FromResult(true));
            var component = new ExecutorService(ApplicationDbContext, PostgreContext, UserService);
            var result = await component.ChangeRespondAsync(1000003, 100, "Спроектирую и создам базу данных", 8, "executor1");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task NullChangeRespondTestAsyncTest()
        {
            var mock = new Mock<IExecutorService>();
            mock.Setup(a => a.ChangeRespondAsync(0, 100, "Спроектирую и создам базу данных", 8, "executor1")).Returns(Task.FromResult(true));
            var component = new ExecutorService(ApplicationDbContext, PostgreContext, UserService);
            var result = await component.ChangeRespondAsync(0, 100, "Спроектирую и создам базу данных", 8, "executor1");

            Assert.IsFalse(result);
        }
    }
}
