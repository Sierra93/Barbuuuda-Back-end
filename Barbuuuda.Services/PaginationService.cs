using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Pagination.Output;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Barbuuuda.Core.Consts;

namespace Barbuuuda.Services
{
    public sealed class PaginationService : IPaginationService
    {
        private readonly PostgreDbContext _postgre;
        private readonly IUserService _userService;

        public PaginationService(PostgreDbContext postgre, IUserService userService)
        {
            _postgre = postgre;
            _userService = userService;
        }

        /// <summary>
        /// Метод пагинации.
        /// </summary>
        /// <param name="pageIdx">Номер страницы.</param>
        /// <param name="userName">Имя юзера.</param>
        /// <returns>Данные пагинации.</returns>
        public async Task<IndexOutput> GetInitPaginationAuctionTasks(int pageIdx)
        {
            try
            {
                var countRows = 10;   // Кол-во заданий на странице.

                var getTasks = (from tasks in _postgre.Tasks
                                join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                                join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                                where statuses.StatusName.Equals(StatusTask.AUCTION)
                                join users in _postgre.Users on tasks.OwnerId equals users.Id
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
                                    users.UserName
                                })
                              .OrderBy(o => o.TaskId)
                              .AsQueryable();

                var count = await getTasks.CountAsync();
                //var items = await getTasks.Skip((pageIdx - 1) * countRows).Take(countRows).ToListAsync();
                var items = await getTasks.Take(countRows).ToListAsync();

                var pageData = new PaginationOutput(count, pageIdx, countRows);
                var paginationData = new IndexOutput
                {
                    PageData = pageData,
                    Tasks = items,
                    TotalCount = count,
                    IsLoadAll = count < countRows,
                    IsVisiblePagination = count > 10
                };

                if (paginationData.IsLoadAll)
                {
                    var difference = countRows - count;
                    paginationData.TotalCount += difference;
                }

                return paginationData;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод пагинации всех заданий аукциона.
        /// </summary>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="countRows">Кол-во строк.</param>
        /// <returns>Данные пагинации.</returns>
        public async Task<IndexOutput> GetPaginationAuction(int pageNumber, int countRows)
        {
            try
            {
                var aAuctionTasks = (from tasks in _postgre.Tasks
                                     join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                                     join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                                     join users in _postgre.Users on tasks.OwnerId equals users.Id
                                     where statuses.StatusName.Equals(StatusTask.AUCTION)
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
                                         users.UserName
                                     })
                              .OrderBy(o => o.TaskId)
                              .AsQueryable();

                var count = await aAuctionTasks.CountAsync();
                var items = await aAuctionTasks.Skip((pageNumber - 1) * countRows).Take(countRows).ToListAsync();

                var pageData = new PaginationOutput(count, pageNumber, countRows);
                var paginationData = new IndexOutput
                {
                    PageData = pageData,
                    Tasks = items,
                    TotalCount = count,
                    IsLoadAll = count < countRows,
                    IsVisiblePagination = count > 10
                };

                if (paginationData.IsLoadAll)
                {
                    var difference = countRows - count;
                    paginationData.TotalCount += difference;
                }

                return paginationData;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод пагинации всех заданий в работе у исполнителя.
        /// </summary>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="countRows">Кол-во строк.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Данные пагинации.</returns>
        public async Task<IndexOutput> GetPaginationWorkAsync(int pageNumber, int countRows, string account)
        {
            try
            {
                var userId = await _userService.GetUserIdByLogin(account);

                var aAuctionTasks = (from tasks in _postgre.Tasks
                                     join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                                     join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                                     join users in _postgre.Users on tasks.OwnerId equals users.Id
                                     where statuses.StatusName.Equals(StatusTask.IN_WORK) 
                                           && tasks.ExecutorId.Equals(userId)
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
                                         users.UserName
                                     })
                              .OrderBy(o => o.TaskId)
                              .AsQueryable();

                var count = await aAuctionTasks.CountAsync();
                var items = await aAuctionTasks.Skip((pageNumber - 1) * countRows).Take(countRows).ToListAsync();

                var pageData = new PaginationOutput(count, pageNumber, countRows);
                var paginationData = new IndexOutput
                {
                    PageData = pageData,
                    Tasks = items,
                    TotalCount = count,
                    IsLoadAll = count < countRows,
                    IsVisiblePagination = count > 10
                };

                if (paginationData.IsLoadAll)
                {
                    var difference = countRows - count;
                    paginationData.TotalCount += difference;
                }

                return paginationData;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Метод пагинации в работе у исполнителя на ините станицы мои задания.
        /// </summary>
        /// <param name="pageIdx">Номер страницы.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Данные пагинации.</returns>
        public async Task<IndexOutput> GetInitPaginationWorkAsync(int pageIdx, string account)
        {
            try
            {
                var countRows = 10;   // Кол-во заданий на странице.
                var userId = await _userService.GetUserIdByLogin(account);

                var getTasks = (from tasks in _postgre.Tasks
                                join categories in _postgre.TaskCategories on tasks.CategoryCode equals categories.CategoryCode
                                join statuses in _postgre.TaskStatuses on tasks.StatusCode equals statuses.StatusCode
                                join users in _postgre.Users on tasks.OwnerId equals users.Id
                                where statuses.StatusName.Equals(StatusTask.IN_WORK)
                                      && tasks.ExecutorId.Equals(userId)
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
                                    users.UserName
                                })
                              .OrderBy(o => o.TaskId)
                              .AsQueryable();

                var count = await getTasks.CountAsync();
                //var items = await getTasks.Skip((pageIdx - 1) * countRows).Take(countRows).ToListAsync();
                var items = await getTasks.Take(countRows).ToListAsync();

                var pageData = new PaginationOutput(count, pageIdx, countRows);
                var paginationData = new IndexOutput
                {
                    PageData = pageData,
                    Tasks = items,
                    TotalCount = count,
                    IsLoadAll = count < countRows,
                    IsVisiblePagination = count > 10
                };

                if (paginationData.IsLoadAll)
                {
                    var difference = countRows - count;
                    paginationData.TotalCount += difference;
                }

                return paginationData;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
