using Microsoft.AspNetCore.Mvc;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Базовый контроллер для всех контроллеров.
    /// </summary>
    public class BaseController : ControllerBase
    {
        private readonly string _moduleName;

        protected BaseController(string moduleName) {
            _moduleName = moduleName;
        }

        /// <summary>
        /// Метод получает имя текущего юзера.
        /// </summary>
        /// <returns>Имя юзера.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string GetUserName()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
