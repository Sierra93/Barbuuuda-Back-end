using Barbuuuda.Controllers;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Chat.Outpoot;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barbuuuda.Tests.Unit_tests.ChatTests
{
    [TestClass]
    public class GetDialogsTest
    {
        [TestMethod]
        public async Task GetDialogTest()
        {
            var mock = new Mock<IChat>();
            mock.Setup(a => a.GetDialogsAsync("lera")).Returns(Task.FromResult(new GetResultDialogOutpoot()));
            ChatController controller = new ChatController(mock.Object);
            var result = controller.GetDialogsAsync();
            System.Console.WriteLine();
        }
    }
}
