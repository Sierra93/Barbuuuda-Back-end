using Barbuuuda.Core.Data;
using Barbuuuda.Core.Extensions.User;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Emails;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Input;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит логику работы с пользователями.
    /// </summary>        
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController, Route("user")]
    public class UserController : BaseController
    {
        private readonly PostgreDbContext _postgre;
        private readonly UserManager<UserEntity> _userManager;
        public static string Module => "Barbuuuda.User";

        /// <summary>
        /// Сервис работы с юзерами.
        /// </summary>
        private readonly IUser _user;

        public UserController(PostgreDbContext postgre, UserManager<UserEntity> userManager, IUser user) : base(Module)
        {
            _userManager = userManager;
            _user = user;
            _postgre = postgre;
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
                throw new Exception(ex.Message.ToString());
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

            return oUser != null ? true : false;
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
            string sToken = await _user.GenerateToken(userName ?? GetUserName() ?? null);

            return Ok(sToken);
        }

        /// <summary>
        /// Метод создает пользователя.
        /// </summary>
        /// <param name="user">Входная модель пользователя.</param>
        /// <returns></returns>
        private async Task<IActionResult> CreateUser(UserInput user)
        {
            IdentityResult errors = null;

            if (user.Score == null)
            {
                user.Score = 0;
            }

            // Ищет такой email в БД.
            bool bErrorEmail = await IdentityUserEmail(user.Email);

            if (!bErrorEmail)
            {
                // Добавляет юзера.
                user.DateRegister = DateTime.UtcNow;
                IdentityResult oAddedUser = await _userManager.CreateAsync(user, user.UserPassword);

                if (oAddedUser.Succeeded)
                {
                    // Генерит временный токен для юзера.
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string callbackUrl = Url.Action("ConfirmAsync", "User", new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    // Отправит уведомление на email.
                    await EmailService.SendEmailAsync(user.Email, "Подтверждение регистрации",
                        $"Подтвердите регистрацию на сервисе Barbuuuda, перейдя по ссылке: <a href='{callbackUrl}'>подтвердить</a>");

                    return Ok(oAddedUser);
                }
            }

            else
            {
                // Что-то пошло не так, собирает ошибки запуская цепочку проверок валидации.
                CustomValidatorExtension custom = new CustomValidatorExtension(_postgre);
                errors = await custom.ValidateAsync(_userManager, user);
            }

            return BadRequest(errors);
        }
    }
}
