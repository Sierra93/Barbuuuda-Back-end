using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Outpoot;
using Barbuuuda.Models.User;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Barbuuuda.Services
{
    public sealed class PaginationService : IPagination
    {
        private readonly PostgreDbContext _postgre;
        private readonly IUser _user;

        public PaginationService(PostgreDbContext postgre, IUser user)
        {
            _postgre = postgre;           
            _user = user;
        }

        /// <summary>
        /// Метод пагинации.
        /// </summary>
        /// <param name="pageIdx">Номер страницы.</param>
        /// <param name="userName">Имя юзера.</param>
        /// <returns>Данные пагинации.</returns>
        public async Task<IndexOutpoot> GetPaginationTasks(int pageIdx, string userName)
        {
            try
            {
                int countTasksPage = 5;   // Кол-во заданий на странице.
                UserEntity user = await _user.GetUserByLogin(userName);

                var aTasks = (from tasks in _postgre.Tasks
                              join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                              join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                              where tasks.OwnerId.Equals(user.Id)
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
                                  userName
                              })
                              .OrderBy(o => o.TaskId)
                              .AsQueryable();
                int count = await aTasks.CountAsync();
                var items = await aTasks.Skip((pageIdx - 1) * countTasksPage).Take(countTasksPage).ToListAsync();

                PaginationOutpoot pageData = new PaginationOutpoot(count, pageIdx, countTasksPage);
                IndexOutpoot paginationData = new IndexOutpoot
                {
                    PageData = pageData,
                    Tasks = items
                };

                return paginationData;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
