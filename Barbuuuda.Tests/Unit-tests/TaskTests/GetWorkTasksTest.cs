//using System.Threading.Tasks;
//using Barbuuuda.Core.Interfaces;
//using Barbuuuda.Models.Task.Output;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace Barbuuuda.Tests.Unit_tests.TaskTests
//{
//    [TestClass]
//    public class GetWorkTasksTest : BaseServiceTest
//    {
//        [TestMethod]
//        public async Task GetWorkTasksAsyncTest()
//        {
//            Mock<IExecutorService> mock = new Mock<IExecutorService>();
//            mock.Setup(a => a.GetWorkTasksAsync(EXECUTOR_LOGIN)).Returns(Task.FromResult(new GetResultTask()));
//            GetResultTask result = await ExecutorService.GetWorkTasksAsync(EXECUTOR_LOGIN);

//            Assert.IsTrue(result.Count > 0);
//        }
//    }
//}
