using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Task;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
