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

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы заданий.
    /// </summary>
    public sealed class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IUserService _user;

        public TaskService(ApplicationDbContext db, PostgreDbContext postgre, IUserService user)
        {
            _db = db;
            _postgre = postgre;
            _user = user;
        }

        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <param name="userName">Login юзера.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        public async Task<TaskEntity> CreateTask(TaskEntity oTask, string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(oTask.TaskTitle) || string.IsNullOrEmpty(oTask.TaskDetail))
                {
                    throw new ArgumentException();
                }

                // Проверяет существование заказчика, который создает задание.
                bool bCustomer = await IdentityCustomer(userName);

                // Проверяет, есть ли такая категория в БД.
                bool bCategory = await IdentityCategory(oTask.CategoryCode);

                // Если все проверки прошли.
                if (bCustomer && bCategory)
                {
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
        public async Task<TaskEntity> EditTask(TaskEntity oTask, string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(oTask.TaskTitle) || string.IsNullOrEmpty(oTask.TaskDetail))
                {
                    throw new ArgumentException();
                }

                // Проверяет существование заказчика, который создал задание.
                bool bCustomer = await IdentityCustomer(userName);

                string ownerId = await _user.GetUserIdByLogin(userName);

                // Запишет Id заказчика.
                if (!string.IsNullOrEmpty(ownerId))
                {
                    oTask.OwnerId = ownerId;
                }

                // Проверяет, есть ли такая категория в БД.
                bool bCategory = await IdentityCategory(oTask.CategoryCode);

                // Если все проверки прошли.
                if (bCustomer && bCategory)
                {
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
        async Task<bool> IdentityCustomer(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException();
                }

                UserEntity oUser = await _postgre.Users
                    .Where(u => u.UserName.Equals(userName))
                    .FirstOrDefaultAsync();

                return oUser != null ? true : throw new ArgumentNullException();
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
        public async Task<IList> GetTasksList(string userName, int? taskId, string type)
        {
            try
            {
                IList aResultTaskObj = null;

                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException();
                }               

                if (!type.Equals(TaskType.ALL) && !type.Equals(TaskType.SINGLE))
                {
                    throw new NotParameterException(type);
                }

                // Вернет либо все задания либо одно.
                return aResultTaskObj = type.Equals(TaskType.ALL)
                    ? aResultTaskObj = await GetAllTasks(userName)
                    : aResultTaskObj = await GetSingleTask(userName, taskId);
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
        async Task<IList> GetSingleTask(string userName, int? taskId)
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
            CustomerOutput customer = await _user.GetCustomerLoginByTaskId(taskId);

            // TODO: отрефачить этот метод, чтоб не обращаться два раза к БД за получением задания.            
            var oTask = await (from tasks in _postgre.Tasks
                               join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                               join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                               where tasks.TaskId.Equals(taskId)
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
                                   taskEndda = string.Format("{0:f}", tasks.TaskEndda),
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
        public async Task DeleteTask(int taskId)
        {
            try
            {
                if (taskId == 0)
                {
                    throw new ArgumentNullException();
                }

                TaskEntity oRemovedTask = await _postgre.Tasks.Where(t => t.TaskId == taskId).FirstOrDefaultAsync();
                _postgre.Tasks.Remove(oRemovedTask);
                await _postgre.SaveChangesAsync();
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

                return new
                {
                    auction = countAuction,
                    work = countWork,
                    garant = countGarant,
                    complete = countComplete,
                    perechet = countPerechet,
                    draft = countDraft
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
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Список заданий с определенным статусом.</returns>
        public async Task<IList> GetStatusTasks(string status, string userName)
        {
            try
            {
                string userId = await GetUserByName(userName);

                return string.IsNullOrEmpty(status) ? throw new ArgumentNullException() :
                     await (from tasks in _postgre.Tasks
                            join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                            join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                            where statuses.StatusName.Equals(status)
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
        /// Метод получает кол-во заданий всего.
        /// </summary>
        /// <param name="userName">Login пользователя.</param>
        /// <returns></returns>
        public async Task<int?> GetTotalCountTasks(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return null;
                }

                UserEntity user = await _user.GetUserByLogin(userName);

                if (user.Id == null)
                {
                    throw new NotFoundUserException(userName);
                }

                return await _postgre.Tasks
                    .Where(t => t.OwnerId
                    .Equals(user.Id))
                    .CountAsync();
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
                GetTaskResultOutput resultTasks = new GetTaskResultOutput();
                IEnumerable auctionTasks = await (from tasks in _postgre.Tasks
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
                foreach (object task in auctionTasks)
                {
                    string jsonString = JsonSerializer.Serialize(task);
                    TaskOutput result = JsonSerializer.Deserialize<TaskOutput>(jsonString);

                    // Считает кол-во ставок к заданию, либо проставит 0.
                    int countResponds = await _postgre.Responds
                        .Where(r => r.TaskId == result.TaskId)
                        .Select(r => r.RespondId)
                        .CountAsync();
                    result.CountOffers = countResponds > 0 ? countResponds : 0;

                    resultTasks.Tasks.Add(result);
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
        public async Task<GetRespondResultOutput> GetRespondsAsync(int taskId, string account)
        {
            try
            {
                if (taskId == 0)
                {
                    throw new NullTaskIdException();
                }

                IEnumerable respondsList = await (_postgre.Responds.Where(t => t.TaskId == taskId)
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
                        res.st.ExecutorId
                    })
                    .ToListAsync());

                GetRespondResultOutput result = new GetRespondResultOutput();

                // Находит Id исполнителя.
                string userId = await _user.GetUserIdByLogin(account);

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

                // Если задание было оплаченно заказчиком.
                if (task.IsPay)
                {
                    // Запишет исполнителя на задание.
                    task.ExecutorId = executorId;
                    await _postgre.SaveChangesAsync();

                    return true;
                }

                return false;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await logger.LogCritical();
                throw;
            }
        }
    }
}
