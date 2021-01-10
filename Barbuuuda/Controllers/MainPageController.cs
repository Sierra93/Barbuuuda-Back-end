using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers {
    /// <summary>
    /// Контроллер главной страницы.
    /// </summary>
    [ApiController, Route("main")]
    public class MainPageController : Controller {
        ApplicationDbContext _db;

        public MainPageController(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает информацию для главного фона.
        /// </summary>
        /// <returns>Объект фона.</returns>
        [HttpPost, Route("get-fon")]
        public async Task<IActionResult> GetFonContent() {
            IMainPage _mainPage = new MainPageService(_db);
            return Ok(await _mainPage.GetFonContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "ПОЧЕМУ BARBUUUDA".
        /// </summary>
        /// <returns>Все объекты WhyDto</returns>
        [HttpPost, Route("get-why")]
        public async Task<IActionResult> GetWhyContent() {
            IMainPage _mainPage = new MainPageService(_db);
            return Ok(await _mainPage.GetWhyContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "КАК ЭТО РАБОТАЕТ".
        /// </summary>
        /// <returns>Все объекты WorkDto</returns>
        [HttpPost, Route("get-work")]
        public async Task<IActionResult> GetWorkContent() {
            IMainPage _mainPage = new MainPageService(_db);
            return Ok(await _mainPage.GetWorkContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "ЧТО ВЫ ПОЛУЧАЕТЕ"
        /// </summary>
        /// <returns>Все объекты PrivilegeDto</returns>
        [HttpPost, Route("get-privilege")]
        public async Task<IActionResult> GetPrivilegeContent() {
            IMainPage _mainPage = new MainPageService(_db);
            return Ok(await _mainPage.GetPrivilegeContent());
        }

        /// <summary>
        /// Метод возвращает данные для блока "Преимущества"
        /// </summary>
        /// <returns>Все объекты Advantage</returns>
        [HttpPost, Route("get-advantage")]
        public async Task<IActionResult> GetAdvantageContent() {
            IMainPage _mainPage = new MainPageService(_db);
            return Ok(await _mainPage.GetAdvantageContent());
        }
    }
}
