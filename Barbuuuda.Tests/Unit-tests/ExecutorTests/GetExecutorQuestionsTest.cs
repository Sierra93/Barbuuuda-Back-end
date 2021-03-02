using Barbuuuda.Core.Data;
using Barbuuuda.Models.Entities.Executor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Barbuuuda.Tests.Unit_tests.ExecutorTests
{
    [TestClass]
    public class GetExecutorQuestionsTest
    {
        public void GetQuestionsTest()
        {
            DbContextOptions<PostgreDbContext> postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "AddQuestionsTest").Options;
            PostgreDbContext postgreContext = new PostgreDbContext(postgreOptions);

            QuestionEntity[] aQuestions = new[]
            {
                new QuestionEntity()
                {
                    QuestionId = 1,
                    QuestionText = "Что такое «Ставка» в терминах сервиса?",
                    NumberQuestion = 1
                },

                new QuestionEntity()
                {
                    QuestionId = 2,
                    QuestionText = "Что такое «Гарантийный период» в терминах сервиса?",
                    NumberQuestion = 2
                }
            };

            postgreContext.Questions.AddRange(aQuestions);
            postgreContext.SaveChanges();

            int questionsCount = postgreContext.Questions.Count();

            Assert.AreEqual(2, questionsCount);
        }
    }
}
