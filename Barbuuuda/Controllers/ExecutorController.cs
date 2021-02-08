using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит методы по работе с исполнителями сервиса.
    /// </summary>
    [ApiController, Route("executor")]
    public class ExecutorController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;

        public ExecutorController(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden)
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
    }
}
