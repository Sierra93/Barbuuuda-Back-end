//using Barbuuuda.Controllers;
//using Barbuuuda.Core.Interfaces;
//using Barbuuuda.Models.Chat.Output;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Barbuuuda.Tests.Unit_tests.ChatTests
//{
//    /// <summary>
//    /// Класс тестирует получение диалогов. 
//    /// </summary>
//    /// <returns>Список диалогов.</returns>
//    [TestClass]
//    public class GetDialogsTest : BaseServiceTest
//    {
//        /// <summary>
//        /// Метод тестирует получение диалогов. 
//        /// </summary>
//        /// <returns>Список диалогов.</returns>
//        [TestMethod]
//        public async Task GetDialogTest()
//        {
//            Mock<IChatService> mock = new Mock<IChatService>();
//            mock.Setup(a => a.GetDialogsAsync(ACCOUNT)).Returns(Task.FromResult(GetResultDialogs()));
//            ChatController controller = new ChatController(mock.Object);
//            OkObjectResult viewResult = await controller.GetDialogsAsync() as OkObjectResult;
//            GetResultDialogOutput result = viewResult.Value as GetResultDialogOutput;

//            Assert.AreEqual(4, result.Dialogs.Count);
//        }

//        private GetResultDialogOutput GetResultDialogs()
//        {
//            GetResultDialogOutput dialogs = new GetResultDialogOutput()
//            {
//                Dialogs = new List<DialogOutput>()
//                {
//                    new DialogOutput()
//                    {
//                        DialogId = 1,
//                        DialogName = "Dialog1"
//                    },

//                   new DialogOutput()
//                    {
//                        DialogId = 2,
//                        DialogName = "Dialog2"
//                    },

//                   new DialogOutput()
//                    {
//                        DialogId = 3,
//                        DialogName = "Dialog3"
//                    },

//                   new DialogOutput()
//                    {
//                        DialogId = 4,
//                        DialogName = "Dialog4"
//                    },
//                }
//            };

//            return dialogs;
//        }
//    }
//}
