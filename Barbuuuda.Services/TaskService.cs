using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
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
                CommonMethodsService<string> common = new CommonMethodsService<string>(_db);

                if (string.IsNullOrEmpty(oTask.TaskTitle) || string.IsNullOrEmpty(oTask.TaskDetail)) {
                    throw new ArgumentException();
                }

                // Проверяет существование заказчика, который создает задание.
                await IdentityCustomer(oTask.OwnerId);

                oTask.DateCreateTask = DateTime.Now;

                // Запишет статус "В аукционе".
                oTask.StatusCode = await _postgre.TaskStatuses
                    .Where(s => s.StatusName
                    .Equals(StatusTask.AUCTION))
                    .Select(s => s.StatusCode).FirstOrDefaultAsync();

                // Проверяет, есть ли такая категория в БД.
                await IdentityCategory(oTask.CategoryCode);

                // Проверяет существование специализации.
                await IdentitySpecialization(oTask.SpecCode);
                await _postgre.Tasks.AddAsync(oTask);
                await _postgre.SaveChangesAsync();

                return oTask;
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
    }
}
