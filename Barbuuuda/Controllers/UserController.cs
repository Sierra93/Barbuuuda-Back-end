﻿using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Extensions.User;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Input;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Barbuuuda.Emails.Service;
using Barbuuuda.Models.Entities.Payment;
using Barbuuuda.Models.User.Output;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит логику работы с пользователями.
    /// </summary>        
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController, Route("user")]
    public class UserController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext db, PostgreDbContext postgre, UserManager<UserEntity> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
            _postgre = postgre;
            _db = db;
        }

        /// <summary>
        /// Метод создает нового пользователя.
        /// <paramref name="user">Входная модель пользователя.</paramref>
        /// </summary>
        [AllowAnonymous]
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserInput user)
        {
            try
            {
                var result = await CreateUser(user);

                return result;
            }

            catch (Exception ex)
            {
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = logger.LogCritical();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод проверяет в БД существование юзера с таким email.
        /// </summary>
        /// <param name="email">Почта юзера.</param>
        /// <returns>true - если существует, иначе false.</returns>
        private async Task<bool> IdentityUserEmail(string email)
        {
            var oUser = await _postgre.Users
                    .Where(u => u.Email
                    .Equals(email))
                    .FirstOrDefaultAsync();

            return oUser != null;
        }

        /// <summary>
        /// Метод проставит подтверждение регистрации в true и перенаправит на главную страницу сервиса.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <param name="code">Временный код токена.</param>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ConfirmAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, code);

            return new RedirectResult("https://barbuuuda.ru");
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// <paramref name="user">Входная модель пользователя.</paramref>
        /// </summary>        
        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserInput user)
        {
            var oAuth = await _userService.LoginAsync(user);

            return Ok(oAuth);
        }

        /// <summary>
        /// Метод проверяет, авторизован ли юзер.
        /// </summary>
        /// <returns>Объект с данными авторизованного юзера.</returns>       
        [AllowAnonymous]
        [HttpGet, Route("authorize")]
        public async Task<IActionResult> GetUserAuthorize([FromQuery] string userName)
        {
            var oAuthorize = await _userService.GetUserAuthorize(GetUserName() ?? userName);

            return Ok(oAuthorize);
        }

        /// <summary>
        /// Метод получает информацию о пользователе для профиля.
        /// </summary>
        /// <returns>Объект с данными о профиле пользователя.</returns>
        [HttpPost, Route("profile")]
        [ProducesResponseType(200, Type = typeof(ProfileOutput))]
        public async Task<IActionResult> GetProfileInfoAsync()
        {
            var user = await _userService.GetProfileInfo(GetUserName());

            return Ok(user);
        }

        /// <summary>
        /// Метод сохраняет личные данные юзера.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        [HttpPost, Route("save-data")]
        public async Task<IActionResult> SaveProfileDataAsync([FromBody] UserInput user)
        {
            await _userService.SaveProfileData(user, GetUserName());

            return Ok();
        }

        /// <summary>
        /// Метод обновляет токен юзеру.
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Обновленный токен.</returns>
        [AllowAnonymous]
        [HttpGet, Route("token")]
        [ProducesResponseType(200, Type = typeof(UserOutput))]
        public async Task<IActionResult> RefreshToken([FromQuery] string userName)
        {
            var refreshData = await _userService.GenerateToken(userName ?? GetUserName());

            return Ok(refreshData);
        }

        /// <summary>
        /// Метод создает пользователя.
        /// </summary>
        /// <param name="user">Входная модель пользователя.</param>
        /// <returns>Список ошибок или статус успеха регистрации.</returns>
        private async Task<IActionResult> CreateUser(UserInput user)
        {
            try
            {
                IdentityResult errors = null;

                // Ищет такой email в БД.
                bool bErrorEmail = await IdentityUserEmail(user.Email);

                if (!bErrorEmail)
                {                   
                    try
                    {
                        // Генерит временный токен для пользователя.
                        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        // Готовит ссылку, которая будет отображена в письме.
                        string callbackUrl = Url.Action("ConfirmAsync", "User", new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme)
                            .Replace("http://localhost:58822", "https://barbuuuda.ru")
                            .Replace("https://barbuuuda.online", "https://barbuuuda.ru");

                        // Отправит уведомление на email.
                        EmailService emailService = new EmailService(_db);
                        await emailService.SendEmailAsync(user.Email, "Подтверждение регистрации",
                            $"Подтвердите регистрацию на сервисе Barbuuuda, перейдя по ссылке: <a href='{callbackUrl}'>подтвердить</a>");
                    }

                    // Если почта не существует.
                    catch (Exception ex)
                    {
                        Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                        _ = logger.LogCritical();

                        return BadRequest(ErrorValidate.EMAIL_NOT_ENTITY);
                    }

                    // Проставит дату регистрации = текущая дата и время.
                    user.DateRegister = DateTime.UtcNow;

                    // Регистрирует пользователя.
                    IdentityResult oAddedUser = await _userManager.CreateAsync(user, user.UserPassword);

                    // Если регистрация успешна.
                    if (oAddedUser.Succeeded)
                    {
                        // Находит добавленного пользователя и берет его Id.
                        string userId = await _userService.GetLastUserAsync();

                        // Создаст счет пользователю (по дефолту в валюте RUB).
                        await _postgre.AddAsync(new InvoiceEntity()
                        {
                            UserId = userId,
                            InvoiceAmount = 0,
                            Currency = CurrencyType.CURRENCY_RUB,
                            ScoreNumber = null,
                            ScoreEmail = string.Empty
                        });
                        await _postgre.SaveChangesAsync();

                        return Ok(oAddedUser);
                    }
                }

                // Что-то пошло не так, собирает ошибки запуская цепочку проверок валидации.
                else
                {                    
                    CustomValidatorExtension custom = new CustomValidatorExtension(_postgre);
                    errors = await custom.ValidateAsync(_userManager, user);
                }

                return BadRequest(errors);
            }

            catch (Exception ex)
            {
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await logger.LogCritical();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод получит роль пользователя по его логину.
        /// </summary>
        /// <returns>Роль пользователя.</returns>
        [HttpPost, Route("role")]
        [ProducesResponseType(200, Type = typeof(UserOutput))]
        public async Task<IActionResult> GetUserRoleByLoginAsync()
        {
            var role = await _userService.GetUserRoleByLoginAsync(GetUserName());

            return Ok(role);
        }
    }
}
