﻿using System.Linq;
using System.Threading.Tasks;
using Barbuuuda.Core.Interfaces;
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
            mock.Setup(a => a.GetTypesSelectAsync());
            var controller = new TaskService(ApplicationDbContext, PostgreContext, UserService);
            var result = await controller.GetTypesSelectAsync();

            Assert.IsTrue(result.Any());
        }
    }
}
