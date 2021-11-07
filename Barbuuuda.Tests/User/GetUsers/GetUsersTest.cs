using System;
using System.Threading.Tasks;
using Barbuuuda.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barbuuuda.Tests.User.GetUsers
{
    [TestClass]
    public class GetUsersTest : BaseServiceTest
    {
        [TestMethod]
        public async Task GetUsersTestAsyncTest()
        {
            var res = await PostgreContext.Users.ToListAsync();

            Console.WriteLine();
        }
    }
}
