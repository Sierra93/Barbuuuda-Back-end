using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит логику работы с заданиями.
    /// </summary>
    [ApiController, Route("task")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserDto> _userManager;
        private readonly SignInManager<UserDto> _signInManager;

        public TaskController(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserDto> userManager, SignInManager<UserDto> signInManager)
        {
            _db = db;
            _postgre = postgre;
            _userManager = userManager;
            _signInManager = signInManager;
            _iden = iden;
        }


        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="oTask">Объект с данными задания.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto oTask)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            TaskDto oResultTask = await _task.CreateTask(oTask);

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="oTask">Объект с данными задания.</param>
        /// <returns>Вернет данные измененного задания.</returns>
        [HttpPost, Route("edit")]
        public async Task<IActionResult> EditTask([FromBody] TaskDto oTask)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            TaskDto oResultTask = await _task.EditTask(oTask);

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        [HttpPost, Route("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aCategories = await _task.GetTaskCategories();

            return Ok(aCategories);
        }

        /// <summary>
        /// Метод выгружает список специализаций заданий.
        /// </summary>
        /// <returns>Коллекцию специализаций.</returns>
        [HttpPost, Route("get-specializations")]
        public Task<IActionResult> GetSpecializations()
        {
            //ITask _task = new TaskService(_db, _postgre);
            //IList aSpecializations = await _task.GetTaskSpecializations();

            //return Ok(aSpecializations);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод получает список заданий заказчика или конкретное задание.
        /// </summary>
        /// <param name="userId">Id заказчика.</param>
        /// <param name="taskId">Id задания.</param>
        /// <param name="type">Параметр получения заданий либо все либо одно.</param>
        /// <returns>Коллекция заданий.</returns>
        [HttpPost, Route("tasks-list")]
        public async Task<IActionResult> GetTasksList([FromQuery] string userId, [FromQuery] int? taskId, [FromQuery] string type)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aCustomerTasks = await _task.GetTasksList(userId, taskId, type);

            return Ok(aCustomerTasks);
        }

        /// <summary>
        /// Метод удаляет задание.
        /// </summary>
        /// <param name="taskId">Id задачи.</param>
        [HttpGet, Route("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            await _task.DeleteTask(taskId);

            return Ok();
        }

        /// <summary>
        /// Метод фильтрует задания заказчика по параметру.
        /// </summary>
        /// <param name="query">Параметр фильтрации.</param>
        /// <returns>Отфильтрованные данные.</returns>
        [HttpGet, Route("filter")]
        public async Task<IActionResult> FilterTask([FromQuery] string query)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aTasks = await _task.FilterTask(query);

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод ищет задание по Id или названию.
        /// </summary>
        /// <param name="param">Поисковый параметр.</param>
        /// <returns>Результат поиска.</returns>
        [HttpGet, Route("search")]
        public async Task<IActionResult> SearchTask([FromQuery] string param)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aTasks = await _task.SearchTask(param);

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод ищет задания указанной даты.
        /// </summary>
        /// <param name="date">Параметр даты.</param>
        /// <returns>Найденные задания.</returns>
        [HttpGet, Route("concretely-date")]
        public async Task<IActionResult> GetSearchTaskDate([FromQuery] string date)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aTasks = await _task.GetSearchTaskDate(date);

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод выгружает активные задания заказчика.
        /// </summary>
        /// <returns>Список активных заданий.</returns>
        [HttpGet, Route("active")]
        public async Task<IActionResult> LoadActiveTasks([FromQuery] string userId)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aTasks = await _task.LoadActiveTasks(userId);

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод получает кол-во задач определенного статуса.
        /// </summary>
        /// <returns>Число кол-ва задач.</returns>
        [HttpPost, Route("count-status")]
        public async Task<IActionResult> GetCountTaskStatuses()
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            object countTask = await _task.GetCountTaskStatuses();

            return Ok(countTask);
        }

        /// <summary>
        /// Метод получает задания определенного статуса.
        /// </summary>
        /// <param name="status">Название статуса.</param>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Список заданий с определенным статусом.</returns>
        [HttpGet, Route("task-status")]
        public async Task<IActionResult> GetStatusTasks([FromQuery] string status, string userId)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aTasks = await _task.GetStatusTasks(status, userId);

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод получает кол-во заданий всего.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns></returns>
        [HttpGet, Route("total")]
        public async Task<IActionResult> GetTotalCountTasks([FromQuery] string userId)
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            int countTasks = await _task.GetTotalCountTasks(userId);

            return Ok(countTasks);
        }

        /// <summary>
        /// Метод получает список заданий в аукционе. Выводит задания в статусе "В аукционе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        [HttpPost, Route("auction")]
        public async Task<IActionResult> LoadAuctionTasks()
        {
            ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aAuctionTasks = await _task.LoadAuctionTasks();

            return Ok(aAuctionTasks);
        }
    }
}
