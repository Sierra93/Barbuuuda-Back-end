using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Outpoot;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер работы с пагинацией.
    /// </summary>
    [ApiController, Route("pagination")]
    public class PaginationController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ITask _task;
        public static string Module => "Barbuuuda.Pagination";

        public PaginationController(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ITask task) : base(Module)
        {
            _db = db;
            _postgre = postgre;
            _userManager = userManager;
            _signInManager = signInManager;
            _iden = iden;
            _task = task;
        }

        /// <summary>
        /// Метод пагинации.
        /// </summary>
        /// <param name="pageIdx">Страница по умолчанию.</param>
        /// <returns>Список заданий.</returns>
        [HttpGet, Route("page")]
        public async Task<IActionResult> GetPaginationTasks(int pageIdx = 1)
        {            
            int countTasksPage = 5;   // Кол-во заданий на странице.
            string userId = await _task.GetUserByName(GetUserName());

            var aTasks = (from tasks in _postgre.Tasks
                          join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                          join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                          where tasks.OwnerId.Equals(userId)
                          select new
                          {
                              tasks.CategoryCode,
                              tasks.CountOffers,
                              tasks.CountViews,
                              tasks.OwnerId,
                              tasks.SpecCode,
                              categories.CategoryName,
                              tasks.StatusCode,
                              statuses.StatusName,
                              taskBegda = string.Format("{0:f}", tasks.TaskBegda),
                              taskEndda = string.Format("{0:f}", tasks.TaskEndda),
                              tasks.TaskTitle,
                              tasks.TaskDetail,
                              tasks.TaskId,
                              taskPrice = string.Format("{0:0,0}", tasks.TaskPrice),
                              tasks.TypeCode,
                              userName = GetUserName()
                          })
                          .OrderBy(o => o.TaskId)
                          .AsQueryable();
            var count = await aTasks.CountAsync();
            var items = await aTasks.Skip((pageIdx - 1) * countTasksPage).Take(countTasksPage).ToListAsync();

            PaginationOutpoot pageData = new PaginationOutpoot(count, pageIdx, countTasksPage);
            IndexOutpoot data = new IndexOutpoot
            {
                PageData = pageData,
                Tasks = items
            };

            return Ok(data);
        }
    }
}
