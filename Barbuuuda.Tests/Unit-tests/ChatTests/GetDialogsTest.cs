using Barbuuuda.Controllers;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Chat.Outpoot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barbuuuda.Tests.Unit_tests.ChatTests
{
    /// <summary>
    /// Класс тестирует получение диалогов. 
    /// </summary>
    /// <returns>Список диалогов.</returns>
    [TestClass]
    public class GetDialogsTest
    {
        private const string ACCOUNT = "lera";

        /// <summary>
        /// Метод тестирует получение диалогов. 
        /// </summary>
        /// <returns>Список диалогов.</returns>
        [TestMethod]
        public async Task GetDialogTest()
        {
            Mock<IChat> mock = new Mock<IChat>();
            mock.Setup(a => a.GetDialogsAsync(ACCOUNT)).Returns(Task.FromResult(GetResultDialogs()));
            ChatController controller = new ChatController(mock.Object);
            OkObjectResult viewResult = await controller.GetDialogsAsync() as OkObjectResult;
            GetResultDialogOutpoot result = viewResult.Value as GetResultDialogOutpoot;

            Assert.AreEqual(4, result.Dialogs.Count);
        }

        private GetResultDialogOutpoot GetResultDialogs()
        {
            GetResultDialogOutpoot dialogs = new GetResultDialogOutpoot()
            {
                Dialogs = new List<DialogOutpoot>()
                {
                    new DialogOutpoot()
                    {
                        DialogId = 1,
                        DialogName = "Dialog1"
                    },

                   new DialogOutpoot()
                    {
                        DialogId = 2,
                        DialogName = "Dialog2"
                    },

                   new DialogOutpoot()
                    {
                        DialogId = 3,
                        DialogName = "Dialog3"
                    },

                   new DialogOutpoot()
                    {
                        DialogId = 4,
                        DialogName = "Dialog4"
                    },
                }
            };

            return dialogs;
        }
    }
}
