using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Barbuuuda.Models.Task.Input;
using Barbuuuda.Models.Respond.Outpoot;
using Microsoft.AspNetCore.Http;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит логику работы с заданиями.
    /// </summary>
    [ApiController, Route("task")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController : BaseController
    {
        public static string Module => "Barbuuuda.Task";

        /// <summary>
        /// Сервис заданий.
        /// </summary>
        private readonly ITask _task;

        public TaskController(ITask task) : base(Module)
        {
            _task = task;
        }


        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="oTask">Объект с данными задания.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskEntity oTask)
        {
            TaskEntity oResultTask = await _task.CreateTask(oTask, GetUserName());

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод изменяет новое задание.
        /// </summary>
        /// <param name="oTask">Объект с данными задания.</param>
        /// <returns>Вернет данные измененного задания.</returns>
        [HttpPost, Route("edit")]
        public async Task<IActionResult> EditTask([FromBody] TaskEntity oTask)
        {
            TaskEntity oResultTask = await _task.EditTask(oTask, GetUserName());

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        [HttpPost, Route("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            IList aCategories = await _task.GetTaskCategories();

            return Ok(aCategories);
        }

        /// <summary>
        /// TODO убрать.
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
        /// <param name="taskId">Id задания.</param>
        /// <param name="type">Параметр получения заданий либо все либо одно.</param>
        /// <returns>Коллекция заданий.</returns>
        [HttpPost, Route("tasks-list")]
        public async Task<IActionResult> GetTasksList([FromQuery] int? taskId, [FromQuery] string type)
        {
            IList aCustomerTasks = await _task.GetTasksList(GetUserName(), taskId, type);

            return Ok(aCustomerTasks);
        }

        /// <summary>
        /// Метод удаляет задание.
        /// </summary>
        /// <param name="taskId">Id задачи.</param>
        [HttpGet, Route("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
        {
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
            IList aTasks = await _task.GetSearchTaskDate(date);

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод выгружает активные задания заказчика.
        /// </summary>
        /// <returns>Список активных заданий.</returns>
        [HttpGet, Route("active")]
        public async Task<IActionResult> LoadActiveTasks()
        {
            IList aTasks = await _task.LoadActiveTasks(GetUserName());

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод получает кол-во задач определенного статуса.
        /// </summary>
        /// <returns>Число кол-ва задач.</returns>
        [HttpPost, Route("count-status")]
        public async Task<IActionResult> GetCountTaskStatuses()
        {
            object countTask = await _task.GetCountTaskStatuses();

            return Ok(countTask);
        }

        /// <summary>
        /// Метод получает задания определенного статуса.
        /// </summary>
        /// <param name="status">Название статуса.</param>
        /// <returns>Список заданий с определенным статусом.</returns>
        [HttpGet, Route("task-status")]
        public async Task<IActionResult> GetStatusTasks([FromQuery] string status)
        {
            IList aTasks = await _task.GetStatusTasks(status, GetUserName());

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод получает кол-во заданий всего.
        /// </summary>
        /// <returns>Кол-во заданий.</returns>
        [HttpGet, Route("total")]
        public async Task<IActionResult> GetTotalCountTasks()
        {
            int? countTasks = await _task.GetTotalCountTasks(GetUserName());

            return Ok(countTasks);
        }

        /// <summary>
        /// Метод получает список заданий в аукционе. Выводит задания в статусе "В аукционе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        [HttpPost, Route("auction")]
        public async Task<IActionResult> LoadAuctionTasks()
        {
            object aAuctionTasks = await _task.LoadAuctionTasks();

            return Ok(aAuctionTasks);
        }

        /// <summary>
        /// Метод получает список ставок к заданию.
        /// </summary>
        /// <param name="getRespondInput">Id задания, для которого нужно получить список ставок.</param>
        /// <returns>Список ставок.</returns>
        [HttpPost("get-responds")]
        [ProducesResponseType(200, Type = typeof(GetRespondResultOutpoot))]
        public async Task<IActionResult> GetRespondsAsync([FromBody] GetRespondInput getRespondInput)
        {
            GetRespondResultOutpoot respondsList = await _task.GetRespondsAsync(getRespondInput.TaskId);

            return Ok(respondsList);
        }
    }
}
