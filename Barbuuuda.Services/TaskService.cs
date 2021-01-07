using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Enums;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Services {
    /// <summary>
    /// Сервис реализует методы заданий.
    /// </summary>
    public class TaskService : ITask {
        ApplicationDbContext _db;
        PostgreDbContext _postgre;

        public TaskService(ApplicationDbContext db, PostgreDbContext postgre) {
            _db = db;
            _postgre = postgre;
        }

        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        public async Task<TaskDto> CreateTask(TaskDto oTask) {
            try {
                if (string.IsNullOrEmpty(oTask.TaskTitle) || string.IsNullOrEmpty(oTask.TaskDetail)) {
                    throw new ArgumentException();
                }

                // Проверяет существование заказчика, который создает задание.
                bool bCustomer = await IdentityCustomer(oTask.OwnerId);

                // Проверяет, есть ли такая категория в БД.
                bool bCategory = await IdentityCategory(oTask.CategoryCode);

                // Проверяет существование специализации.
                bool bSpec = await IdentitySpecialization(oTask.SpecCode);

                // Если все проверки прошли.
                if (bCustomer && bCategory && bSpec) {
                    oTask.TaskBegda = DateTime.Now;

                    // Запишет статус "В аукционе".
                    oTask.StatusCode = StatusTask.AUCTION;

                    // TODO: Доработать передачу с фронта для про или для всех.
                    oTask.TypeCode = "Для всех";                    
                    
                    await _postgre.Tasks.AddAsync(oTask);
                    await _postgre.SaveChangesAsync();

                    return oTask;
                }

                throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Не все проверки пройдены {ex.Message}");
            }

            catch (ArgumentException ex) {
                throw new ArgumentException($"Не все обязательные поля заполнены {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод ищет заказчика в БД.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true/false</returns>
        async Task<bool> IdentityCustomer(int userId) {
            try {
                if (userId == 0) {
                    throw new ArgumentNullException();
                }

                UserDto oUser = await _postgre.Users.Where(u => u.UserId.Equals(userId)).FirstOrDefaultAsync();

                return oUser != null ? true : throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Заказчик с таким Id не найден {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод находит категорию заданий.
        /// </summary>
        /// <param name="category">Название категории.</param>
        /// <returns>true/false</returns>
        async Task<bool> IdentityCategory(string categoryCode) {
           try {
                return await _postgre.TaskCategories
                    .Where(c => c.CategoryCode
                    .Equals(categoryCode))
                    .FirstOrDefaultAsync() != null ? true : throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Такой категории не найдено {ex.Message}");
            }
        }

        /// <summary>
        /// Метод проверяет существование специализации.
        /// </summary>
        /// <param name="specId"></param>
        /// <returns></returns>
        async Task<bool> IdentitySpecialization(string code) {
            try {
                return await _postgre.TaskSpecializations
                    .Where(s => s.SpecCode
                    .Equals(code))
                    .FirstOrDefaultAsync() != null ? true : throw new ArgumentNullException();
            }
            
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Специализации с таким кодом не найдено {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        public async Task<IList> GetTaskCategories() {
            try {
                return await _postgre.TaskCategories.ToListAsync();
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает список специализаций заданий.
        /// </summary>
        /// <returns>Коллекцию специализаций.</returns>
        public async Task<IList> GetTaskSpecializations() {
            try {
                return await _postgre.TaskSpecializations.ToListAsync();
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }


        /// <summary>
        /// Метод получает список заданий заказчика.
        /// </summary>
        /// <param name="userId">Id заказчика.</param>
        /// <param name="type">Параметр получения заданий либо все либо одно.</param>
        /// <returns>Коллекция заданий.</returns>
        public async Task<IList> GetTasksList(int userId, int? taskId, string type) {
            try {
                IList aResultTaskObj = null;

                if (userId == 0) {
                    throw new ArgumentNullException();
                }

                // Вернет либо все задания либо одно.
                return aResultTaskObj = type.Equals(GetTaskTypeEnum.All.ToString()) 
                    ? aResultTaskObj = await GetAllTasks(userId)
                    : aResultTaskObj = await GetSingleTask(userId, taskId);
            }
           
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Не передан Id {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает все задания заказчика.
        /// </summary>
        /// <param name="id">Id заказчика.</param>
        /// <returns>Коллекцию заданий.</returns>
        async Task<IList> GetAllTasks(int userId) {
            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          join users in _postgre.Users on tasks.OwnerId equals users.UserId
                          where tasks.OwnerId.Equals(userId)
                          select new {
                              tasks.CategoryCode,
                              tasks.CountOffers,
                              tasks.CountViews,
                              tasks.OwnerId,
                              tasks.SpecCode,
                              categories.CategoryName,
                              tasks.StatusCode,
                              statuses.StatusName,
                              tasks.TaskBegda,
                              tasks.TaskEndda,
                              tasks.TaskTitle,
                              tasks.TaskDetail,
                              tasks.TaskId,
                              taskPrice = string.Format("{0:0,0}", tasks.TaskPrice),
                              tasks.TypeCode,
                              users.UserLogin
                          }).ToListAsync(); 
        }

        /// <summary>
        /// Метод получает одно задание заказчика.
        /// </summary>
        /// <param name="id">Id задачи.</param>
        /// <returns>Коллекцию заданий.</returns>
        async Task<IList> GetSingleTask(int userId, int? taskId) {
            return await _postgre.Tasks
                .Where(u => u.OwnerId == userId)
                .Where(t => t.TaskId == taskId).ToListAsync();
        }
    }
}
