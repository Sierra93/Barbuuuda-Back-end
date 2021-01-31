using Barbuuuda.Core.Data;
using Barbuuuda.Core.Extensions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Core.ViewModels.User;
using Barbuuuda.Emails;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы пользователя.
    /// </summary>
    public class UserService : IUser
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly IdentityDbContext _iden;
        private readonly UserManager<UserDto> _userManager;
        private readonly SignInManager<UserDto> _signInManager;

        public UserService(ApplicationDbContext db, PostgreDbContext postgre, IdentityDbContext iden, UserManager<UserDto> userManager, SignInManager<UserDto> signInManager)
        {
            _db = db;
            _postgre = postgre;
            _userManager = userManager;
            _signInManager = signInManager;
            _iden = iden;
        }

        /// <summary>
        /// Метод создает нового пользователя.
        /// </summary>
        /// <param name="user">Объект с данными регистрации пользователя.</param>
        public async Task<object> CreateAsync(UserDto user)
        {
            try
            {
                // Добавляет юзера.
                user.DateRegister = DateTime.UtcNow;
                var addedUser = await _userManager.CreateAsync(user, user.UserPassword);

                // Если регистрация успешна.
                if (addedUser.Succeeded)
                {
                    return addedUser;
                }

                else
                {
                    // Что-то пошло не так, собирает ошибки запуская цепочку проверок валидации.
                    CustomValidatorVm custom = new CustomValidatorVm(_iden);

                    return await custom.ValidateAsync(_userManager, user);
                }

                throw new Exception();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        /// <param name="user">Объект данных юзера.</param>
        /// <returns>Статус true/false</returns>
        public async Task<object> LoginAsync(UserDto user)
        {
            try
            {
                // Авторизует юзера.
                var oAuth = await _signInManager.PasswordSignInAsync(user.UserName, user.UserPassword, user.RememberMe, false);

                // Выбирает роли юзера.
                IList<string> aRoles = await GetUserRole(user.UserName);

                // Если авторизация успешна.
                if (oAuth.Succeeded)
                {
                    string sToken = await GetToken(user); // Генерит токен юзеру.

                    return new
                    {
                        oAuth.Succeeded,
                        oAuth.IsLockedOut,
                        user = user.UserName,
                        userToken = sToken,
                        userId = user.Id,
                        role = aRoles
                    };
                }

                else
                {
                    throw new ArgumentException();
                }
            }

            catch (ArgumentNullException ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new ArgumentNullException("Такого пользователя не существует", ex.Message.ToString());
            }

            catch (ArgumentException ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogError();
                throw new ArgumentException("Логин или пароль введены не верно", ex.Message.ToString());
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выбирает роли юзера.
        /// </summary>
        /// <param name="username">Логин юзера.</param>
        /// <returns></returns>
        private async Task<IList<string>> GetUserRole(string username)
        {
            return await _iden.AspNetUsers
                .Where(u => u.UserName
                .Equals(username))
                .Select(r => r.UserRole)
                .ToListAsync();
        }

        /// <summary>
        /// Метод выдает токен юзеру, если он прошел авторизацию.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        /// <returns>Токен юзера.</returns>
        private async Task<string> GetToken(UserDto user)
        {
            ClaimsIdentity oClaim = GetClaim(user.UserName);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: oClaim.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            await SetUserToken(encodedJwt, user.UserName);   // Запишет токен юзера в БД.

            return encodedJwt;
        }

        /// <summary>
        /// Метод запишет токен юзера в БД.
        /// </summary>
        /// <param name="token">Токен юзера.</param>
        private async Task SetUserToken(string token, string username)
        {
            UserDto oUser = await _iden.AspNetUsers
                .Where(u => u.UserName
                .Equals(username))
                .FirstOrDefaultAsync();

            // Запишет токен.
            oUser.UserToken = token;
            await _iden.SaveChangesAsync();
        }

        private ClaimsIdentity GetClaim(string username)
        {
            var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username)
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        /// <summary>
        /// Метод проверяет, авторизован ли юзер.
        /// </summary>
        /// <param name="username">login юзера.</param>
        /// <returns>Объект с данными авторизованного юзера.</returns>
        public async Task<object> GetUserAuthorize(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentNullException();
                }
                string userId = string.Empty;

                // Выбирает юзера по логину.
                UserDto oUser = await _iden.AspNetUsers
                    .Where(u => u.UserName
                    .Equals(username))
                    .FirstOrDefaultAsync();

                // В зависимости от роли юзера формирует хидер.
                IList<HeaderTypeDto> aHeaderFields = await GetHeader(oUser.UserRole);

                if (oUser != null)
                {
                    userId = oUser.Id;
                }

                // Авторизован ли юзер.
                bool bAuth = oUser.UserToken != null ? true : false;

                return new
                {
                    aHeaderFields,
                    bAuth,
                    userId
                };
            }

            catch (ArgumentNullException ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
                throw new ArgumentNullException("Логин пользователя не передан", ex.Message.ToString());
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает хидер в зависимости от роли.
        /// </summary>
        /// <param name="role">Роль юзера.</param>
        /// <returns></returns>
        private async Task<IList<HeaderTypeDto>> GetHeader(string role)
        {
            try
            {
                return await _db.Headers
                    .Where(h => h.HeaderType
                    .Equals(role))
                    .ToListAsync();
            }

            catch (IndexOutOfRangeException ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
                throw new IndexOutOfRangeException($"Поля этой роли не сформированы {ex.Message}");
            }

            catch (ArgumentNullException ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogError();
                throw new ArgumentNullException($"Роль пользователя не передана {ex.Message}");
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает информацию о пользователе для профиля.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <returns>Объект с данными о профиле пользователя.</returns>
        public async Task<object> GetProfileInfo(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException();
                }

                return await _postgre.Users
                    .Where(u => u.Id.Equals(userId))
                    .Select(up => new
                    {
                        up.UserName,
                        up.Email,
                        up.PhoneNumber,
                        up.LastName,
                        up.FirstName,
                        up.Patronymic,
                        up.UserIcon,
                        up.Rating,
                        dateRegister = string.Format("{0:f}", up.DateRegister),
                        scoreMoney = string.Format("{0:0,0}", up.Score),
                        up.AboutInfo,
                        up.Plan,
                        up.City,
                        up.Age
                    })
                    .FirstOrDefaultAsync();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не передан UserId {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод сохраняет личные данные юзера.
        /// </summary>
        /// <param name="needUserUpdate">Объект с данными юзера.</param>
        public async Task SaveProfileData(UserDto needUserUpdate)
        {
            try
            {
                if (string.IsNullOrEmpty(needUserUpdate.Id))
                    throw new ArgumentNullException();

                // Изменяет объект юзера.
                await ChangeProfileData(needUserUpdate);
                await _postgre.SaveChangesAsync();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не передан UserId {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод изменяет объект юзера.
        /// </summary>
        /// <param name="needUserUpdate">Исходный объект юзера для изменения.</param>
        private async Task ChangeProfileData(UserDto needUserUpdate)
        {
            UserDto oldUser = await _postgre.Users
                    .Where(u => u.Id.Equals(needUserUpdate.Id))
                    .FirstOrDefaultAsync();

            // Изменяет некоторые поля.
            oldUser.LastName = needUserUpdate.LastName;
            oldUser.FirstName = needUserUpdate.FirstName;
            oldUser.Patronymic = needUserUpdate.Patronymic;
            oldUser.Email = needUserUpdate.Email;
            oldUser.City = needUserUpdate.City;
            oldUser.Gender = needUserUpdate.Gender;
        }
    }
}
