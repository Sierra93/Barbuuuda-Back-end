//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Barbuuuda.Controllers;
//using Barbuuuda.Core.Interfaces;
//using Barbuuuda.Models.Payment.Output;
//using Barbuuuda.Models.User.Input;
//using Barbuuuda.Models.User.Output;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace Barbuuuda.Tests.Unit_tests.Payment
//{
//    [TestClass]
//    public class GetBalanceTest : BaseServiceTest
//    {
//        [TestMethod]
//        public async Task GetBalanceAsyncTest()
//        {
//            Mock<IPaymentService> mock = new Mock<IPaymentService>();
//            mock.Setup(a => a.GetBalanceAsync(ACCOUNT)).Returns(Task.FromResult(GetBalance()));
//            PaymentController controller = new PaymentController(mock.Object);
//            OkObjectResult viewResult = await controller.GetBalanceAsync(new UserInput { UserName = "petya"}) as OkObjectResult;

//            Assert.AreEqual(viewResult?.StatusCode, 200);
//        }

//        private UserOutput GetBalance()
//        {
//            List<InvoiceOutput> invoices = new List<InvoiceOutput>()
//            {
//                new InvoiceOutput()
//                {
//                    ScoreId = 1,
//                    InvoiceAmount = 100,
//                    UserId = "76656c52-3c8b-43ae-a149-431fe2487e9b"
//                },

//                new InvoiceOutput()
//                {
//                    ScoreId = 2,
//                    InvoiceAmount = 0,
//                    UserId = "293a5a5f-cd4e-4874-8f79-3018fdd530fc"
//                },

//                new InvoiceOutput()
//                {
//                    ScoreId = 3,
//                    InvoiceAmount = 50,
//                    UserId = "b723e618-6e6a-41da-a1ac-50610fd4ae96"
//                }
//            };

//            var balanceAmount = invoices
//                .Where(i => i.UserId
//                .Equals(USER_ID))
//                .Select(res => res.InvoiceAmount)
//                .FirstOrDefault();

//            var result = new UserOutput
//            {
//                Amount = balanceAmount
//            };

//            return result;
//        }
//    }
//}
