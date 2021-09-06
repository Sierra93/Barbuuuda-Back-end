using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.TaskTests
{
    [TestClass]
    public class SelectStatusesTest : BaseServiceTest
    {
        [TestMethod]
        public async Task SelectStatusesTestAsync()
        {
            var mock = new Mock<ITaskService>();
            mock.Setup(a => a.GetStatusesSelectAsync()).Returns(TestReturnList());
            var controller = new TaskService(ApplicationDbContext, PostgreContext, UserService);
            var result = await controller.GetStatusesSelectAsync();

            Assert.IsTrue(result.Any());
        }

        private async Task<IEnumerable<TaskStatusOutput>> TestReturnList()
        {
            var res = new List<TaskStatusOutput>();

            return await Task.FromResult(res);
        }
    }
}
