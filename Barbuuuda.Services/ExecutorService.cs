using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы по работе с исполнителями сервиса.
    /// </summary>
    public sealed class ExecutorService : IExecutor
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;

        public ExecutorService(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden)
        {
            _db = db;
            _postgre = postgre;
            _iden = iden;
        }

        /// <summary>
        /// Метод выгружает список исполнителей сервиса.
        /// </summary>
        /// <returns>Список исполнителей.</returns>
        public async Task<IEnumerable> GetExecutorListAsync()
        {
            try
            {
                return await (from users in _postgre.Users
                              where users.UserRole.Equals(UserRole.EXECUTOR)
                              select new
                              {
                                  users.UserName,
                                  dateRegister = string.Format("{0:f}", users.DateRegister),
                                  users.AboutInfo
                              })
                          .ToListAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
                throw new Exception(ex.Message.ToString());
            }
        }


        /// <summary>
        /// Метод добавляет специализации исполнителя.
        /// </summary>
        /// <param name="specializations">Массив специализаций.</param>
        public async Task AddExecutorSpecializations(ExecutorSpecialization[] specializations, string executorName)
        {
            try
            {
                if (specializations.Length == 0)
                {
                    throw new ArgumentNullException();
                }

                UserEntity oExecutor = await _iden.AspNetUsers
                    .Where(e => e.UserName
                    .Equals(executorName))
                    .FirstOrDefaultAsync();

                oExecutor.Specializations = CheckEmptySpec(oExecutor, specializations);
                await _iden.SaveChangesAsync();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Передан пустой массив специализаций {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private ExecutorSpecialization[] CheckEmptySpec(UserEntity oExecutor, ExecutorSpecialization[] specializations)
        {
            // Если массив в БД был пустой, то заполнит его.
            if (oExecutor.Specializations == null)
            {
                oExecutor.Specializations = specializations;
            }

            // Если массив в БД не был пустой, то очистит его и заполнит заново.
            else
            {
                oExecutor.Specializations = Array.Empty<ExecutorSpecialization>();
                oExecutor.Specializations = specializations;
            }

            return oExecutor.Specializations;
        }

        /// <summary>
        /// Метод получает вопрос для теста исполнителя в зависимости от номера вопроса, переданного с фронта.
        /// </summary>
        /// <param name="numberQuestion">Номер вопроса.</param>
        /// <returns>Вопрос с вариантами ответов.</returns>
        public async Task<object> GetQuestionAsync(int numberQuestion)
        {
            try
            {
                if (numberQuestion == 0)
                {
                    throw new UserMessageException(TextException.ERROR_EMPTY_NUMBER_QUESTION);
                }

                var question = await _postgre.Questions
                .Join(_postgre.AnswerVariants,
                t1 => t1.QuestionId,
                t2 => t2.QuestionId,
                (t1, t2) => new
                {
                    t1.QuestionText,
                    t1.NumberQuestion,
                    t2.AnswerVariantText
                })
                .Where(q => q.NumberQuestion == numberQuestion)
                .FirstOrDefaultAsync();

                // Затирает верные ответы, чтобы фронт их не видел.
                question.AnswerVariantText.ToList().ForEach(el => el.IsRight = null);

                return question;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во вопросов для теста исполнителя.
        /// </summary>
        /// <returns>Кол-во вопросов.</returns>
        public async Task<int> GetCountAsync()
        {
            try
            {
                return await _postgre.Questions.CountAsync();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод проверяет результаты ответов на тест исполнителем.
        /// </summary>
        /// <param name="answers">Массив с ответами на тест.</param>
        ///  /// <param name="userName">Логин юзера.</param>
        /// <returns>Статус прохождения теста true/false.</returns>
        public async Task<bool> CheckAnswersTestAsync(List<AnswerVariant> answers, string userName)
        {
            try
            {
                if (answers.ToList().Count == 0)
                {
                    throw new UserMessageException(TextException.ERROR_EMPTY_INPUT_ARRAY_ANSWERS);
                }

                // Считает кол-во правильных ответов.
                int countError = 0;
                for (int i = 0; i < answers.Count; i++)
                {
                    bool? isRight = await _postgre.AnswerVariants
                        .Where(a => a.AnswerVariantText
                        .Equals(answers[i].AnswerVariantText))
                        .Select(s => s.AnswerVariantText[i].IsRight)
                        .SingleOrDefaultAsync();

                    if (!(bool)!isRight)
                    {
                        continue;
                    }

                    countError++;
                }

                // Если не все ответы были верными, то тест не пройден.
                if (!countError.Equals(answers.Count))
                {
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
