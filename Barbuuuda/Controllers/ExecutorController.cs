using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Executor.Input;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Barbuuuda.Models.Task.Output;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит методы по работе с исполнителями сервиса.
    /// </summary>
    [ApiController, Route("executor")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExecutorController : BaseController
    {
        /// <summary>
        /// Сервис исполнителя.
        /// </summary>
        private readonly IExecutorService _executor;

        public ExecutorController(IExecutorService executor)
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
        public async Task<IActionResult> RespondTaskAsync([FromBody] RespondInput taskInput)
        {
            bool isRespond = await _executor.RespondAsync(taskInput.TaskId, taskInput.Price, taskInput.IsTemplate, taskInput, taskInput.Comment, GetUserName());

            return Ok(isRespond);
        }

        /// <summary>
        /// Метод проверит, была ли сделана ставка к заданию текущим исполнителем.
        /// </summary>
        /// <param name="checkRespondInput">Входная модель.</param>
        /// <returns>Статус проверки true/false.</returns>
        [HttpPost, Route("check-respond")]
        public async Task<IActionResult> CheckRespondAsync([FromBody] CheckRespondInput checkRespondInput)
        {
            bool isCheck = await _executor.CheckRespondAsync(checkRespondInput.TaskId, GetUserName());

            return Ok(isCheck);
        }

        /// <summary>
        /// Метод выгрузит список заданий, в которых был выбран исполнитель.
        /// </summary>
        /// <returns>Список приглашений с данными заданий.</returns>
        [HttpPost, Route("invite")]
        [ProducesResponseType(200, Type = typeof(GetResultTask))]
        public async Task<IActionResult> InviteAsync()
        {
            GetResultTask result = await _executor.InviteAsync(GetUserName());

            return Ok(result);
        }

        /// <summary>
        /// Метод выгрузит список заданий, в которых был выбран текущий исполнитель.
        /// </summary>
        /// <returns>Список заданий.</returns>
        //[HttpPost, Route("my")]
        //[ProducesResponseType(200, Type = typeof(GetResultTask))]
        //public async Task<IActionResult> MyWorkTasksAsync()
        //{
        //    GetResultTask result = await _executor.MyTasksAsync(GetUserName());

        //    return Ok(result);
        //}

        /// <summary>
        /// Метод проставит согласие на выполнение задания.
        /// </summary>
        /// <returns>Флаг результата.</returns>
        [HttpPost, Route("accept")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> AcceptTaskAsync([FromBody] AcceptOrCancelWorkTaskInput input)
        {
            bool result = await _executor.AcceptTaskAsync(input.TaskId, GetUserName());

            return Ok(result);
        }

        /// <summary>
        /// Метод проставит отказ на выполнение задания.
        /// </summary>
        /// <returns>Флаг результата.</returns>
        [HttpPost, Route("cancel")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> CancelTaskAsync([FromBody] AcceptOrCancelWorkTaskInput input)
        {
            bool result = await _executor.CancelTaskAsync(input.TaskId, GetUserName());

            return Ok(result);
        }

        /// <summary>
        /// Метод получит список заданий для вкладки "Мои задания". Т.е задания, работа над которыми начата текущим исполнителем.
        /// </summary>
        /// <returns>Список заданий.</returns>
        [HttpPost, Route("work")]
        [ProducesResponseType(200, Type = typeof(GetResultTask))]
        public async Task<IActionResult> GetWorkTasksAsync()
        {
            GetResultTask result = await _executor.GetWorkTasksAsync(GetUserName());

            return Ok(result);
        }
    }
}
