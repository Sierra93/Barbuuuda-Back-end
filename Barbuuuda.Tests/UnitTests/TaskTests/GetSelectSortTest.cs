using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.Unit_tests.TaskTests
{
    [TestClass]
    public class GetSelectSortTest : BaseServiceTest
    {
        [TestMethod]
        public async Task GetBalanceAsyncTest()
        {
            var mock = new Mock<ITaskService>();
            mock.Setup(a => a.GetSortSelectAsync()).Returns(Task.FromResult(new ControlSortResult()));
            var component = new TaskService(ApplicationDbContext, PostgreContext, UserService);
            var result = await component.GetSortSelectAsync();

            Assert.IsTrue(result.Count > 0);
        }
    }
}
