using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Pagination.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.PaginationTests
{
    /// <summary>
    /// Тест на пагинацию в аукционе.
    /// </summary>
    [TestClass]
    public class GetAuctionPaginationTest : BaseServiceTest
    {
        [TestMethod]
        public async Task GetAuctionPaginationTestAsync()
        {
            var mock = new Mock<IPaginationService>();
            mock.Setup(a => a.GetPaginationAuction(1, 10)).Returns(Task.FromResult(new IndexOutput()));
            var component = new PaginationService(PostgreContext, UserService);
            var result = await component.GetPaginationAuction(1, 10);

            Assert.IsNotNull(result.Tasks);
        }
    }
}
