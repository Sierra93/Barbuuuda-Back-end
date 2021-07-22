using Microsoft.AspNetCore.Mvc;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Базовый контроллер для всех контроллеров.
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Метод получает имя текущего юзера.
        /// </summary>
        /// <returns>Имя юзера.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string GetUserName()
        {
            return HttpContext?.User?.Identity?.Name;
        }
    }
}
