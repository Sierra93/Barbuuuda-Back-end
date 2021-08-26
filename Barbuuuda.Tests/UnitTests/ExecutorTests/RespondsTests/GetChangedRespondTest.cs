using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Respond.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.ExecutorTests.RespondsTests
{

    /// <summary>
    /// Класс тестирует получение ставки исполнителя для ее изменения.
    /// </summary>
    [TestClass]
    public class GetChangedRespondTest : BaseServiceTest
    {
        [TestMethod]
        public async Task GetChangeRespondAsyncTest()
        {
            var mock = new Mock<IExecutorService>();
            mock.Setup(a => a.GetChangedRespondAsync(1000003, 8, "executor1")).Returns(Task.FromResult(new ChangeRespondOutput()));
            var component = new ExecutorService(ApplicationDbContext, PostgreContext, UserService);
            var result = await component.GetChangedRespondAsync(1000003, 8, "executor1");

            Assert.IsNotNull(result);
        }
    }
}
