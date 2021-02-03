using Barbuuuda.Core.Data;
using Barbuuuda.Models.Outpoot;
using Barbuuuda.Models.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер работы с пагинацией.
    /// </summary>
    [ApiController, Route("pagination")]
    public class PaginationController : ControllerBase
    {
        private readonly PostgreDbContext _postgre;

        public PaginationController(PostgreDbContext postgre)
        {
            _postgre = postgre;
        }

        /// <summary>
        /// Метод пагинации.
        /// </summary>
        /// <param name="pageIdx"></param>
        /// <returns></returns>
        [HttpGet, Route("page")]
        public async Task<IActionResult> Index([FromQuery] int pageIdx = 1)
        {            
            int countTasksPage = 5;   // Кол-во заданий на странице.
            IQueryable<TaskEntity> tasks = _postgre.Tasks;
            var count = await tasks.CountAsync();
            var items = await tasks.Skip((pageIdx - 1) * countTasksPage).Take(countTasksPage).ToListAsync();

            ModelPaginationOutpoot pageData = new ModelPaginationOutpoot(count, pageIdx, countTasksPage);
            ModelIndexOutpoot data = new ModelIndexOutpoot
            {
                PageData = pageData,
                Tasks = items
            };

            return Ok(data);
        }
    }
}
