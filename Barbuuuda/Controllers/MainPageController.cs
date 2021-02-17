using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.MainPage;
using Barbuuuda.Models.User;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер главной страницы.
    /// </summary>
    [ApiController, Route("main")]
    public class MainPageController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public MainPageController(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
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
        [HttpPost, Route("get-fon")]
        public async Task<IActionResult> GetFonContent()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            return Ok(await _mainPage.GetFonContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "ПОЧЕМУ BARBUUUDA".
        /// </summary>
        /// <returns>Все объекты WhyDto</returns>
        [HttpPost, Route("get-why")]
        public async Task<IActionResult> GetWhyContent()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            return Ok(await _mainPage.GetWhyContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "КАК ЭТО РАБОТАЕТ".
        /// </summary>
        /// <returns>Все объекты WorkDto</returns>
        [HttpPost, Route("get-work")]
        public async Task<IActionResult> GetWorkContent()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            return Ok(await _mainPage.GetWorkContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "ЧТО ВЫ ПОЛУЧАЕТЕ"
        /// </summary>
        /// <returns>Все объекты PrivilegeDto</returns>
        [HttpPost, Route("get-privilege")]
        public async Task<IActionResult> GetPrivilegeContent()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            return Ok(await _mainPage.GetPrivilegeContent());
        }

        /// <summary>
        /// Метод возвращает данные для блока "Преимущества"
        /// </summary>
        /// <returns>Все объекты Advantage</returns>
        [HttpPost, Route("get-advantage")]
        public async Task<IActionResult> GetAdvantageContent()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            return Ok(await _mainPage.GetAdvantageContent());
        }


        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("category-list")]
        public async Task<IActionResult> GetCategoryList()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            IList aCategories = await _mainPage.GetCategoryList();

            return Ok(aCategories);
        }

        /// <summary>
        /// Метод полчает данные долгосрочного сотрудничества.
        /// </summary>
        /// <returns>ОБъект с данными.</returns>
        [HttpGet, Route("get-hope")]
        public async Task<IActionResult> GetHopeContent()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            HopeEntity oHope = await _mainPage.GetHopeContent();

            return Ok(oHope);
        }

        /// <summary>
        /// Метод выгружает 5 последних заданий. Не важно, чьи они.
        /// </summary>
        /// <returns>Список с 5 заданиями.</returns>
        [HttpGet, Route("last")]
        public async Task<IActionResult> GetLastTasksAsync()
        {
            IMainPage _mainPage = new MainPageService(_db, _postgre, _iden, _userManager, _signInManager);
            IEnumerable aLastTasks = await _mainPage.GetLastTasksAsync();

            return Ok(aLastTasks);
        }
    }
}
