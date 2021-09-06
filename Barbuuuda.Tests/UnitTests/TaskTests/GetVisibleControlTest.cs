using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.TaskTests
{
    [TestClass]
    public class GetVisibleControlTest : BaseServiceTest
    {
        [TestMethod]
        public async Task GetVisibleControlTestAsync()
        {
            var mock = new Mock<ITaskService>();
            mock.Setup(a => a.VisibleControlAsync("По статусу"));
            var controller = new TaskService(ApplicationDbContext, PostgreContext, UserService);
            var result = await controller.VisibleControlAsync("По статусу");

            Assert.IsNotNull(result);
        }
    }
}
