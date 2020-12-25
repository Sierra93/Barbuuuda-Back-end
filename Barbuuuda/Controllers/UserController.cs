using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.ViewModels.User;
using Barbuuuda.Models.User;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers {
    /// <summary>
    /// Контроллер содержит логику работы с пользователями.
    /// </summary>
    [ApiController, Route("user")]
    public class UserController : ControllerBase {
        ApplicationDbContext _db;

        public UserController(ApplicationDbContext db) {
            _db = db;
        }


        /// <summary>
        /// Метод создает нового пользователя.
        /// </summary>
        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] UserDto user) {
            IUser _user = new UserService(_db);
            UserDto oUser = await _user.Create(user);

            return Ok(oUser);
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto user) {
            IUser _user = new UserService(_db);
            var oUser = await _user.Login(user);

            return Ok(oUser);
        }

        /// <summary>
        /// Метод проверяет, авторизован ли юзер, если нет, то вернет false, иначе true.
        /// </summary>
        /// <param name="role">Роль юзера.</param>
        /// <returns>true/false</returns>
        [HttpPost, Route("authorize/{login}")]
        public async Task<IActionResult> Authorize([FromBody] UserAuthorizeVm user) {
            IUser _user = new UserService(_db);

            // Проверяет, авторизован ли юзер.
            bool bAuthorize = await _user.Authorize(user.UserLogin);

            // В зависимости от роли юзера формирует хидер.
            IList<HeaderTypeDto> aHeaderFields = await _user.GetHeader(user.UserRole);

            // Результирующий объект.
            object oObj = new {
                bAuthorize,
                aHeaderFields
            };

            return Ok(oObj);
        }
    }
}
