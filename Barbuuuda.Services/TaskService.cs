using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Enums;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                //bool bSpec = await IdentitySpecialization(oTask.SpecCode);

                // Если все проверки прошли.
                if (bCustomer && bCategory) {
                    oTask.TaskBegda = DateTime.Now;

                    // Запишет код статуса "В аукционе".
                    oTask.StatusCode = await _postgre.TaskStatuses
                    .Where(s => s.StatusName
                    .Equals(StatusTask.AUCTION))
                    .Select(s => s.StatusCode).FirstOrDefaultAsync();

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
        /// Метод редактирует задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <returns>Вернет данные измененного задания.</returns>
        public async Task<TaskDto> EditTask(TaskDto oTask) {
            try {
                if (string.IsNullOrEmpty(oTask.TaskTitle) || string.IsNullOrEmpty(oTask.TaskDetail)) {
                    throw new ArgumentException();
                }

                // Проверяет существование заказчика, который создает задание.
                bool bCustomer = await IdentityCustomer(oTask.OwnerId);

                // Проверяет, есть ли такая категория в БД.
                bool bCategory = await IdentityCategory(oTask.CategoryCode);

                // Проверяет существование специализации.
                //bool bSpec = await IdentitySpecialization(oTask.SpecCode);

                // Если все проверки прошли.
                if (bCustomer && bCategory) {
                    // Запишет код статуса "В аукционе".
                    oTask.StatusCode = await _postgre.TaskStatuses
                    .Where(s => s.StatusName
                    .Equals(StatusTask.AUCTION))
                    .Select(s => s.StatusCode).FirstOrDefaultAsync();

                    // TODO: Доработать передачу с фронта для про или для всех.
                    oTask.TypeCode = "Для всех";

                    _postgre.Tasks.Update(oTask);
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
            //try {
            //    return await _postgre.TaskSpecializations
            //        .Where(s => s.SpecCode
            //        .Equals(code))
            //        .FirstOrDefaultAsync() != null ? true : throw new ArgumentNullException();
            //}

            //catch (ArgumentNullException ex) {
            //    throw new ArgumentNullException($"Специализации с таким кодом не найдено {ex.Message}");
            //}

            //catch (Exception ex) {
            //    throw new Exception(ex.Message.ToString());
            //}
            throw new NotImplementedException();
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
        //public async Task<IList> GetTaskSpecializations() {
        //    try {
        //        return await _postgre.TaskSpecializations.ToListAsync();
        //    }

        //    catch (Exception ex) {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}


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
                throw new ArgumentNullException($"Не передан UserId {ex.Message}");
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
                          where tasks.OwnerId == userId
                          select new {
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
                              users.UserLogin
                          })
                          .OrderBy(o => o.TaskId)
                          .ToListAsync(); 
        }

        /// <summary>
        /// Метод получает одно задание заказчика.
        /// </summary>
        /// <param name="id">Id задачи.</param>
        /// <param name="taskId">Id задачи. Может быть null.</param>
        /// <returns>Коллекцию заданий.</returns>
        async Task<IList> GetSingleTask(int userId, int? taskId) {
            // Выбирает объект задачи, который нужно редактировать.
            TaskDto oEditTask = await _postgre.Tasks.Where(t => t.TaskId == taskId).FirstOrDefaultAsync();

            // Выбирает список специализаций конкретной категории по коду категории.
            IList aTaskSpecializations = await _postgre.TaskCategories
                .Where(c => c.CategoryCode.Equals(oEditTask.CategoryCode))
                .Select(s => s.Specializations)
                .FirstOrDefaultAsync();
            string specName = string.Empty;

            // Выбирает название специализации.
            foreach (Specialization spec in aTaskSpecializations) {
                if (spec.SpecCode.Equals(oEditTask.SpecCode)) {
                    specName = spec.SpecName;
                }
            }

            var oTask = await (from tasks in _postgre.Tasks
                               join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                               join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                               join users in _postgre.Users on tasks.OwnerId equals users.UserId
                               where tasks.OwnerId == userId
                               where tasks.TaskId == taskId
                               select new {
                                   tasks.CategoryCode,
                                   tasks.CountOffers,
                                   tasks.CountViews,
                                   tasks.OwnerId,
                                   tasks.SpecCode,
                                   categories.CategoryName,
                                   specName,
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

            return oTask;
        }


        /// <summary>
        /// Метод удаляет задание.
        /// </summary>
        /// <param name="taskId">Id задачи.</param>
        public async Task DeleteTask(int taskId) {
            try {
                if (taskId == 0) {
                    throw new ArgumentNullException();
                }

                TaskDto oRemovedTask = await _postgre.Tasks.Where(t => t.TaskId == taskId).FirstOrDefaultAsync();
                _postgre.Tasks.Remove(oRemovedTask);
                await _postgre.SaveChangesAsync();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"TaskId не передан {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод фильтрует задания заказчика по параметру.
        /// </summary>
        /// <param name="query">Параметр фильтрации.</param>
        /// <returns>Отфильтрованные данные.</returns>
        public async Task<IList> FilterTask(string query) {
            try {
                IList aFilterCollection = null;

                if (string.IsNullOrEmpty(query)) {
                    throw new ArgumentNullException();
                }

                // Фильтрует список заданий в зависимости от параметра фильтрации.
                switch (query) {
                    // Фильтрует по дате создания задания.
                    case TaskFilterType.DATE_BEGDA:
                        aFilterCollection = await _postgre.Tasks.OrderBy(s => s.TaskBegda).ToListAsync();
                        break;

                    // Фильтрует по дате сдачи задания.
                    case TaskFilterType.DATE_ENDDA:
                        aFilterCollection = await _postgre.Tasks.OrderBy(s => s.TaskEndda).ToListAsync();
                        break;
                }

                return aFilterCollection;
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Не передан параметр запроса {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Метод ищет задание по Id или названию.
        /// </summary>
        /// <param name="param">Поисковый параметр.</param>
        /// <returns>Результат поиска.</returns>
        public async Task<IList> SearchTask(string param) {
            try {
                // Если ничего, значит выгрузить все.
                if (string.IsNullOrEmpty(param)) {
                    return await _postgre.Tasks.ToListAsync();
                }

                // Если передали Id.
                if (int.TryParse(param, out int bIntParse)) {
                    int taskId = Convert.ToInt32(param);
                    return await _postgre.Tasks
                    .Where(t => t.TaskId == taskId)
                    .ToListAsync();
                }

                // Если передали строку.
                if (param is string) {
                    return await _postgre.Tasks
                        .Where(t => t.TaskTitle.Contains(param))
                        .ToListAsync();
                }

                return null;
            }

            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод ищет задания указанной даты.
        /// </summary>
        /// <param name="date">Параметр даты.</param>
        /// <returns>Найденные задания.</returns>
        public async Task<IList> GetSearchTaskDate(string date) {
            try {
                if (string.IsNullOrEmpty(date)) {
                    throw new ArgumentNullException();
                }

                List<TaskDto> aTasks = new List<TaskDto>();

                // Выбирает задания форматируя даты для сравнений.
                foreach (TaskDto task in _postgre.Tasks) {
                    DateTime dt = Convert.ToDateTime(date);

                    // Форматирует дату с фронта оставляет только день, месяц и год.
                    string formatDate = Convert.ToDateTime(string.Format("{0:u}", dt).Replace("Z", "")).ToString("d");

                    // Форматирует дату с БД оставляет только день, месяц и год.
                    string dbFormatDate = task.TaskEndda.ToString("d");

                    if (dbFormatDate.Equals(formatDate)) {
                        aTasks.Add(task);
                    }
                }

                // Выбирает нужные задания форматируя даты к нужному виду.
                foreach (TaskDto oTask in aTasks) {
                    IList aResultTasks = await GetTasksByIds(oTask.TaskId);

                    return aResultTasks;
                }

                return null;
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Дата не передана {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод вернет задания по Id, которые передаются в метод.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <returns>Список заданий.</returns>
        async Task<IList> GetTasksByIds(int taskId) {
            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          join users in _postgre.Users on tasks.OwnerId equals users.UserId
                          where tasks.TaskId == taskId
                          select new {
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
                                  users.UserLogin
                              })
                          .OrderBy(o => o.TaskId)
                          .ToListAsync();
        }
    }
}
