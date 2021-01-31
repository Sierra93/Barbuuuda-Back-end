using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.ViewModels.User;
using Barbuuuda.Emails;
using Barbuuuda.Models.User;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит логику работы с пользователями.
    /// </summary>
    [ApiController, Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserDto> _userManager;
        private readonly SignInManager<UserDto> _signInManager;

        public UserController(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserDto> userManager, SignInManager<UserDto> signInManager)
        {
            _db = db;
            _postgre = postgre;
            _userManager = userManager;
            _signInManager = signInManager;
            _iden = iden;
        }


        /// <summary>
        /// Метод создает нового пользователя.
        /// <paramref name="user">Объект с данными юзера.</paramref>
        /// </summary>
        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            // Добавляет юзера.
            user.DateRegister = DateTime.UtcNow;
            var addedUser = await _userManager.CreateAsync(user, user.UserPassword);

            // Если регистрация успешна.
            if (addedUser.Succeeded)
            {
                // Генерит временный токен для юзера.
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmAsync",
                    "User",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService(_iden);
                await emailService.SendEmailAsync(user.Email, "Подтверждение регистрации",
                    $"Подтвердите регистрацию на сервисе Barbuuuda, перейдя по ссылке: <a href='{callbackUrl}'>подтвердить</a>");

                return Ok();
            }

            else
            {
                // Что-то пошло не так, собирает ошибки запуская цепочку проверок валидации.
                CustomValidatorVm custom = new CustomValidatorVm(_iden);
                var aErrors = await custom.ValidateAsync(_userManager, user);

                return Ok(aErrors);
            }
        }

        /// <summary>
        /// Метод проставит подтверждение регистрации в true и перенаправит на главную страницу сервиса.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <param name="code">Временный код токена.</param>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmAsync(string userId, string code)
        {            
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, code);            

            return new RedirectResult("https://publico-dev.xyz");
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// <paramref name="user">Объект с данными юзера.</paramref>
        /// </summary>
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            IUser _user = new UserService(_db, _postgre, _iden, _userManager, _signInManager);
            var oAuth = await _user.LoginAsync(user);

            return Ok(oAuth);
        }
        /// <summary>
        /// Метод проверяет, авторизован ли юзер.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        /// <returns>Объект с данными авторизованного юзера.</returns>
        [HttpPost, Route("authorize")]
        public async Task<IActionResult> GetUserAuthorize([FromBody] UserDto user)
        {
            IUser _user = new UserService(_db, _postgre, _iden, _userManager, _signInManager);
            object oAuthorize = await _user.GetUserAuthorize(user.UserName);

            return Ok(oAuthorize);
        }

        /// <summary>
        /// Метод получает информацию о пользователе для профиля.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <returns>Объект с данными о профиле пользователя.</returns>
        [HttpPost, Route("profile")]
        public async Task<IActionResult> GetProfileInfo([FromQuery] string userId)
        {
            IUser _user = new UserService(_db, _postgre, _iden, _userManager, _signInManager);
            object oUser = await _user.GetProfileInfo(userId);

            return Ok(oUser);
        }

        /// <summary>
        /// Метод сохраняет личные данные юзера.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        [HttpPost, Route("save-data")]
        public async Task<IActionResult> SaveProfileData([FromBody] UserDto user)
        {
            IUser _user = new UserService(_db, _postgre, _iden, _userManager, _signInManager);
            await _user.SaveProfileData(user);

            return Ok();
        }
    }
}
