using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
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
    }
}
