using Barbuuuda.Core.Consts;
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

        /// <summary>
        /// Сервис работы с юзерами.
        /// </summary>
        private readonly IUserService _user;

        public UserController(ApplicationDbContext db, PostgreDbContext postgre, UserManager<UserEntity> userManager, IUserService user)
        {
            _userManager = userManager;
            _user = user;
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
                IActionResult result = await CreateUser(user);

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
            UserEntity oUser = await _postgre.Users
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
            UserEntity user = await _userManager.FindByIdAsync(userId);
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
            object oAuth = await _user.LoginAsync(user);

            return Ok(oAuth);
        }
        /// <summary>
        /// TODO: нужно убрать этот метод, так как авторизация переделана на основе токенов и доступ и так будет отваливаться когда токен протухнет.
        /// Метод проверяет, авторизован ли юзер.
        /// </summary>
        /// <returns>Объект с данными авторизованного юзера.</returns>       
        [AllowAnonymous]
        [HttpGet, Route("authorize")]
        public async Task<IActionResult> GetUserAuthorize([FromQuery] string userName)
        {
            object oAuthorize = await _user.GetUserAuthorize(GetUserName() ?? userName);

            return Ok(oAuthorize);
        }

        /// <summary>
        /// Метод получает информацию о пользователе для профиля.
        /// </summary>
        /// <returns>Объект с данными о профиле пользователя.</returns>
        [HttpGet, Route("profile")]
        public async Task<IActionResult> GetProfileInfoAsync()
        {
            object oUser = await _user.GetProfileInfo(GetUserName());

            return Ok(oUser);
        }

        /// <summary>
        /// Метод сохраняет личные данные юзера.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        [HttpPost, Route("save-data")]
        public async Task<IActionResult> SaveProfileDataAsync([FromBody] UserInput user)
        {
            await _user.SaveProfileData(user, GetUserName());

            return Ok();
        }

        /// <summary>
        /// Метод обновляет токен юзеру.
        /// </summary>
        /// <returns>Строка токена.</returns>
        [AllowAnonymous]
        [HttpGet, Route("token")]
        public async Task<IActionResult> RefreshToken([FromQuery] string userName)
        {
            string sToken = await _user.GenerateToken(userName ?? GetUserName());

            return Ok(sToken);
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
                        string userId = await _user.GetLastUserAsync();

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
    }
}
