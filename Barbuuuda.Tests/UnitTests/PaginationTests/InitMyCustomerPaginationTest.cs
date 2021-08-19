using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Pagination.Output;
using Barbuuuda.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.UnitTests.PaginationTests
{
    [TestClass]
    public class InitMyCustomerPaginationTest : BaseServiceTest
    {
        [TestMethod]
        public async Task InitMyCustomerPaginationTestAsync()
        {
            var mock = new Mock<IPaginationService>();
            mock.Setup(a => a.InitMyCustomerPaginationAsync(1, "petya")).Returns(Task.FromResult(new IndexOutput()));
            var component = new PaginationService(PostgreContext, UserService);
            var result = await component.InitMyCustomerPaginationAsync(3, "petya");

            Assert.IsNotNull(result);
        }
    }
}
