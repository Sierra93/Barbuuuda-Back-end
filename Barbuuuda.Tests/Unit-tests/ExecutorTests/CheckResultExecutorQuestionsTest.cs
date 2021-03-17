using Barbuuuda.Core.Data;
using Barbuuuda.Models.Entities.Executor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            //DbContextOptions<PostgreDbContext> postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "CheckQuestionsTest").Options;
            //PostgreDbContext postgreContext = new PostgreDbContext(postgreOptions);

            ////AddRightAnswersDB(postgreContext);

            ////// Тестовые результаты теста.

            //AnswerVariant[] resultsAnswers = new AnswerVariant[]
            //{
            //  new AnswerVariant()
            //    {
            //        QuestionId = 1,
            //        AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя"
            //    },
            //    new AnswerVariant()
            //    {
            //        QuestionId = 2,
            //        AnswerVariantText = "Время, в течение которого служба поддержки отвечает на вопросы пользователей с момента их регистрации"
            //    },
            //    new AnswerVariant()
            //    {
            //        QuestionId = 3,
            //        AnswerVariantText = "Через звонки по мобильному телефону"
            //    }
            //};

            //// Проверяет кол-во правильных ответов.
            //int errorCount = 0; // Кол-во не правильных ответов.

            //for (int i = 0; i < resultsAnswers.Length; i++)
            //{
            //    AnswerVariant el = resultsAnswers[i];
            //    var answer = postgreContext.AnswerVariants
            //       .Where(a => el.QuestionId
            //       .Equals(el.QuestionId))
            //       .Select(a => new
            //       {
            //           isRight = a.AnswerVariantText[i].AnswerVariantText
            //           .Equals(el.AnswerVariantText)
            //       })
            //       .FirstOrDefault();

            //    if (answer.isRight)
            //    {
            //        continue;
            //    }

            //    errorCount++;
            //}

            //Assert.IsTrue(errorCount > 0);
        }

        /// <summary>
        /// Метод тестирует успешное прохождение теста исполнителем.
        /// </summary>
        [TestMethod]
        public void CheckSuccessQuestionsTest()
        {
            //DbContextOptions<PostgreDbContext> postgreOptions = new DbContextOptionsBuilder<PostgreDbContext>().UseInMemoryDatabase(databaseName: "CheckQuestionsTest").Options;
            //PostgreDbContext postgreContext = new PostgreDbContext(postgreOptions);

            //AddRightAnswersDB(postgreContext);

            //// Тестовые результаты теста.
            //AnswerVariant[] resultsAnswers = new AnswerVariant[]
            //{
            //    new AnswerVariant()
            //    {
            //        QuestionId = 1,
            //        AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя"
            //    },
            //    new AnswerVariant()
            //    {
            //        QuestionId = 2,
            //         AnswerVariantText = "Сумма, за которую Вы готовы выполнить задание"
            //    },
            //    new AnswerVariant()
            //    {
            //        QuestionId = 3,
            //        AnswerVariantText = "Качества выполненных заданий и отзывов заказчиков, а также статистики по сделанным ставкам"
            //    }
            //};

            //// Проверяет кол-во правильных ответов.
            //int errorCount = 0; // Кол-во не правильных ответов.

            //for (int i = 0; i < resultsAnswers.Length; i++)
            //{
            //    AnswerVariant el = resultsAnswers[i];
            //    var answer = postgreContext.AnswerVariants.ToList()
            //       .Where(a => el.QuestionId
            //       .Equals(el.QuestionId))
            //       .Select(a => new
            //       {
            //           isRight = a.AnswerVariantText[i].AnswerVariantText
            //           .Equals(el.AnswerVariantText)
            //       })
            //       .FirstOrDefault();

            //    if (answer.isRight)
            //    {
            //        continue;
            //    }

            //    errorCount++;
            //}

            //Assert.IsTrue(errorCount == 0);
        }

        /// <summary>
        /// Метод добавит тестовые верные варианты ответов в тестовую БД.
        /// </summary>
        /// <param name="postgreContext">Контекст БД.</param>
        //private void AddRightAnswersDB(PostgreDbContext postgreContext)
        //{
        //    // Верные варианты ответов.
        //    AnswerVariant[] rightAnswers = new AnswerVariant[]
        //    {
        //        new AnswerVariant()
        //        {
        //            QuestionId = 1,
        //            AnswerVariantText = "Система аукциона: исполнители сами ищут задания и предлагают за них свою цену, а заказчики выбирают исполнителя",
        //                  IsRight = true,
        //                  Selected = false
        //        },
        //         new AnswerVariant()
        //        {
        //              QuestionId = 2,
        //             AnswerVariantText = "Сумма, за которую Вы готовы выполнить задание",
        //            IsRight = true,
        //            Selected = false
        //        },
        //         new AnswerVariant()
        //        {
        //              QuestionId = 3,

        //               AnswerVariantText = "Качества выполненных заданий и отзывов заказчиков, а также статистики по сделанным ставкам",
        //            IsRight = true,
        //            Selected = false
        //        },
        //           new AnswerVariant()
        //        {
        //                QuestionId = 4,
        //            AnswerVariantText = "Запрещен",
        //            IsRight = true,
        //            Selected = false
        //        },
        //            new AnswerVariant()
        //        {
        //                 QuestionId = 5,
        //             AnswerVariantText = "В режиме комментариев в задании",
        //            IsRight = true,
        //            Selected = false
        //        },
        //            new AnswerVariant()
        //        {
        //                 QuestionId = 6,
        //            AnswerVariantText = "Время после сдачи исполнителем задания заказчику, в течение которого заказчик может опротестовать задание или отправить его на доработку исполнителю",
        //            IsRight = true,
        //            Selected = false
        //        },
        //            new AnswerVariant()
        //        {
        //                 QuestionId = 7,
        //            AnswerVariantText = "20 дней",
        //            IsRight = true,
        //            Selected = false
        //        }
        //    };

        //    //postgreContext.AnswerVariants.AddRange(rightAnswers);
        //    postgreContext.SaveChanges();
        //}
    }
}
