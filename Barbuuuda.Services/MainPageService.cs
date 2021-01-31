using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.MainPage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы главной страницы.
    /// </summary>
    public class MainPageService : IMainPage
    {
        ApplicationDbContext _db;
        PostgreDbContext _postgre;

        public MainPageService(ApplicationDbContext db, PostgreDbContext postgre)
        {
            _db = db;
            _postgre = postgre;
        }

        /// <summary>
        /// Метод получает информацию для главного фона.
        /// </summary>
        /// <returns>Объект фона.</returns>
        public async Task<FonDto> GetFonContent()
        {
            try
            {
                FonDto oFon = await _db.Fons.FirstOrDefaultAsync();

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
        public async Task<IList<WhyDto>> GetWhyContent()
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
        public async Task<IList<PrivilegeDto>> GetPrivilegeContent()
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
        public async Task<IList<WorkDto>> GetWorkContent()
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
        public async Task<IList<AdvantageDto>> GetAdvantageContent()
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
        public async Task<HopeDto> GetHopeContent()
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
    }
}
