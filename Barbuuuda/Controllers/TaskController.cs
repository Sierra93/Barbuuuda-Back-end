﻿using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Barbuuuda.Models.Task.Input;
using Barbuuuda.Models.Respond.Output;
using Barbuuuda.Models.Task.Output;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит логику работы с заданиями.
    /// </summary>
    [ApiController, Route("task")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController : BaseController
    {
        /// <summary>
        /// Сервис заданий.
        /// </summary>
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="oTask">Объект с данными задания.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskEntity task)
        {
            var oResultTask = await _taskService.CreateTask(task, GetUserName());

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод изменяет новое задание.
        /// </summary>
        /// <param name="oTask">Объект с данными задания.</param>
        /// <returns>Вернет данные измененного задания.</returns>
        [HttpPost, Route("edit")]
        public async Task<IActionResult> EditTaskAsync([FromBody] TaskInput task)
        {
            var oResultTask = await _taskService.EditTask(task, GetUserName());

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        [HttpPost, Route("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            IList aCategories = await _taskService.GetTaskCategories();

            return Ok(aCategories);
        }

        /// <summary>
        /// Метод выгружает список специализаций заданий.
        /// </summary>
        /// <returns>Коллекцию специализаций.</returns>
        //[HttpPost, Route("get-specializations")]
        //public Task<IActionResult> GetSpecializations()
        //{
        //    //ITask _taskService = new TaskService(_db, _postgre);
        //    //IList aSpecializations = await _taskService.GetTaskSpecializations();

        //    //return Ok(aSpecializations);
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Метод получает список заданий заказчика или конкретное задание.
        /// </summary>
        /// <param name="taskInput">Входная модель.</param>
        /// <returns>Коллекция заданий.</returns>
        [HttpPost, Route("tasks-list")]
        public async Task<IActionResult> GetTasksList([FromBody] TaskInput taskInput)
        {
            var aCustomerTasks = await _taskService.GetTasksList(GetUserName(), taskInput.TaskId, taskInput.Type);

            return Ok(aCustomerTasks);
        }

        /// <summary>
        /// Метод удаляет задание.
        /// </summary>
        /// <param name="taskId">Id задачи.</param>
        /// <returns>Статус удаления.</returns>
        [HttpPut, Route("delete/{taskId}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> DeleteTask([FromRoute] long taskId)
        {
            var result = await _taskService.DeleteTask(taskId);

            return Ok(result);
        }

        /// <summary>
        /// Метод фильтрует задания заказчика по параметру.
        /// </summary>
        /// <param name="query">Параметр фильтрации.</param>
        /// <returns>Отфильтрованные данные.</returns>
        [HttpGet, Route("filter")]
        public async Task<IActionResult> FilterTask([FromQuery] string query)
        {
            IList aTasks = await _taskService.FilterTask(query);

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
            IList aTasks = await _taskService.SearchTask(param);

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
            IList aTasks = await _taskService.GetSearchTaskDate(date);

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод выгружает активные задания заказчика.
        /// </summary>
        /// <returns>Список активных заданий.</returns>
        [HttpGet, Route("active")]
        public async Task<IActionResult> LoadActiveTasks()
        {
            IList aTasks = await _taskService.LoadActiveTasks(GetUserName());

            return Ok(aTasks);
        }

        /// <summary>
        /// Метод получает кол-во задач определенного статуса.
        /// </summary>
        /// <returns>Число кол-ва задач.</returns>
        [HttpPost, Route("count-status")]
        public async Task<IActionResult> GetCountTaskStatuses()
        {
            var countTask = await _taskService.GetCountTaskStatuses();

            return Ok(countTask);
        }

        /// <summary>
        /// Метод получает задания определенного статуса.
        /// </summary>
        /// <param name="taskInput">Входная модель.</param>
        /// <returns>Список заданий с определенным статусом.</returns>
        [HttpPost, Route("task-status")]
        [ProducesResponseType(200, Type = typeof(GetTaskResultOutput))]
        public async Task<IActionResult> GetStatusTasks([FromBody] TaskInput taskInput)
        {
            var tasks = await _taskService.GetStatusTasks(taskInput.Status, GetUserName());

            return Ok(tasks);
        }

        /// <summary>
        /// Метод получает список заданий в аукционе. Выводит задания в статусе "В аукционе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        [HttpPost, Route("auction")]
        [ProducesResponseType(200, Type = typeof(GetTaskResultOutput))]
        public async Task<IActionResult> LoadAuctionTasks()
        {
            GetTaskResultOutput auctionTasks = await _taskService.LoadAuctionTasks();

            return Ok(auctionTasks); 
        }

        /// <summary>
        /// Метод получает список ставок к заданию.
        /// </summary>
        /// <param name="getRespondInput">Id задания, для которого нужно получить список ставок.</param>
        /// <returns>Список ставок.</returns>
        [HttpPost, Route("get-responds")]
        [ProducesResponseType(200, Type = typeof(GetRespondResultOutput))]
        public async Task<IActionResult> GetRespondsAsync([FromBody] GetRespondInput getRespondInput)
        {
            GetRespondResultOutput respondsList = await _taskService.GetRespondsAsync(getRespondInput.TaskId, GetUserName());

            return Ok(respondsList);
        }  

        /// <summary> 
        /// Метод выберет исполнителя задания.
        /// </summary>
        /// <param name="payInput">Входная модель.</param> 
        /// <returns>Флаг выбора.</returns>
        [HttpPost, Route("select")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> SelectAsync([FromBody] CheckPayInput payInput)
        {
            bool result = await _taskService.SelectAsync(payInput.TaskId, payInput.ExecutorId);

            return Ok(result);
        }

        /// <summary>
        /// Метод проверит оплату заданияь.
        /// </summary>
        /// <param name="payInput">Входная модель.</param>
        /// <returns>Флаг проверки.</returns>
        [HttpPost, Route("check-select-pay")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> CheckSelectPayAsync([FromBody] CheckPayInput payInput)
        {
            bool result = await _taskService.CheckSelectPayAsync(payInput.TaskId);

            return Ok(result);
        }

        /// <summary>
        /// Метод проверит, принял ли исполнитель в работу задание и не отказался ли от него.
        /// </summary>
        /// <param name="payInput">Входная модель.</param>
        /// <returns>Если все хорошо, то вернет список ставок к заданию, в котором будет только ставка исполнителя, которого выбрали и который принял в работу задание.</returns>
        [HttpPost, Route("check-accept-invite")]
        [ProducesResponseType(200, Type = typeof(GetRespondResultOutput))]
        public async Task<IActionResult> CheckAcceptAndNotCancelInviteTaskAsync([FromBody] CheckPayInput payInput)
        {
            GetRespondResultOutput result = await _taskService.CheckAcceptAndNotCancelInviteTaskAsync(payInput.TaskId, GetUserName());

            return Ok(result);
        }

        /// <summary>
        /// Метод запишет переход к просмотру или изменению задания исполнителем.
        /// </summary>
        /// <param name="transitionInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("set-transition")]
        [ProducesResponseType(200, Type = typeof(TransitionOutput))]
        public async Task<IActionResult> SetTransitionAsync([FromBody] TransitionInput transitionInput)
        {
            var transition = await _taskService.SetTransitionAsync(transitionInput.TaskId, transitionInput.Type, GetUserName());

            return Ok(transition);
        }

        /// <summary>
        /// Метод получит переход.
        /// </summary>
        /// <param name="transitionInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("get-transition")]
        [ProducesResponseType(200, Type = typeof(TransitionOutput))]
        public async Task<IActionResult> SetTransitionAsync()
        {
            var transition = await _taskService.GetTransitionAsync(GetUserName());

            return Ok(transition);
        }

        /// <summary>
        /// Метод получит список значений для селекта сортировки заданий.
        /// </summary>
        /// <returns>Список значений.</returns>
        [HttpPost, Route("get-sort-select")]
        [ProducesResponseType(200, Type = typeof(ControlSortResult))]
        public async Task<IActionResult> GetSortSelectAsync()
        {
            var result = await _taskService.GetSortSelectAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получит список значений для селекта фильтров заданий.
        /// </summary>
        /// <returns>Список значений.</returns>
        [HttpPost, Route("get-filter-select")]
        [ProducesResponseType(200, Type = typeof(ControlFilterResult))]
        public async Task<IActionResult> GetFilterSelectAsync()
        {
            var result = await _taskService.GetFilterSelectAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получит список статусов для селекта статусов.
        /// </summary>
        /// <returns>Список статусов.</returns>
        [HttpPost, Route("get-filter-statuses-select")]
        [ProducesResponseType(200, Type = typeof(TaskStatusOutput))]
        public async Task<IActionResult> GetStatusesSelectAsync()
        {
            var result = await _taskService.GetStatusesSelectAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получит список типов заданий для селекта фильтрации.
        /// </summary>
        /// <returns>Список типов заданий.</returns>
        [HttpPost, Route("get-filter-types-select")]
        [ProducesResponseType(200, Type = typeof(TaskTypeOutput))]
        public async Task<IActionResult> GetTypesSelectAsync()
        {
            var result = await _taskService.GetTypesSelectAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод выдаст тип контрола.
        /// </summary>
        /// <param name="controlVisibleInput">Входная модель.</param>
        /// <returns>Тип контрола.</returns>
        [HttpPost, Route("visible-control")]
        [ProducesResponseType(200, Type = typeof(ControlVisibleOutput))]
        public async Task<IActionResult> VisibleControlByCodeAsync([FromBody] ControlVisibleInput controlVisibleInput)
        {
            var type = await _taskService.VisibleControlAsync(controlVisibleInput.SelectedValue);

            return Ok(type);
        }
    }
}
