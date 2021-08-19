using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Pagination.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.PaginationTests
{
    [TestClass]
    public class GetMyCustomerPaginationTest : BaseServiceTest
    {
        [TestMethod]
        public async Task GetMyCustomerPaginationTestAsync()
        {
            var mock = new Mock<IPaginationService>();
            mock.Setup(a => a.GetMyCustomerPaginationAsync(2, 10, "petya")).Returns(Task.FromResult(new IndexOutput()));
            var component = new PaginationService(PostgreContext, UserService);
            var result = await component.GetMyCustomerPaginationAsync(2, 10, "petya");

            Assert.IsNotNull(result);
        }
    }
}
