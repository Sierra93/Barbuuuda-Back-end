using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
        /// Метод получает список вопросов с вариантами ответа для теста исполнителя.
        /// </summary>
        /// <returns>Список вопросов с вариантами ответов.</returns>
        public async Task<IEnumerable> GetExecutorTestAsync()
        {
            IEnumerable aTests = await _postgre.Questions.Join(_postgre.AnswerVariants,
                t1 => t1.QuestionId,
                t2 => t2.QuestionId,
                (t1, t2) => new {
                    t1.QuestionId,
                    t1.QuestionText,
                    t2.AnswerVariantText
                }).ToListAsync();

            return aTests;
        }
    }
}
