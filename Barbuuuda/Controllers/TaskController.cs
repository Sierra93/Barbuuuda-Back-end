using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers {
    /// <summary>
    /// Контроллер содержит логику работы с заданиями.
    /// </summary>
    [ApiController, Route("task")]
    public class TaskController : ControllerBase {
        ApplicationDbContext _db;
        PostgreDbContext _postgre;

        public TaskController(ApplicationDbContext db, PostgreDbContext postgre) {
            _db = db;
            _postgre = postgre;
        }

        
        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto oTask) {
            ITask _task = new TaskService(_db, _postgre);
            TaskDto oResultTask = await _task.CreateTask(oTask);

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <returns>Вернет данные измененного задания.</returns>
        [HttpPost, Route("edit")]
        public async Task<IActionResult> EditTask([FromBody] TaskDto oTask) {
            ITask _task = new TaskService(_db, _postgre);
            TaskDto oResultTask = await _task.EditTask(oTask);

            return Ok(oResultTask);
        }

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        [HttpPost, Route("get-categories")]
        public async Task<IActionResult> GetCategories() {
            ITask _task = new TaskService(_db, _postgre);
            IList aCategories = await _task.GetTaskCategories();

            return Ok(aCategories);
        }

        /// <summary>
        /// Метод выгружает список специализаций заданий.
        /// </summary>
        /// <returns>Коллекцию специализаций.</returns>
        [HttpPost, Route("get-specializations")]
        public async Task<IActionResult> GetSpecializations() {
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
        public async Task<IActionResult> GetTasksList([FromQuery] int userId, [FromQuery] int? taskId, [FromQuery] string type) {
            ITask _task = new TaskService(_db, _postgre);
            IList aCustomerTasks = await _task.GetTasksList(userId, taskId, type);

            return Ok(aCustomerTasks);
        }
    }
}
