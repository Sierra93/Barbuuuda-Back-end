using Barbuuuda.Core.Custom;
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
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер содержит логику работы с пользователями.
    /// </summary>
    [CustomAuthorization]
    [ApiController, Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserController(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
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
        public async Task<IActionResult> CreateUserAsync([FromBody] UserEntity user)
        {
            try
            {                
                // Ищет такой email в БД.
                bool bErrorEmail = await IdentityUserEmail(user.Email);

                if (!bErrorEmail)
                {
                    // Добавляет юзера.
                    user.DateRegister = DateTime.UtcNow;
                    var oAddedUser = await _userManager.CreateAsync(user, user.UserPassword);

                    if (oAddedUser.Succeeded)
                    {
                        // Генерит временный токен для юзера.
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            "ConfirmAsync",
                            "User",
                            new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme);

                        // Отправит уведомление на email.
                        await EmailService.SendEmailAsync(user.Email, "Подтверждение регистрации",
                            $"Подтвердите регистрацию на сервисе Barbuuuda, перейдя по ссылке: <a href='{callbackUrl}'>подтвердить</a>");

                        return Ok(oAddedUser);
                    }

                    else
                    {
                        // Что-то пошло не так, собирает ошибки запуская цепочку проверок валидации.
                        CustomValidatorVm custom = new CustomValidatorVm(_iden);
                        var aErrors = await custom.ValidateAsync(_userManager, user);

                        return Ok(aErrors);
                    }
                }

                throw new Exception();
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
            UserEntity oUser = await _iden.AspNetUsers
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
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, code);

            return new RedirectResult("https://publico-dev.xyz");
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// <paramref name="user">Объект с данными юзера.</paramref>
        /// </summary>        
        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserEntity user)
        {
            IUser _user = new UserService(_db, _postgre, _iden, _userManager, _signInManager);
            var oAuth = await _user.LoginAsync(user);
            //var res = User.Identity.Name;

            return Ok(oAuth);
        }
        /// <summary>
        /// Метод проверяет, авторизован ли юзер.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        /// <returns>Объект с данными авторизованного юзера.</returns>
        [HttpPost, Route("authorize")]
        public async Task<IActionResult> GetUserAuthorize([FromBody] UserEntity user)
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
        [Authorize]
        [HttpPost, Route("profile")]
        public async Task<IActionResult> GetProfileInfoAsync([FromQuery] string userId)
        {
            IUser _user = new UserService(_db, _postgre, _iden, _userManager, _signInManager);
            object oUser = await _user.GetProfileInfo(userId);

            return Ok(oUser);
        }

        /// <summary>
        /// Метод сохраняет личные данные юзера.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        [Authorize]
        [HttpPost, Route("save-data")]
        public async Task<IActionResult> SaveProfileDataAsync([FromBody] UserEntity user)
        {
            IUser _user = new UserService(_db, _postgre, _iden, _userManager, _signInManager);
            await _user.SaveProfileData(user);

            return Ok();
        }
    }
}
