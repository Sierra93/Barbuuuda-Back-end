using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.MainPage;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы главной страницы.
    /// </summary>
    public class MainPageService : IMainPage
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public MainPageService(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _db = db;
            _postgre = postgre;
            _userManager = userManager;
            _signInManager = signInManager;
            _iden = iden;
        }

        /// <summary>
        /// Метод получает информацию для главного фона.
        /// </summary>
        /// <returns>Объект фона.</returns>
        public async Task<FonEntity> GetFonContent()
        {
            try
            {
                FonEntity oFon = await _db.Fons.FirstOrDefaultAsync();

                return oFon != null ? oFon : throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new ArgumentNullException("Данные фона не найдены", ex.Message.ToString());
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает данные для блока "ПОЧЕМУ BARBUUUDA"
        /// </summary>
        /// <returns>Все объекты WhyDto</returns>
        public async Task<IList<WhyEntity>> GetWhyContent()
        {
            try
            {
                return await _db.Whies.ToListAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает данные для блока "ЧТО ВЫ ПОЛУЧАЕТЕ"
        /// </summary>
        /// <returns>Все объекты PrivilegeDto</returns>
        public async Task<IList<PrivilegeEntity>> GetPrivilegeContent()
        {
            try
            {
                return await _db.Privileges.ToListAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает данные для блока "КАК ЭТО РАБОТАЕТ"
        /// </summary>
        /// <returns>Все объекты WorkDto</returns>
        public async Task<IList<WorkEntity>> GetWorkContent()
        {
            try
            {
                return await _db.Works.ToListAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Выгружает данные для раздела "Преимущества"
        /// </summary>
        /// <returns>Все объекты Advantage</returns>
        public async Task<IList<AdvantageEntity>> GetAdvantageContent()
        {

            try
            {
                return await _db.Advantages.ToListAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }

        }


        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns></returns>
        public async Task<IList> GetCategoryList()
        {
            try
            {
                return await _postgre
                     .TaskCategories
                     .OrderBy(t => t.CategoryId)
                     .ToListAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод полчает данные долгосрочного сотрудничества.
        /// </summary>
        /// <returns>ОБъект с данными.</returns>
        public async Task<HopeEntity> GetHopeContent()
        {
            try
            {
                return await _db.Hopes.FirstOrDefaultAsync();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает 5 последних заданий. Не важно, чьи они.
        /// </summary>
        /// <returns>Список с 5 заданиями.</returns>
        public async Task<IEnumerable> GetLastTasksAsync()
        {
            try
            {
                ITask _task = new TaskService(_db, _postgre, _iden, _userManager, _signInManager);
                var aTasks = await (from tasks in _postgre.Tasks
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
                          .ToListAsync();
                IEnumerable aReverseTasks = ExtensionService.Reverse(aTasks).Take(5);

                return aReverseTasks;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }        
    }
}
