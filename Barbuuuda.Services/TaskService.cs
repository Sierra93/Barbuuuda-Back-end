using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Enums;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Logger;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
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
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserDto> _userManager;
        private readonly SignInManager<UserDto> _signInManager;

        public TaskService(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserDto> userManager, SignInManager<UserDto> signInManager) {
            _db = db;
            _postgre = postgre;
            _userManager = userManager;
            _signInManager = signInManager;
            _iden = iden;
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

                // Проверяет существование заказчика, который создал задание.
                bool bCustomer = await IdentityCustomer(oTask.OwnerId);

                // Проверяет, есть ли такая категория в БД.
                bool bCategory = await IdentityCategory(oTask.CategoryCode);

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
        async Task<bool> IdentityCustomer(string userId) {
            try {
                if (string.IsNullOrEmpty(userId)) {
                    throw new ArgumentNullException();
                }

                UserDto oUser = await _postgre.Users.Where(u => u.Id.Equals(userId)).FirstOrDefaultAsync();

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
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        public async Task<IList> GetTaskCategories() {
            try {
                return await _postgre
                    .TaskCategories
                    .OrderBy(t => t.CategoryId)
                    .ToListAsync();
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
        public async Task<IList> GetTasksList(string userId, int? taskId, string type) {
            try {
                IList aResultTaskObj = null;

                if (string.IsNullOrEmpty(userId)) {
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
        async Task<IList> GetAllTasks(string userId) {
            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          join users in _iden.AspNetUsers on tasks.OwnerId equals users.Id
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
                              taskBegda = string.Format("{0:f}", tasks.TaskBegda),
                              taskEndda = string.Format("{0:f}", tasks.TaskEndda),
                              tasks.TaskTitle,
                              tasks.TaskDetail,
                              tasks.TaskId,
                              taskPrice = string.Format("{0:0,0}", tasks.TaskPrice),
                              tasks.TypeCode,
                              users.UserName
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
        async Task<IList> GetSingleTask(string userId, int? taskId) {
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
                               join users in _postgre.Users on tasks.OwnerId equals users.Id
                               where tasks.OwnerId.Equals(userId)
                               where tasks.TaskId.Equals(taskId)
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
                                   taskBegda = string.Format("{0:f}", tasks.TaskBegda),
                                   taskEndda = string.Format("{0:f}", tasks.TaskEndda),
                                   tasks.TaskTitle,
                                   tasks.TaskDetail,
                                   tasks.TaskId,
                                   taskPrice = string.Format("{0:0,0}", tasks.TaskPrice),
                                   tasks.TypeCode,
                                   users.UserName
                               })
                               .ToListAsync();

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
                // Находит задание переводя все в нижний регистр для поиска.
                if (param is string) {
                    return await _postgre.Tasks
                        .Where(t => t.TaskTitle.ToLower()
                        .Contains(param.ToLower()))
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
                          join users in _postgre.Users on tasks.OwnerId equals users.Id
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
                                  users.UserName
                              })
                          .OrderBy(o => o.TaskId)
                          .ToListAsync();
        }

        /// <summary>
        /// Метод выгружает активные задания заказчика.
        /// Активными считаются задания в статусе в аукционе и в работе.
        /// </summary>
        /// <returns>Список активных заданий.</returns>
        public async Task<IList> LoadActiveTasks(string userId) {
            try {
                return !string.IsNullOrEmpty(userId) ? await GetActiveTasks(userId) : throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Не передан Id {ex.Message}");
            }

            catch (Exception ex) {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает активные задания в статусах "В аукционе" и "В работе".
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        async Task<IList> GetActiveTasks(string userId) {
            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          join users in _postgre.Users on tasks.OwnerId equals users.Id
                          where statuses.StatusName.Equals(StatusTask.AUCTION) ||
                          statuses.StatusName.Equals(StatusTask.IN_WORK)
                          where users.Id.Equals(userId)
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
                              users.UserName
                          })
                          .OrderBy(o => o.TaskId)
                          .ToListAsync();
        }

        /// <summary>
        /// Метод получает задания в статусе "В аукционе".
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        async Task<IList> GetAuctionTasks() {
            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          join users in _postgre.Users on tasks.OwnerId equals users.Id
                          where statuses.StatusName.Equals(StatusTask.AUCTION)
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
                              users.UserName
                          })
                          .OrderBy(o => o.TaskId)
                          .ToListAsync();
        }

        /// <summary>
        /// Метод получает кол-во задач определенного статуса.
        /// </summary>
        /// <param name="status">Имя статуса, кол-во задач которых нужно получить.</param>
        /// <returns>Число кол-ва задач.</returns>
        public async Task<object> GetCountTaskStatuses() {
            try {
                int countTotal = await GetStatusName(StatusTask.TOTAL);
                int countAuction = await GetStatusName(StatusTask.AUCTION);
                int countWork = await GetStatusName(StatusTask.IN_WORK);
                int countGarant = await GetStatusName(StatusTask.GARANT);
                int countComplete = await GetStatusName(StatusTask.COMPLETE);
                int countPerechet = await GetStatusName(StatusTask.PERECHET);
                int countDraft = await GetStatusName(StatusTask.DRAFT);

                return new {
                    total = countTotal,
                    auction = countAuction,
                    work = countWork,
                    garant = countGarant,
                    complete = countComplete,
                    perechet = countPerechet,
                    draft = countDraft
                };
            }

            catch (Exception ex) {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во заданий определенного статуса.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        async Task<int> GetStatusName(string status) {
            return await _postgre.Tasks
                .Join(_postgre.TaskStatuses,
                t => t.StatusCode,
                s => s.StatusCode,                
                (t, s) => new { s.StatusName })
                .Where(s => s.StatusName
                .Equals(status))                
                .CountAsync();
        }

        /// <summary>
        /// Метод получает задания определенного статуса.
        /// </summary>
        /// <param name="status">Название статуса.</param>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Список заданий с определенным статусом.</returns>
        public async Task<IList> GetStatusTasks(string status, string userId) {
            try {
                return string.IsNullOrEmpty(status) ? throw new ArgumentNullException() :
                     await (from tasks in _postgre.Tasks
                                   join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                                   join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                                   join users in _postgre.Users on tasks.OwnerId equals users.Id
                                   where statuses.StatusName.Equals(status)
                                   where users.Id.Equals(userId)
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
                                       users.UserName
                                   })
                          .OrderBy(o => o.TaskId)
                          .ToListAsync();
            }

            catch (ArgumentNullException ex) {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new ArgumentNullException($"Не передан статус {ex.Message}");
            }

            catch (Exception ex) {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во заданий всего.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns></returns>
        public async Task<int> GetTotalCountTasks(string userId) {
            try {
                return !string.IsNullOrEmpty(userId) ? await _postgre.Tasks
                    .Join(_iden.AspNetUsers,
                    t => t.OwnerId,
                    u => u.Id,
                    (t, u) => new { u.Id })
                    .Where(u => u.Id.Equals(userId))
                    .CountAsync() : throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex) {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new ArgumentNullException($"Id не передан {ex.Message}");
            }

            catch (Exception ex) {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает список заданий в аукционе. Выводит задания в статусе "В аукционе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        public async Task<IList> LoadAuctionTasks() {
            try {
                return await GetAuctionTasks();
            }

            catch (Exception ex) {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
