using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.User;
using Barbuuuda.Services;
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
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        public static string Module => "Barbuuuda.Executor";

        public ExecutorController(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden) : base(Module)
        {
            _db = db;
            _postgre = postgre;
            _iden = iden;
        }

        /// <summary>
        /// Метод выгружает список исполнителей сервиса.
        /// </summary>
        /// <returns>Список исполнителей.</returns>
        [HttpPost, Route("list")]
        public async Task<IActionResult> GetExecutorListAsync()
        {
            IExecutor _executor = new ExecutorService(_db, _postgre, _iden);
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
            IExecutor _executor = new ExecutorService(_db, _postgre, _iden);
            await _executor.AddExecutorSpecializations(specializations, GetUserName());

            return Ok();  
        }
    }
}
