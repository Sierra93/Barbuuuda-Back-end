using Barbuuuda.Core.Data;
using Barbuuuda.Models.Entities.Executor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Barbuuuda.Tests.Unit_tests.ExecutorTests
{
    /// <summary>
    /// Класс тестирует проверку результатов ответов на вопросы теста исполнителем.
    /// </summary>
    [TestClass]
    public class CheckResultExecutorQuestionsTest
    {
        [TestMethod]
       public void CheckQuestionsTest()
        {
            DbContextOptions<PostgreDbContext> postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "CheckQuestionsTest").Options;
            PostgreDbContext postgreContext = new PostgreDbContext(postgreOptions);

            AddRightAnswersDB(postgreContext);

            // Тестовые результаты теста.
            AnswerVariants[] aResultsAnswers = new AnswerVariants[]
            {
                new AnswerVariants() {
                    AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Сумма, за которую Вы готовы выполнить задание",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Качества выполненных заданий и отзывов заказчиков, а также статистики по сделанным ставкам",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Запрещен",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "В режиме комментариев в задании",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Время после сдачи исполнителем задания заказчику, в течение которого заказчик может опротестовать задание или отправить его на доработку исполнителю",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "20 дней",
                    IsRight = true,
                    Selected = false
                }
            };
        }

        /// <summary>
        /// Метод добавит тестовые верные варианты ответов в тестовую БД.
        /// </summary>
        /// <param name="postgreContext">Контекст БД.</param>
        private void AddRightAnswersDB(PostgreDbContext postgreContext)
        {
            // Верные варианты ответов.
            AnswerVariants[] aRightAnswers = new AnswerVariants[]
            {
                new AnswerVariants() {
                    AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Сумма, за которую Вы готовы выполнить задание",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Качества выполненных заданий и отзывов заказчиков, а также статистики по сделанным ставкам",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Запрещен",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "В режиме комментариев в задании",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "Время после сдачи исполнителем задания заказчику, в течение которого заказчик может опротестовать задание или отправить его на доработку исполнителю",
                    IsRight = true,
                    Selected = false
                },
                new AnswerVariants() {
                    AnswerVariantText = "20 дней",
                    IsRight = true,
                    Selected = false
                }
            };

            postgreContext.AddRange(aRightAnswers);
            postgreContext.SaveChanges();
        }
    }
}
