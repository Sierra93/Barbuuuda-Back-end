﻿using Barbuuuda.Core.Data;
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
        PostgreDbContext _postgre;

        public UserController(ApplicationDbContext db, PostgreDbContext postgre) {
            _db = db;
            _postgre = postgre;
        }


        /// <summary>
        /// Метод создает нового пользователя.
        /// </summary>
        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] UserDto user) {
            IUser _user = new UserService(_db, _postgre);
            UserDto oUser = await _user.Create(user);

            return Ok(oUser);
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto user) {
            IUser _user = new UserService(_db, _postgre);
            var oUser = await _user.Login(user);

            return Ok(oUser);
        }

        /// <summary>
        /// Метод проверяет, авторизован ли юзер, если нет, то вернет false, иначе true.
        /// </summary>
        /// <param name="user">Объект для авторизации юзера.</param>
        /// <returns>true/false</returns>
        [HttpPost, Route("authorize")]
        public IActionResult Authorize([FromBody] UserAuthorizeVm user) {
            IUser _user = new UserService(_db, _postgre);
            int userId = 0;

            // Проверяет, авторизован ли юзер.
            bool bAuthorize = _user.Authorize(user.UserLogin, ref userId);

            // В зависимости от роли юзера формирует хидер.
            IList<HeaderTypeDto> aHeaderFields = _user.GetHeader(user.UserRole);

            // Результирующий объект.
            object oObj = new {
                bAuthorize,
                aHeaderFields,
                userId
            };

            return Ok(oObj);
        }

        /// <summary>
        /// Метод получает информацию о пользователе для профиля.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <returns>Объект с данными о профиле пользователя.</returns>
        [HttpPost, Route("profile")]
        public async Task<IActionResult> GetProfileInfo([FromQuery] int userId) {
            IUser _user = new UserService(_db, _postgre);
            object oUser = await _user.GetProfileInfo(userId);

            return Ok(oUser);
        }

        /// <summary>
        /// Метод сохраняет личные данные юзера.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        /// <returns>Измененные данные.</returns>
        [HttpPost, Route("save-data")]
        public async Task<IActionResult> SaveProfileData([FromBody] UserDto user) {
            IUser _user = new UserService(_db, _postgre);
            UserDto oUser = await _user.SaveProfileData(user);

            return Ok(oUser);
        }
    }
}
