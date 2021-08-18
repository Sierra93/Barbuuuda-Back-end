using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Pagination.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.PaginationTests
{
    /// <summary>
    /// Тест на пагинацию в работе исполнителя на ините страницы мои задания.
    /// </summary>
    [TestClass]
    public class InitWorkPaginationTest : BaseServiceTest
    {
        [TestMethod]
        public async Task GetAuctionPaginationTestAsync()
        {
            var mock = new Mock<IPaginationService>();
            mock.Setup(a => a.GetInitPaginationWorkAsync(1, ExecutorLogin)).Returns(Task.FromResult(new IndexOutput()));
            var component = new PaginationService(PostgreContext, UserService);
            var result = await component.GetInitPaginationWorkAsync(1, ExecutorLogin);

            Assert.IsNotNull(result.Tasks);
        }
    }
}
