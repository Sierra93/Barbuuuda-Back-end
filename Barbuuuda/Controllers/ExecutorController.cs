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
using Barbuuuda.Models.Respond.Input;
using Barbuuuda.Models.Respond.Output;
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
        private readonly IExecutorService _executorService;

        public ExecutorController(IExecutorService executorService)
        {
            _executorService = executorService;
        }

        /// <summary>
        /// Метод выгружает список исполнителей сервиса.
        /// </summary>
        /// <returns>Список исполнителей.</returns>
        [HttpPost, Route("list")]
        public async Task<IActionResult> GetExecutorListAsync()
        {            
            IEnumerable aExecutors = await _executorService.GetExecutorListAsync();

            return Ok(aExecutors);
        }

        /// <summary>
        /// Метод добавляет специализации исполнителя.
        /// </summary>
        /// <param name="specializations">Массив специализаций.</param>             
        [HttpPost, Route("add-spec")]
        public async Task<IActionResult> AddExecutorSpecializations([FromBody] ExecutorSpecialization[] specializations)
        {
            await _executorService.AddExecutorSpecializations(specializations, GetUserName());

            return Ok();  
        }

        /// <summary>
        /// Метод получает вопрос для теста исполнителя в зависимости от номера вопроса, переданного с фронта.
        /// </summary>
        /// <param name="nextQuestionInput">Входная модель.</param>
        /// <returns>Вопрос с вариантами ответов.</returns>
        [HttpPost, Route("answer")]
        public async Task<IActionResult> GetExecutorTestAsync([FromBody] NextQuestionInput nextQuestionInput)
        {
            var question = await _executorService.GetQuestionAsync(nextQuestionInput.NumberQuestion);

            return Ok(question);
        }

        /// <summary>
        /// Метод получает кол-во вопросов для теста исполнителя.
        /// </summary>
        /// <returns>Кол-во вопросов.</returns>
        [HttpGet, Route("answers-count")]
        public async Task<IActionResult> GetAnswersCountAsync()
        {
            int count = await _executorService.GetCountAsync();

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
            bool isCheck = await _executorService.CheckAnswersTestAsync(answers, GetUserName());

            return Ok(isCheck);
        }

        /// <summary>
        /// Метод выгружает задания, которые находятся в работе у исполнителя. Т.е у которых статус "В работе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        [HttpPost, Route("tasks-work")]
        public async Task<IActionResult> GetTasksWorkAsync()
        {
            IEnumerable tasks = await _executorService.GetTasksWorkAsync(GetUserName());

            return Ok(tasks);
        }

        /// <summary>
        /// Метод оставляет ставку к заданию.
        /// </summary>
        /// <param name="taskInput">Входная модель.</param>
        [HttpPost, Route("create-respond")]
        public async Task<IActionResult> RespondTaskAsync([FromBody] RespondInput taskInput)
        {
            bool isRespond = await _executorService.RespondAsync(taskInput.TaskId, taskInput.Price, taskInput.IsTemplate, taskInput, taskInput.Comment, GetUserName());

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
            bool isCheck = await _executorService.CheckRespondAsync(checkRespondInput.TaskId, GetUserName());

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
            GetResultTask result = await _executorService.InviteAsync(GetUserName());

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
        //    GetResultTask result = await _executorService.MyTasksAsync(GetUserName());

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
            bool result = await _executorService.AcceptTaskAsync(input.TaskId, GetUserName());

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
            bool result = await _executorService.CancelTaskAsync(input.TaskId, GetUserName());

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
            GetResultTask result = await _executorService.GetWorkTasksAsync(GetUserName());

            return Ok(result);
        }

        /// <summary>
        /// Метод изменит ставку к заданию.
        /// </summary>
        /// <param name="taskInput">Входная модель.</param>
        [HttpPatch, Route("change-respond")]
        public async Task<IActionResult> ChangeRespondTaskAsync([FromBody] ChangeRespondInput changeRespondInput)
        {
            bool isRespond = await _executorService.ChangeRespondAsync(changeRespondInput.TaskId, changeRespondInput.Price, changeRespondInput.Comment, changeRespondInput.RespondId, GetUserName());

            return Ok(isRespond);
        }

        /// <summary>
        /// Метод получит ставку исполнителя для ее изменения.
        /// </summary>
        /// <param name="changeRespondInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("get-change-respond")]
        [ProducesResponseType(200, Type = typeof(ChangeRespondOutput))]
        public async Task<IActionResult> GetChangedRespondAsync([FromBody] ChangeRespondInput changeRespondInput)
        {
            var editRespond = await _executorService.GetChangedRespondAsync(changeRespondInput.TaskId,
                changeRespondInput.RespondId, GetUserName());

            return Ok(editRespond);
        }
    }
}
