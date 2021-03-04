using Barbuuuda.Core.Data;
using Barbuuuda.Models.Entities.Executor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Barbuuuda.Tests.Unit_tests.ExecutorTests
{
    /// <summary>
    /// Класс тестирует проверку результатов ответов на вопросы теста исполнителем.
    /// </summary>
    [TestClass]
    public class CheckResultExecutorQuestionsTest
    {
        /// <summary>
        /// Метод тестирует провал теста исполнителем.
        /// </summary>
        [TestMethod]
        public void CheckFailQuestionsTest()
        {
            DbContextOptions<PostgreDbContext> postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "CheckQuestionsTest").Options;
            PostgreDbContext postgreContext = new PostgreDbContext(postgreOptions);

            AddRightAnswersDB(postgreContext);

            // Тестовые результаты теста.
            AnswerVariantEntity[] resultsAnswers = new AnswerVariantEntity[]
            {
                new AnswerVariantEntity()
                {
                    QuestionId = 1,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя"
                    }
                },
                new AnswerVariantEntity()
                {
                    QuestionId = 2,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Время, в течение которого служба поддержки отвечает на вопросы пользователей с момента их регистрации"
                    }
                },
                new AnswerVariantEntity()
                {
                    QuestionId = 3,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Через звонки по мобильному телефону"
                    }
                }
            };

            // Проверяет кол-во правильных ответов.
            int errorCount = 0; // Кол-во не правильных ответов.

            foreach (AnswerVariantEntity el in resultsAnswers)
            {
                var answer = postgreContext.AnswerVariants.ToList()
                   .Where(a => a.QuestionId
                   .Equals(el.QuestionId))
                   .Select(a => new
                   {
                       isRight = a.AnswerVariantText.AnswerVariantText
                       .Equals(el.AnswerVariantText.AnswerVariantText)
                   })
                   .FirstOrDefault();

                if (answer.isRight)
                {
                    continue;
                }

                errorCount++;
            }

            Assert.IsTrue(errorCount > 0);
        }

        /// <summary>
        /// Метод тестирует успешное прохождение теста исполнителем.
        /// </summary>
        [TestMethod]
        public void CheckSuccessQuestionsTest()
        {
            DbContextOptions<PostgreDbContext> postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "CheckQuestionsTest").Options;
            PostgreDbContext postgreContext = new PostgreDbContext(postgreOptions);

            AddRightAnswersDB(postgreContext);

            // Тестовые результаты теста.
            AnswerVariantEntity[] resultsAnswers = new AnswerVariantEntity[]
            {
                new AnswerVariantEntity()
                {
                    QuestionId = 1,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя"
                    }
                },
                new AnswerVariantEntity()
                {
                    QuestionId = 2,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Сумма, за которую Вы готовы выполнить задание"
                    }
                },
                new AnswerVariantEntity()
                {
                    QuestionId = 3,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Качества выполненных заданий и отзывов заказчиков, а также статистики по сделанным ставкам"
                    }
                }
            };

            // Проверяет кол-во правильных ответов.
            int errorCount = 0; // Кол-во не правильных ответов.

            foreach (var el in resultsAnswers)
            {
                var answer = postgreContext.AnswerVariants.ToList()
                   .Where(a => a.QuestionId
                   .Equals(el.QuestionId))
                   .Select(a => new
                   {
                       isRight = a.AnswerVariantText.AnswerVariantText
                       .Equals(el.AnswerVariantText.AnswerVariantText)
                   })
                   .FirstOrDefault();

                if (answer.isRight)
                {
                    continue;
                }

                errorCount++;
            }

            Assert.IsTrue(errorCount == 0);
        }

        /// <summary>
        /// Метод добавит тестовые верные варианты ответов в тестовую БД.
        /// </summary>
        /// <param name="postgreContext">Контекст БД.</param>
        private void AddRightAnswersDB(PostgreDbContext postgreContext)
        {
            // Верные варианты ответов.
            AnswerVariantEntity[] rightAnswers = new AnswerVariantEntity[]
            {
                new AnswerVariantEntity()
                {
                    QuestionId = 1,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя",
                          IsRight = true,
                          Selected = false
                    }
                },
                 new AnswerVariantEntity()
                {
                      QuestionId = 2,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "Сумма, за которую Вы готовы выполнить задание",
                    IsRight = true,
                    Selected = false
                    }
                },
                 new AnswerVariantEntity()
                {
                      QuestionId = 3,
                    AnswerVariantText = new AnswerVariants()
                    {
                       AnswerVariantText = "Качества выполненных заданий и отзывов заказчиков, а также статистики по сделанным ставкам",
                    IsRight = true,
                    Selected = false
                    }
                },
                   new AnswerVariantEntity()
                {
                        QuestionId = 4,
                    AnswerVariantText = new AnswerVariants()
                    {
                       AnswerVariantText = "Запрещен",
                    IsRight = true,
                    Selected = false
                    }
                },
                    new AnswerVariantEntity()
                {
                         QuestionId = 5,
                    AnswerVariantText = new AnswerVariants()
                    {
                        AnswerVariantText = "В режиме комментариев в задании",
                    IsRight = true,
                    Selected = false
                    }
                },
                    new AnswerVariantEntity()
                {
                         QuestionId = 6,
                    AnswerVariantText = new AnswerVariants()
                    {
                         AnswerVariantText = "Время после сдачи исполнителем задания заказчику, в течение которого заказчик может опротестовать задание или отправить его на доработку исполнителю",
                    IsRight = true,
                    Selected = false
                    }
                },
                    new AnswerVariantEntity()
                {
                         QuestionId = 7,
                    AnswerVariantText = new AnswerVariants()
                    {  AnswerVariantText = "20 дней",
                    IsRight = true,
                    Selected = false
                    }
                }
            };

            postgreContext.AnswerVariants.AddRange(rightAnswers);
            postgreContext.SaveChanges();
        }
    }
}
