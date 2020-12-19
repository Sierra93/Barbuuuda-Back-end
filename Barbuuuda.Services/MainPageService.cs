using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.MainPage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Services {
    /// <summary>
    /// Сервис реализует методы главной страницы.
    /// </summary>
    public class MainPageService : IMainPage {
        ApplicationDbContext _db;

        public MainPageService(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает информацию для главного фона.
        /// </summary>
        /// <returns>Объект фона.</returns>
        public async Task<FonDto> GetFonContent() {
            try {
                FonDto oFon = await _db.Fons.FirstOrDefaultAsync();

                return oFon != null ? oFon : throw new ArgumentNullException();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Данные фона не найдены", ex.Message.ToString());
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает данные для блока "ПОЧЕМУ BARBUUUDA"
        /// </summary>
        /// <returns>Все объекты WhyDto</returns>
        public async Task<IList<WhyDto>> GetWhyContent() {
            try {
                return await _db.Whies.ToListAsync();
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает данные для блока "ЧТО ВЫ ПОЛУЧАЕТЕ"
        /// </summary>
        /// <returns>Все объекты PrivilegeDto</returns>
        public async Task<IList<PrivilegeDto>> GetPrivilegeContent() {
            try {
                return await _db.Privileges.ToListAsync();
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает данные для блока "КАК ЭТО РАБОТАЕТ"
        /// </summary>
        /// <returns>Все объекты WorkDto</returns>
        public async Task<IList<WorkDto>> GetWorkContent() {
            try {
                return await _db.Works.ToListAsync();
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Выгружает данные для раздела "Преимущества"
        /// </summary>
        /// <returns>Все объекты Advantage</returns>
        public async Task<IList<AdvantageDto>> GetAdvantageContent() {

            try {
                return await _db.Advantages.ToListAsync();
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }

        }
    }
}
