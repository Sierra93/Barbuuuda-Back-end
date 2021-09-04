using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Respond.Output;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.Task.Output;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Entities.Task;
using Barbuuuda.Models.Task.Input;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы заданий.
    /// </summary>
    public sealed class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IUserService _userService;

        public TaskService(ApplicationDbContext db, PostgreDbContext postgre, IUserService userService)
        {
            _db = db;
            _postgre = postgre;
            _userService = userService;
        }

        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <param name="userName">Login юзера.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        public async Task<TaskEntity> CreateTask(TaskEntity task, string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(task.TaskTitle) || string.IsNullOrEmpty(task.TaskDetail))
                {
                    throw new ArgumentException();
                }

                // Проверяет существование заказчика, который создает задание.
                var user = await IdentityCustomer(userName);

                // Проверяет, есть ли такая категория в БД.
                bool bCategory = await IdentityCategory(task.CategoryCode);

                // Если все проверки прошли.
                if (user != null && bCategory)
                {
                    task.TaskBegda = DateTime.Now;

                    // Запишет код статуса "В аукционе".
                    task.StatusCode = await _postgre.TaskStatuses
                    .Where(s => s.StatusName
                    .Equals(StatusTask.AUCTION))
                    .Select(s => s.StatusCode)
                    .FirstOrDefaultAsync();

                    // Запишет Id заказчика, создавшего задание.
                    task.OwnerId = user.Id;

                    // TODO: Доработать передачу с фронта для про или для всех.
                    task.TypeCode = "Для всех";

                    // Найдет и увеличит последний PK.
                    var incrementTaskId = await _postgre.Tasks.Select(x => x.TaskId).MaxAsync();

                    task.TaskId = ++incrementTaskId;

                    await _postgre.Tasks.AddAsync(task);
                    await _postgre.SaveChangesAsync();

                    return task;
                }

                throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не все проверки пройдены {ex.Message}");
            }

            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Не все обязательные поля заполнены {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод редактирует задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <param name="userName">Login юзера.</param>
        /// <returns>Вернет данные измененного задания.</returns>
        public async Task<TaskEntity> EditTask(TaskInput task, string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(task.TaskTitle)
                    || string.IsNullOrEmpty(task.TaskDetail)
                    || task.TaskId <= 0
                    || task.TaskId == null)
                {
                    throw new ArgumentException();
                }

                // Проверяет существование заказчика, который создал задание.
                var user = await IdentityCustomer(userName);

                // Проверяет, есть ли такая категория в БД.
                var bCategory = await IdentityCategory(task.CategoryCode);

                // Найдет задание, которое нужно изменить.
                var updateTask = await (from t in _postgre.Tasks
                                        where t.TaskId == task.TaskId
                                        select t)
                    .FirstOrDefaultAsync();

                // Если все проверки прошли.
                if (user != null && bCategory && updateTask != null)
                {
                    // Запишет Id заказчика.
                    updateTask.OwnerId = user.Id;

                    // Запишет код статуса "В аукционе".
                    updateTask.StatusCode = await _postgre.TaskStatuses
                        .Where(s => s.StatusName
                        .Equals(StatusTask.AUCTION))
                        .Select(s => s.StatusCode)
                        .FirstOrDefaultAsync();

                    // TODO: Доработать передачу с фронта для про или для всех.
                    updateTask.TypeCode = "Для всех";

                    // Обновит поля задания.
                    updateTask.TaskTitle = task.TaskTitle;
                    updateTask.TaskDetail = task.TaskDetail;
                    updateTask.CategoryCode = task.CategoryCode;
                    updateTask.SpecCode = task.SpecCode;
                    updateTask.TaskEndda = task.TaskEndda;
                    updateTask.TaskPrice = task.TaskPrice;

                    await _postgre.SaveChangesAsync();

                    return updateTask;
                }

                throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не все проверки пройдены {ex.Message}");
            }

            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Не все обязательные поля заполнены {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод ищет заказчика в БД.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true/false</returns>
        async Task<UserEntity> IdentityCustomer(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException();
                }

                var user = await _postgre.Users
                    .Where(u => u.UserName.Equals(userName))
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new ArgumentNullException();
                }

                return user;
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Заказчик с таким UserName не найден {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод находит категорию заданий.
        /// </summary>
        /// <param name="category">Название категории.</param>
        /// <returns>true/false</returns>
        async Task<bool> IdentityCategory(string categoryCode)
        {
            try
            {
                return await _postgre.TaskCategories
                    .Where(c => c.CategoryCode
                    .Equals(categoryCode))
                    .FirstOrDefaultAsync() != null ? true : throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Такой категории не найдено {ex.Message}");
            }
        }

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        public async Task<IList> GetTaskCategories()
        {
            try
            {
                return await _postgre
                    .TaskCategories
                    .OrderBy(t => t.CategoryId)
                    .ToListAsync();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает список заданий заказчика.
        /// </summary>
        /// <param name="userName">Login заказчика.</param>
        /// <param name="type">Параметр получения заданий либо все либо одно.</param>
        /// <param name="taskId">TaskId задания, которое нужно получить.</param>
        /// <returns>Коллекция заданий.</returns>
        public async Task<IList> GetTasksList(string userName, long? taskId, string type)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException();
                }               

                if (!type.Equals(TaskType.ALL) && !type.Equals(TaskType.SINGLE))
                {
                    throw new NotParameterException(type);
                }

                // Вернет либо все задания либо одно.
                return type.Equals(TaskType.ALL)
                    ? await GetAllTasks(userName)
                    : await GetSingleTask(userName, taskId);
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не передан Id {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает все задания заказчика.
        /// </summary>
        /// <param name="userId">Id заказчика.</param>
        /// <returns>Коллекцию заданий.</returns>
        async Task<IList> GetAllTasks(string userName)
        {
            string userId = await GetUserByName(userName);

            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          where tasks.OwnerId.Equals(userId)
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

        /// <summary>
        /// Метод получает одно задание заказчика.
        /// </summary>
        /// <param name="userName">Логин юзера.</param>
        /// <param name="taskId">Id задачи. Может быть null.</param>
        /// <returns>Коллекцию заданий.</returns>
        async Task<IList> GetSingleTask(string userName, long? taskId)
        {
            // Выбирает объект задачи, который нужно редактировать.
            TaskEntity oEditTask = await _postgre.Tasks.Where(t => t.TaskId == taskId).FirstOrDefaultAsync();

            // Выбирает список специализаций конкретной категории по коду категории.
            IList aTaskSpecializations = await _postgre.TaskCategories
                .Where(c => c.CategoryCode.Equals(oEditTask.CategoryCode))
                .Select(s => s.Specializations)
                .FirstOrDefaultAsync();
            string specName = string.Empty;

            // Выбирает название специализации.
            foreach (Specialization spec in aTaskSpecializations)
            {
                if (spec.SpecCode.Equals(oEditTask.SpecCode))
                {
                    specName = spec.SpecName;
                }
            }

            // Получит логин и иконку профиля заказчика задания.
            CustomerOutput customer = await _userService.GetCustomerLoginByTaskId(taskId);

            // TODO: отрефачить этот метод, чтоб не обращаться два раза к БД за получением задания.            
            var oTask = await (from tasks in _postgre.Tasks
                               join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                               join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                               where tasks.TaskId == taskId
                               select new
                               {
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
                                   //taskEndda = string.Format("{0:f}", tasks.TaskEndda),
                                   tasks.TaskEndda,
                                   tasks.TaskTitle,
                                   tasks.TaskDetail,
                                   tasks.TaskId,
                                   taskPrice = string.Format("{0:0,0}", tasks.TaskPrice),
                                   tasks.TypeCode,
                                   userName,
                                   customerLogin = customer.UserName,
                                   customerProfileIcon = customer.UserIcon
                               })
                               .ToListAsync();

            return oTask;
        }


        /// <summary>
        /// Метод удаляет задание.
        /// </summary>
        /// <param name="taskId">Id задачи.</param>
        /// <returns>Статус удаления.</returns>
        public async Task<bool> DeleteTask(long taskId)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new ArgumentNullException();
                }

                var removeTask = await _postgre.Tasks
                    .Where(t => t.TaskId == taskId)
                    .FirstOrDefaultAsync();

                if (removeTask == null)
                {
                    return false;
                }

                _postgre.Tasks.Remove(removeTask);

                await _postgre.SaveChangesAsync();

                return true;
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"TaskId не передан {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод фильтрует задания заказчика по параметру.
        /// </summary>
        /// <param name="query">Параметр фильтрации.</param>
        /// <returns>Отфильтрованные данные.</returns>
        public async Task<IList> FilterTask(string query)
        {
            try
            {
                IList aFilterCollection = null;

                if (string.IsNullOrEmpty(query))
                {
                    throw new ArgumentNullException();
                }

                // Фильтрует список заданий в зависимости от параметра фильтрации.
                switch (query)
                {
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

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не передан параметр запроса {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Метод ищет задание по Id или названию.
        /// </summary>
        /// <param name="param">Поисковый параметр.</param>
        /// <returns>Результат поиска.</returns>
        public async Task<IList> SearchTask(string param)
        {
            try
            {
                // Если ничего, значит выгрузить все.
                if (string.IsNullOrEmpty(param))
                {
                    return await _postgre.Tasks.ToListAsync();
                }

                // Если передали Id.
                if (int.TryParse(param, out int bIntParse))
                {
                    int taskId = Convert.ToInt32(param);
                    return await _postgre.Tasks
                    .Where(t => t.TaskId == taskId)
                    .ToListAsync();
                }

                // Если передали строку.
                // Находит задание переводя все в нижний регистр для поиска.
                if (param is string)
                {
                    return await _postgre.Tasks
                        .Where(t => t.TaskTitle.ToLower()
                        .Contains(param.ToLower()))
                        .ToListAsync();
                }

                return null;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод ищет задания указанной даты.
        /// </summary>
        /// <param name="date">Параметр даты.</param>
        /// <returns>Найденные задания.</returns>
        public async Task<IList> GetSearchTaskDate(string date)
        {
            try
            {
                if (string.IsNullOrEmpty(date))
                {
                    throw new ArgumentNullException();
                }

                List<TaskEntity> aTasks = new List<TaskEntity>();

                // Выбирает задания форматируя даты для сравнений.
                foreach (TaskEntity task in _postgre.Tasks)
                {
                    DateTime dt = Convert.ToDateTime(date);

                    // Форматирует дату с фронта оставляет только день, месяц и год.
                    string formatDate = Convert.ToDateTime(string.Format("{0:u}", dt).Replace("Z", "")).ToString("d");

                    // Форматирует дату с БД оставляет только день, месяц и год.
                    string dbFormatDate = task.TaskEndda.ToString("d");

                    if (dbFormatDate.Equals(formatDate))
                    {
                        aTasks.Add(task);
                    }
                }

                // Выбирает нужные задания форматируя даты к нужному виду.
                foreach (TaskEntity oTask in aTasks)
                {
                    IList aResultTasks = await GetTasksByIds(oTask.TaskId);

                    return aResultTasks;
                }

                return null;
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Дата не передана {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод вернет задания по Id, которые передаются в метод.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <returns>Список заданий.</returns>
        async Task<IList> GetTasksByIds(int taskId)
        {
            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          join users in _postgre.Users on tasks.OwnerId equals users.Id
                          where tasks.TaskId == taskId
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
        public async Task<IList> LoadActiveTasks(string userName)
        {
            try
            {
                return !string.IsNullOrEmpty(userName) ? await GetActiveTasks(userName) : 
                    throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не передан UserName {ex.Message}");
            }

            catch (Exception ex)
            {
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
        async Task<IList> GetActiveTasks(string userName)
        {
            string userId = await GetUserLoginById(userName);

            return await (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          join users in _postgre.Users on tasks.OwnerId equals users.Id
                          where statuses.StatusName.Equals(StatusTask.AUCTION) ||
                          statuses.StatusName.Equals(StatusTask.IN_WORK)
                          where users.Id.Equals(userId)
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

        /// <summary>
        /// Метод получает кол-во задач определенного статуса.
        /// </summary>
        /// <param name="status">Имя статуса, кол-во задач которых нужно получить.</param>
        /// <returns>Число кол-ва задач.</returns>
        public async Task<object> GetCountTaskStatuses()
        {
            try
            {
                int countAuction = await GetStatusName(StatusTask.AUCTION);
                int countWork = await GetStatusName(StatusTask.IN_WORK);
                int countGarant = await GetStatusName(StatusTask.GARANT);
                int countComplete = await GetStatusName(StatusTask.COMPLETE);
                int countPerechet = await GetStatusName(StatusTask.PERECHET);
                int countDraft = await GetStatusName(StatusTask.DRAFT);
                int total = await GetStatusName(StatusTask.TOTAL);

                return new
                {
                    auction = countAuction,
                    work = countWork,
                    garant = countGarant,
                    complete = countComplete,
                    perechet = countPerechet,
                    draft = countDraft,
                    total
                };
            }

            catch (Exception ex)
            {
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
        async Task<int> GetStatusName(string status)
        {
            int tasksCount = 0;

            if (status.Equals(StatusTask.TOTAL))
            {
                tasksCount = await _postgre.Tasks
                    .Join(_postgre.TaskStatuses,
                        t => t.StatusCode,
                        s => s.StatusCode,
                        (t, s) => new { s.StatusName })
                    .CountAsync();
            }

            else
            {
                tasksCount = await _postgre.Tasks
                    .Join(_postgre.TaskStatuses,
                        t => t.StatusCode,
                        s => s.StatusCode,
                        (t, s) => new { s.StatusName })
                    .Where(s => s.StatusName
                        .Equals(status))
                    .CountAsync();
            }

            return tasksCount;
        }

        /// <summary>
        /// Метод получает задания определенного статуса.
        /// </summary>
        /// <param name="status">Название статуса.</param>
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Список заданий с определенным статусом.</returns>
        public async Task<GetTaskResultOutput> GetStatusTasks(string status, string userName)
        {
            try
            {
                var result = new GetTaskResultOutput();

                var userId = await GetUserByName(userName);

                var tasks = string.IsNullOrEmpty(status) ? throw new ArgumentNullException() :
                    await (from task in _postgre.Tasks
                            join categories in _postgre.TaskCategories on task.CategoryCode equals categories.CategoryCode
                            join statuses in _postgre.TaskStatuses on task.StatusCode equals statuses.StatusCode
                            where statuses.StatusName.Equals(status)
                            where task.OwnerId.Equals(userId)
                            select new
                            {
                                task.CategoryCode,
                                task.CountOffers,
                                task.CountViews,
                                task.OwnerId,
                                task.SpecCode,
                                categories.CategoryName,
                                task.StatusCode,
                                statuses.StatusName,
                                TaskBegda = string.Format("{0:f}", task.TaskBegda),
                                TaskEndda = string.Format("{0:f}", task.TaskEndda),
                                task.TaskTitle,
                                task.TaskDetail,
                                task.TaskId,
                                TaskPrice = string.Format("{0:0,0}", task.TaskPrice),
                                task.TypeCode,
                                UserName = userName
                            })
                        .OrderBy(o => o.TaskId)
                        .ToListAsync();

                foreach (var t in tasks)
                {
                    var jsonString = JsonSerializer.Serialize(t);
                    var jResult = JsonSerializer.Deserialize<TaskOutput>(jsonString);

                    result.Tasks.Add(jResult);
                }

                return result;
            }

            catch (ArgumentNullException ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new ArgumentNullException($"Не передан статус {ex.Message}");
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает задания в статусе "В аукционе".
        /// </summary>
        /// <returns>Список заданий в аукционе.</returns>
        public async Task<GetTaskResultOutput> LoadAuctionTasks()
        {
            try
            {
                var resultTasks = new GetTaskResultOutput();
                var auctionTasks = await (from tasks in _postgre.Tasks
                                                  join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                                                  join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                                                  join users in _postgre.Users on tasks.OwnerId equals users.Id
                                                  where statuses.StatusName.Equals(StatusTask.AUCTION)
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
                                                      TaskBegda = string.Format("{0:f}", tasks.TaskBegda),
                                                      TaskEndda = string.Format("{0:f}", tasks.TaskEndda),
                                                      tasks.TaskTitle,
                                                      tasks.TaskDetail,
                                                      tasks.TaskId,
                                                      TaskPrice = string.Format("{0:0,0}", tasks.TaskPrice),
                                                      tasks.TypeCode,
                                                      users.UserName
                                                  })
                          .OrderBy(o => o.TaskId)
                          .ToListAsync();

                // Приводит к типу коллекции GetTaskResultOutput.
                foreach (var task in auctionTasks)
                {
                    var jsonString = JsonSerializer.Serialize(task);
                    var result = JsonSerializer.Deserialize<TaskOutput>(jsonString);

                    if (result != null)
                    {
                        // Считает кол-во ставок к заданию, либо проставит 0.
                        var countResponds = await _postgre.Responds
                            .Where(r => r.TaskId == result.TaskId)
                            .Select(r => r.RespondId)
                            .CountAsync();

                        result.CountOffers = countResponds > 0 ? countResponds : 0;

                        // Если дата сдачи задания больше чем текущая дата, значит срок еще есть. В аукцион задание попадет.
                        if (Convert.ToDateTime(result.TaskEndda) > DateTime.Now)
                        {
                            resultTasks.Tasks.Add(result);
                        }

                        // Если дата сдачи задания меньше чем текущая дата, значит срок истек, и нужно добавить в таблицу просроченных заданий. В аукцион задание не попадет.
                        else if (Convert.ToDateTime(result.TaskEndda) < DateTime.Now)
                        {
                            // Ищет такое задание в таблице просроченных.
                            var overdueTask = await (from ov in _postgre.OverdueTasks
                                                     where ov.TaskId == result.TaskId
                                                     select ov)
                                .FirstOrDefaultAsync();

                            if (overdueTask != null)
                            {
                                continue;
                            }

                            await _postgre.OverdueTasks.AddAsync(new OverdueTaskEntity
                            {
                                TaskId = result.TaskId
                            });

                            await _postgre.SaveChangesAsync();
                        }
                    }
                }

                return resultTasks;
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выбирает Login юзера по его Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Login юзера.</returns>
        public async Task<string> GetUserLoginById(string userId)
        {
            return await _postgre.Users
                .Where(u => u.Id
                .Equals(userId))
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Метод выбирает Id юзера по его Login.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Login юзера.</returns>
        public async Task<string> GetUserByName(string userName)
        {
            return await _postgre.Users
                .Where(u => u.UserName
                .Equals(userName))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Метод получает список ставок к заданию.
        /// </summary>
        /// <param name="taskId">Id задания, для которого нужно получить список ставок.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Список ставок.</returns>
        public async Task<GetRespondResultOutput> GetRespondsAsync(long taskId, string account)
        {
            try
            {
                if (taskId == 0)
                {
                    throw new NullTaskIdException();
                }

                IEnumerable respondsList = await GetRespondsListAsync(taskId);

                GetRespondResultOutput result = new GetRespondResultOutput();

                // Находит Id исполнителя.
                string userId = await _userService.GetUserIdByLogin(account);

                // Приведет к типу коллекции GetRespondResultOutput.
                foreach (object respond in respondsList)
                {
                    string jsonString = JsonSerializer.Serialize(respond);
                    RespondOutput resultObject = JsonSerializer.Deserialize<RespondOutput>(jsonString);

                    // Если исполнитель оставлял ставку к данному заданию, то проставит флаг видимости кнопки "ИЗМЕНИТЬ СТАВКУ", иначе скроет ее.
                    if (resultObject.ExecutorId.Equals(userId))
                    {
                        resultObject.IsVisibleButton = true;
                    }

                    // Выберет приглашение.
                    InviteEntity invite = await _postgre.Invities
                        .Where(c => c.TaskId == taskId && c.ExecutorId.Equals(resultObject.ExecutorId))
                        .FirstOrDefaultAsync();

                    if (invite != null && !string.IsNullOrEmpty(invite.ExecutorId) && invite.TaskId > 0)
                    {

                        if (invite.IsAccept)
                        {
                            resultObject.IsSendInvite = true;
                        }

                        if (invite.IsCancel)
                        {
                            resultObject.IsCancelInvite = true;
                        }
                    }

                    result.Responds.Add(resultObject);
                }

                return result;
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выберет исполнителя задания.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="executorId">Id исполнителя, которого заказчик выбрал.</param>
        /// <returns>Флаг проверки оплаты.</returns>
        public async Task<bool> SelectAsync(long taskId, string executorId)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new NullTaskIdException();
                }

                if (string.IsNullOrEmpty(executorId))
                {
                    throw new EmptyExecutorIdException();
                }

                TaskEntity task = await _postgre.Tasks
                    .Where(t => t.TaskId == taskId)
                    .FirstOrDefaultAsync();

                // Если задание не было оплаченно заказчиком.
                if (!task.IsPay)
                {
                    return false;
                }

                // Запишет исполнителя на задание и будет ждать ответа от исполнителя на приглашение.
                await _postgre.Invities.AddAsync(new InviteEntity
                {
                    TaskId = task.TaskId,
                    ExecutorId = executorId,
                    IsAccept = true,
                    OwnerId = task.OwnerId
                });
                await _postgre.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод проверит оплату задания.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <returns>Флаг проверки.</returns>
        public async Task<bool> CheckSelectPayAsync(long taskId)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new NullTaskIdException();
                }

                // Выберет задание. Должно быть оплачено и исполнитель должен быть проставлен.
                var task = await _postgre.Tasks
                    .Where(c => c.TaskId == taskId)
                    .Select(res => new
                    {
                        IsPay = res.IsPay.Equals(true)
                    })
                    .FirstOrDefaultAsync();

                return task.IsPay;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод проверит, принял ли исполнитель в работу задание и не отказался ли от него.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Если все хорошо, то вернет список ставок к заданию, в котором будет только ставка исполнителя, которого выбрали и который принял в работу задание.</returns>
        public async Task<GetRespondResultOutput> CheckAcceptAndNotCancelInviteTaskAsync(long taskId, string account)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new NullTaskIdException();
                }

                // Выберет задание. Задание должно быть принято в работу.
                var task = await _postgre.Invities
                    .Where(c => c.TaskId == taskId && c.IsAccept.Equals(true))
                    .FirstOrDefaultAsync();

                if (task == null)
                {
                    return new GetRespondResultOutput();
                }

                // Получит список ставок к заданию.
                GetRespondResultOutput responds = await GetRespondsAsync(taskId, account);

                // Если ставок нет.
                if (!responds.Responds.Any())
                {
                    return new GetRespondResultOutput();
                }

                // Оставит только ставку исполнителя, который принял задание в работу.
                responds.Responds.RemoveAll(item => !item.ExecutorId.Equals(task.ExecutorId));

                return responds;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                var logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод запишет переход к просмотру или изменению задания исполнителем.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="type">Тип перехода.</param>
        /// <param name="account">Логин пользователя</param>
        /// <returns>Id задания.</returns>
        public async Task<TransitionOutput> SetTransitionAsync(int taskId, string type, string account)
        {
            try
            {
                var result = new TransitionOutput
                {
                    TaskId = taskId,
                    Type = type
                };

                if (string.IsNullOrEmpty(type))
                {
                    throw new EmptyTransitionTypeException();
                }

                if (taskId <= 0)
                {
                    throw new NullTaskIdException();
                }

                // Найдет Id пользователя.
                var userId = await _userService.GetUserIdByLogin(account);

                // Проверит существование перехода пользователя. Если он уже записан в таблицу (для избежания дублей).
                var transition = await (from t in _postgre.Transitions
                                        where t.Id.Equals(userId)
                                              && t.TransitionType.Equals(type)
                                        select t)
                    .FirstOrDefaultAsync();

                // Если переход уже делался ранее.
                if (transition != null && transition.Id.Equals(userId))
                {
                    // Перезапишет переод.
                    transition.Id = userId;
                    transition.TaskId = taskId;

                    //_postgre.Transitions.Update(transition);
                    await _postgre.SaveChangesAsync();

                    return result;
                }

                // Перехода еще не было. Запишет переход.
                await _postgre.Transitions.AddAsync(new TransitionEntity
                {
                    TaskId = taskId,
                    Id = userId,
                    TransitionType = type.Equals("View") ? "View" : "Edit"
                });

                await _postgre.SaveChangesAsync();

                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_db, e.GetType().FullName, e.Message.ToString(), e.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод получит переход.
        /// </summary>
        /// <param name="account">Логин пользователя</param>
        /// <returns>Id задания.</returns>
        public async Task<TransitionOutput> GetTransitionAsync(string account)
        {
            try
            {
                // Найдет Id пользователя.
                var userId = await _userService.GetUserIdByLogin(account);

                var transition = await (from t in _postgre.Transitions
                                        where t.Id.Equals(userId)
                                        select new TransitionEntity
                                        {
                                            TaskId = t.TaskId,
                                            TransitionType = t.TransitionType
                                        })
                    .FirstOrDefaultAsync();

                if (transition == null)
                {
                    throw new NotFoundTransitionException();
                }

                var jsonString = JsonSerializer.Serialize(transition);
                var result = JsonSerializer.Deserialize<TransitionOutput>(jsonString);

                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_db, e.GetType().FullName, e.Message.ToString(), e.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод получит список значений для селекта сортировки заданий.
        /// </summary>
        /// <returns>Список значений.</returns>
        public async Task<ControlSortResult> GetSortSelectAsync()
        {
            try
            {
                var result = new ControlSortResult();

                await (from res in _postgre.ControlSorts
                       select new ControlSortOutput
                       {
                           SortKey = res.SortKey,
                           SortValue = res.SortValue
                       })
                    .ForEachAsync(item => result.ControlSorts.Add(item));

                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_db, e.GetType().FullName, e.Message.ToString(), e.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод получит список значений для селекта фильтров заданий.
        /// </summary>
        /// <returns>Список значений.</returns>
        public async Task<ControlFilterResult> GetFilterSelectAsync()
        {
            try
            {
                var result = new ControlFilterResult();

                await (from res in _postgre.ControlFilters
                       select new ControlFilterOutput
                       {
                           FilterKey = res.FilterKey,
                           FilterValue = res.FilterValue
                       })
                    .ForEachAsync(item => { result.ControlFilters.Add(item); });

                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_db, e.GetType().FullName, e.Message.ToString(), e.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }

        /// <summary>
        /// Метод получит список ставок исполнителей к заданию.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <returns>Список заданий.</returns>
        private async Task<IEnumerable> GetRespondsListAsync(long taskId)
        {
            IEnumerable tasks = await (_postgre.Responds.Where(t => t.TaskId == taskId)
                .Join(_postgre.Users, re => re.ExecutorId, u => u.Id, (re, u) => new { re, u })
                .Join(_postgre.Statistics, user => user.u.Id, st => st.ExecutorId, (user, st) => new { user, st })
                .Select(res => new {
                    res.user.u.UserName,
                    res.user.re.Comment,
                    Price = string.Format("{0:0,0}", res.user.re.Price),
                    res.st.CountPositive,
                    res.st.CountNegative,
                    res.st.CountTotalCompletedTask,
                    res.st.Rating,
                    UserIcon = res.user.u.UserIcon ?? NoPhotoUrl.NO_PHOTO,
                    res.st.ExecutorId,
                    res.user.re.RespondId
                })
                .ToListAsync());

            return tasks;
        }
    }
}
