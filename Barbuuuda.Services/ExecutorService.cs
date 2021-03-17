using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Task;
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
        private readonly IUser _user;

        public ExecutorService(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, IUser user)
        {
            _db = db;
            _postgre = postgre;
            _iden = iden;
            _user = user;
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
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
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
                int count = await _postgre.Questions.CountAsync();

                if (numberQuestion == 0)
                {
                    throw new UserMessageException(TextException.ERROR_EMPTY_NUMBER_QUESTION);
                }

                // Если номер вопроса некорректный.
                if (numberQuestion > count)
                {
                    throw new ErrorRangeAnswerException(numberQuestion);
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
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
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
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
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
                if (answers.Count == 0)
                {
                    throw new UserMessageException(TextException.ERROR_EMPTY_INPUT_ARRAY_ANSWERS);
                }
                
                List<bool> answersEqual = new List<bool>(); 

                // Считает кол-во правильных ответов.
                for (int i = 0; i < answers.Count; i++)
                {
                    // Уберет пробелы в начале и в конце.
                    answers[i].AnswerVariantText = CommonMethodsService.ReplaceSpacesString(answers[i].AnswerVariantText);

                    // Заменит флаг правильности с null на false.
                    if (answers[i].IsRight == null)
                    {
                        answers[i].IsRight = false;
                    }

                    // Находит такой ответ в БД.
                    AnswerVariantEntity answer = await _postgre.AnswerVariants
                        .Where(a => a.QuestionId
                        .Equals(answers[i].QuestionNumber))
                        .SingleOrDefaultAsync();

                    // Выбирает конкретный вариант для проверки правильности.
                    string rightVariant = answer.AnswerVariantText
                        .Where(a => a.IsRight.Equals(true))
                        .Select(a => a.AnswerVariantText)
                        .FirstOrDefault();

                    answers[i].IsRight = answers[i].AnswerVariantText.Equals(rightVariant);
                    answersEqual.Add((bool)answers[i].IsRight);
                }

                // Если не все ответы были верными, то тест не пройден.  
                bool isSuccessed = answersEqual.All(a => a.Equals(true));

                // Если исполнитель не прошел тест.
                if (!isSuccessed)
                {
                    return false;
                }

                UserEntity user = await _user.GetUserByLogin(userName);
                user.IsSuccessedTest = true;
                await _postgre.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает задания, которые находятся в работе у исполнителя. Т.е у которых статус "В работе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        public async Task<IEnumerable> GetTasksWorkAsync(string userName)
        {
            try
            {
                UserEntity user = await _user.GetUserByLogin(userName);

                return await (from tasks in _postgre.Tasks
                              join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                              join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                              join users in _postgre.Users on tasks.OwnerId equals users.Id
                              where statuses.StatusName.Equals(StatusTask.IN_WORK)
                              where users.Id.Equals(user.Id)
                              select new
                              {
                                  tasks.CategoryCode,
                                  tasks.CountOffers,
                                  tasks.CountViews,
                                  tasks.OwnerId,
                                  tasks.SpecCode,
                                  categories.CategoryName,
                                  tasks.StatusCode,
                                  statuses.StatusName,
                                  taskBegda = string.Format("{0:f}", tasks.TaskBegda),
                                  taskEndda = string.Format("{0:f}", tasks.TaskEndda),
                                  tasks.TaskTitle,
                                  tasks.TaskDetail,
                                  tasks.TaskId,
                                  taskPrice = string.Format("{0:0,0}", tasks.TaskPrice),
                                  tasks.TypeCode,
                                  userName
                              })
                          .OrderBy(o => o.TaskId)
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
        /// Метод оставляет ставку к заданию.
        /// </summary>
        /// <param name="taskId">Id задания, к которому оставляют ставку.</param>
        /// <param name="userName">Имя юзера.</param>
        public async Task RespondAsync(int taskId, string userName)
        {
            try
            {
                if (taskId == 0)
                {
                    throw new NullTaskIdException();
                }

                // Находит Id юзера.
                UserEntity user = await _user.GetUserByLogin(userName);

                // Находит задание по его TaskId.
                TaskEntity task = await _postgre.Tasks.Where(t => t.TaskId.Equals(taskId)).FirstOrDefaultAsync();

                if (task == null)
                {
                    throw new NotFoundTaskIdException(taskId);
                }

                // Проставит ставку к заданию.
                task.TaskMembers.Add(user.Id);
                _postgre.Tasks.Update(task);
                await _postgre.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
