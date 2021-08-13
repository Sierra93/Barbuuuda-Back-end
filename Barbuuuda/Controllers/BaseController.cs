﻿using Microsoft.AspNetCore.Mvc;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Базовый контроллер для всех контроллеров.
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Метод получит имя текущего юзера.
        /// </summary>
        /// <returns>Логин пользователя.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string GetUserName()
        {
            // Запишет логин в куки и вернет фронту.
            if (!HttpContext.Request.Cookies.ContainsKey("name"))
            {
                HttpContext.Response.Cookies.Append("name", HttpContext?.User?.Identity?.Name);
            }

            return HttpContext?.User?.Identity?.Name ?? GetLoginFromCookie();
        }

        /// <summary>
        /// Метод вернет логин пользователя из куки.
        /// </summary>  
        /// <returns>Логин пользователя.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        private string GetLoginFromCookie()
        {
            return HttpContext.Request.Cookies["name"];
        }
    }
}
