using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Pagination.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.PaginationTests
{
    /// <summary>
    /// Тест на пагинацию на ините аукциона.
    /// </summary>
    [TestClass]
    public class InitAuctionPaginationTest : BaseServiceTest
    {
        [TestMethod]
        public async Task InitPaginationTestAsync()
        {
            var mock = new Mock<IPaginationService>();
            mock.Setup(a => a.GetInitPaginationAuctionTasks(1)).Returns(Task.FromResult(new IndexOutput()));
            var component = new PaginationService(PostgreContext, UserService);
            var result = await component.GetInitPaginationAuctionTasks(1);

            Assert.IsNotNull(result.Tasks);
        }
    }
}
