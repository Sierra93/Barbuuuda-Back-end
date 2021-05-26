using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.MainPage;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;
using Barbuuuda.Models.Entities.MainPage.Output;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер главной страницы.
    /// </summary>
    [ApiController, Route("main")]
    public class MainPageController : BaseController
    {
        /// <summary>
        /// Сервис стартовой страницы.
        /// </summary>
        private readonly IMainPageService _mainPage;

        public MainPageController(IMainPageService mainPage)
        {
            _mainPage = mainPage;
        }

        /// <summary>
        /// Метод получает информацию для главного фона.
        /// </summary>
        /// <returns>Объект фона.</returns>
        [HttpPost, Route("get-fon")]
        public async Task<IActionResult> GetFonContent()
        {
            return Ok(await _mainPage.GetFonContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "ПОЧЕМУ BARBUUUDA".
        /// </summary>
        /// <returns>Все объекты WhyDto</returns>
        [HttpPost, Route("get-why")]
        public async Task<IActionResult> GetWhyContent()
        {
            return Ok(await _mainPage.GetWhyContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "КАК ЭТО РАБОТАЕТ".
        /// </summary>
        /// <returns>Все объекты WorkDto</returns>
        [HttpPost, Route("get-work")]
        public async Task<IActionResult> GetWorkContent()
        {
            return Ok(await _mainPage.GetWorkContent());
        }

        /// <summary>
        /// Метод выгружает данные для блока "ЧТО ВЫ ПОЛУЧАЕТЕ"
        /// </summary>
        /// <returns>Все объекты PrivilegeDto</returns>
        [HttpPost, Route("get-privilege")]
        public async Task<IActionResult> GetPrivilegeContent()
        {
            return Ok(await _mainPage.GetPrivilegeContent());
        }

        /// <summary>
        /// Метод возвращает данные для блока "Преимущества"
        /// </summary>
        /// <returns>Все объекты Advantage</returns>
        [HttpPost, Route("get-advantage")]
        public async Task<IActionResult> GetAdvantageContent()
        {
            return Ok(await _mainPage.GetAdvantageContent());
        }


        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("category-list")]
        public async Task<IActionResult> GetCategoryList()
        {
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
            IEnumerable aLastTasks = await _mainPage.GetLastTasksAsync();

            return Ok(aLastTasks);
        }

        /// <summary>
        /// Метод получит контактные данные сервиса.
        /// </summary>
        /// <returns>Контактная информация.</returns>
        [HttpPost, Route("contacts")]
        [ProducesResponseType(200, Type = typeof(ContactOutput))]
        public async Task<IActionResult> GetContactsAsync()
        {
            ContactOutput contact = await _mainPage.GetContactsAsync();

            return Ok(contact);
        }
    }
}
