using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Executor.Input;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит методы по работе с исполнителями сервиса.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController, Route("executor")]
    public class ExecutorController : BaseController
    {
        public static string Module => "Barbuuuda.Executor";

        /// <summary>
        /// Сервис исполнителя.
        /// </summary>
        private readonly IExecutor _executor;

        public ExecutorController(IExecutor executor) : base(Module)
        {
            _executor = executor;
        }

        /// <summary>
        /// Метод выгружает список исполнителей сервиса.
        /// </summary>
        /// <returns>Список исполнителей.</returns>
        [HttpPost, Route("list")]
        public async Task<IActionResult> GetExecutorListAsync()
        {            
            IEnumerable aExecutors = await _executor.GetExecutorListAsync();

            return Ok(aExecutors);
        }

        /// <summary>
        /// Метод добавляет специализации исполнителя.
        /// </summary>
        /// <param name="specializations">Массив специализаций.</param>             
        [HttpPost, Route("add-spec")]
        public async Task<IActionResult> AddExecutorSpecializations([FromBody] ExecutorSpecialization[] specializations)
        {
            await _executor.AddExecutorSpecializations(specializations, GetUserName());

            return Ok();  
        }

        /// <summary>
        /// Метод получает вопрос для теста исполнителя в зависимости от номера вопроса, переданного с фронта.
        /// </summary>
        /// <param name="numberQuestion">Номер вопроса.</param>
        /// <returns>Вопрос с вариантами ответов.</returns>
        [HttpGet, Route("answer")]
        public async Task<IActionResult> GetExecutorTestAsync([FromQuery] int numberQuestion)
        {
            var oQuestion = await _executor.GetQuestionAsync(numberQuestion);

            return Ok(oQuestion);
        }

        /// <summary>
        /// Метод получает кол-во вопросов для теста исполнителя.
        /// </summary>
        /// <returns>Кол-во вопросов.</returns>
        [HttpGet, Route("answers-count")]
        public async Task<IActionResult> GetAnswersCountAsync()
        {
            int count = await _executor.GetCountAsync();

            return Ok(count);
        }

        /// <summary>
        /// Метод проверяет результаты ответов на тест исполнителем.
        /// </summary>
        /// <param name="answers">Массив с ответами на тест.</param>
        /// <returns>Статус прохождения теста true/false.</returns>
        [HttpPost, Route("check")]
        public async Task<IActionResult> CheckAnswersTestAsync([FromBody] List<AnswerVariant> answers)
        {
            bool isCheck = await _executor.CheckAnswersTestAsync(answers, GetUserName());

            return Ok(isCheck);
        }

        /// <summary>
        /// Метод выгружает задания, которые находятся в работе у исполнителя. Т.е у которых статус "В работе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        [HttpPost, Route("tasks-work")]
        public async Task<IActionResult> GetTasksWorkAsync()
        {
            IEnumerable tasks = await _executor.GetTasksWorkAsync(GetUserName());

            return Ok(tasks);
        }

        /// <summary>
        /// Метод оставляет ставку к заданию.
        /// </summary>
        /// <param name="taskInput">Входная модель.</param>
        [HttpPost, Route("respond")]
        public async Task<IActionResult> RespondAsync([FromBody] TaskInput taskInput)
        {
            await _executor.RespondAsync(taskInput.TaskId, GetUserName());

            return Ok();
        }
    }
}
