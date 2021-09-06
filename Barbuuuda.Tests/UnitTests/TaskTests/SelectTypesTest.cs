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
    public class SelectTypesTest : BaseServiceTest
    {
        [TestMethod]
        public async Task SelectTypesTestAsync()
        {
            var mock = new Mock<ITaskService>();
            mock.Setup(a => a.GetTypesSelectAsync()).Returns(TestReturnList());
            var controller = new TaskService(ApplicationDbContext, PostgreContext, UserService);
            var result = await controller.GetTypesSelectAsync();

            Assert.IsTrue(result.Any());
        }

        private async Task<IEnumerable<TaskTypeOutput>> TestReturnList()
        {
            var res = new List<TaskTypeOutput>();

            return await Task.FromResult(res);
        }
    }
}
