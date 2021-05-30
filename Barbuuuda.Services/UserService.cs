using AutoMapper;
using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Extensions.User;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Input;
using Barbuuuda.Models.User.Output;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы пользователя.
    /// </summary>
    public sealed class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public UserService(ApplicationDbContext db, PostgreDbContext postgre, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _db = db;
            _postgre = postgre;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        /// <param name="user">Объект данных юзера.</param>
        /// <returns>Статус true/false</returns>
        public async Task<object> LoginAsync(UserInput user)
        {
            try
            {
                SignInResult auth = null;
                bool isContinue = false;

                // Проверит, логин передан или email.
                bool isEmail = CustomValidatorExtension.CheckIsEmail(user.UserName);

                // Если нужно проверять по логину.
                if (!isEmail)
                {
                    // Авторизует юзера.
                    auth = await _signInManager.PasswordSignInAsync(user.UserName, user.UserPassword, user.RememberMe, false);
                }

                // Значит в UserName лежит email.
                else
                {
                    // Найдет пользователя по почте.
                    UserEntity findUser = await _userManager.FindByEmailAsync(user.UserName);

                    if (findUser != null && findUser.UserPassword.Equals(user.UserPassword))
                    {
                        isContinue = true;

                        // Перезапишет UserName на логин выбранный из БД для фронта.
                        user.UserName = findUser.UserName;
                    }                    
                }

                // Если авторизация успешна.
                if ((auth != null && auth.Succeeded) || isContinue)
                {
                    ClaimsIdentity claim = GetIdentityClaim(user);

                    // Выбирает роли юзера.
                    IEnumerable<string> aRoles = await GetUserRole(user.UserName);

                    // Генерит токен юзеру.
                    string sToken = GenerateToken(claim).Result;

                    return new
                    {
                        user = claim.Name,
                        userToken = sToken,
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
                throw new ArgumentException("Логин и (или) пароль введены не верно", ex.Message.ToString());
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
            return await _postgre.Users
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
        private ClaimsIdentity GetIdentityClaim(UserEntity user)
        {
            ClaimsIdentity oClaim = GetClaim(user.UserName);           

            return oClaim;
        }

        /// <summary>
        /// Метод запишет токен юзера в БД.
        /// </summary>
        /// <param name="token">Токен юзера.</param>
        //private async Task SetUserToken(string token, string username)
        //{
        //    UserEntity oUser = await _postgre.Users
        //        .Where(u => u.UserName
        //        .Equals(username))
        //        .FirstOrDefaultAsync();

        //    // Запишет токен.
        //    oUser.UserToken = token;
        //    await _postgre.SaveChangesAsync();
        //}

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
                UserEntity oUser = await _postgre.Users
                    .Where(u => u.UserName
                    .Equals(username))
                    .FirstOrDefaultAsync();

                // В зависимости от роли юзера формирует хидер.
                IList<HeaderTypeEntity> aHeaderFields = await GetHeader(oUser.UserRole);

                if (oUser != null)
                {
                    userId = oUser.Id;
                }

                return new { aHeaderFields };
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
        private async Task<IList<HeaderTypeEntity>> GetHeader(string role)
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
        public async Task<object> GetProfileInfo(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException();
                }

                return await _postgre.Users
                    .Where(u => u.UserName.Equals(userName))
                    .Select(up => new
                    {
                        up.UserName,
                        up.Email,
                        up.PhoneNumber,
                        up.LastName,
                        up.FirstName,
                        up.Patronymic,
                        up.UserIcon,
                        dateRegister = string.Format("{0:f}", up.DateRegister),
                        //scoreMoney = string.Format("{0:0,0}", up.Score),
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
        public async Task SaveProfileData(UserEntity needUserUpdate, string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    throw new ArgumentNullException();

                // Изменяет объект юзера.
                await ChangeProfileData(needUserUpdate, userName);
                await _postgre.SaveChangesAsync();
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не передан UserId {ex.Message}");
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод изменяет объект юзера.
        /// </summary>
        /// <param name="needUserUpdate">Исходный объект юзера для изменения.</param>
        private async Task ChangeProfileData(UserEntity needUserUpdate, string userName)
        {
            UserEntity oldUser = await _postgre.Users
                    .Where(u => u.UserName.Equals(userName))
                    .FirstOrDefaultAsync();

            // Изменяет некоторые поля.
            oldUser.LastName = needUserUpdate.LastName;
            oldUser.FirstName = needUserUpdate.FirstName;
            oldUser.Patronymic = needUserUpdate.Patronymic;
            oldUser.Email = needUserUpdate.Email;
            oldUser.City = needUserUpdate.City;
            oldUser.Gender = needUserUpdate.Gender;
        }

        /// <summary>
        /// Метод генерит токен юзеру.
        /// </summary>
        /// <param name="claimsIdentity">Объект полномочий.</param>
        /// <returns>Строку токена.</returns>
        public Task<string> GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Task.FromResult(encodedJwt);
        }


        /// <summary>
        /// Метод обновит токен юзеру.
        /// </summary>
        /// <param name="claimsIdentity">Объект полномочий.</param>
        /// <returns>Строку токена.</returns>
        public Task<string> GenerateToken(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException();
                }

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: new ClaimsIdentity().Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Task.FromResult(encodedJwt);
            }

            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Не передано имя пользователя {ex.Message}");
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод находит юзера по его логину.
        /// </summary>
        /// <param name="userName">Логин юзера.</param>
        /// <returns>Объект с данными юзера.</returns>
        public async Task<UserEntity> GetUserByLogin(string userName)
        {
            // Находит юзера, чтобы проставить ему успешное прохождение теста.
            UserEntity user = await _postgre.Users
                .Where(u => u.UserName
                .Equals(userName))
                .FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Метод находит Id пользователя по его логину.
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Id пользователя.</returns>
        public async Task<string> GetUserIdByLogin(string userName)
        {
            string userId = await _postgre.Users
                .Where(u => u.UserName
                .Equals(userName))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return userId;
        }


        /// <summary>
        /// Метод находит логин пользователя по его Id.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Логин пользователя.</returns>
        public async Task<string> FindUserIdByLogin(string userId)
        {
            string userLogin = await _postgre.Users
                .Where(u => u.Id
                .Equals(userId))
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

            return userLogin;
        }

        /// <summary>
        /// Метод находит фамилию, имя, фото профиля пользователя по его Id.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Фамилия, имя, фото профиля пользователя.</returns>
        public async Task<UserOutput> GetUserInitialsByIdAsync(string userId)
        {
            UserOutput user = await _postgre.Users
                .Where(u => u.Id
                .Equals(userId))
                .Select(res => new UserOutput { 
                    FirstName = res.FirstName, 
                    LastName = res.LastName,
                    UserIcon = res.UserIcon,
                    UserRole = res.UserRole,
                    UserName = res.UserName
                })
                .FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Метод получит логин и иконку профиля заказчика по Id его задания.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <returns>Данные заказчика.</returns>
        public async Task<CustomerOutput> GetCustomerLoginByTaskId(int? taskId)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new NotFoundTaskIdException(taskId);
                }

                // Получит Id заказчика, который создал задание.
                string customerId = await _postgre.Tasks
                    .Where(t => t.TaskId == taskId)
                    .Select(t => t.OwnerId)
                    .FirstOrDefaultAsync();

                // Если заказчика задания с таким OwnerId не найдено.
                if (string.IsNullOrEmpty(customerId))
                {
                    throw new NotFoundTaskCustomerIdException(customerId);
                }

                // Найдет логин и иконку профиля заказчика задания и мапит к типу CustomerOutput.
                MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<UserEntity, CustomerOutput>());
                Mapper mapper = new Mapper(config);
                CustomerOutput customer = mapper.Map<CustomerOutput>(await _postgre.Users
                    .Where(u => u.Id
                    .Equals(customerId))
                    .FirstOrDefaultAsync());

                // Если логина заказчика задания не найдено.
                if (!string.IsNullOrEmpty(customer.UserName))
                {
                    // Если у заказчика не установлена иконка профиля, то запишет ее по дефолту.
                    if (string.IsNullOrEmpty(customer.UserIcon))
                    {
                        customer.UserIcon = NoPhotoUrl.NO_PHOTO;
                    }

                    return customer;
                }

                throw new NotFoundCustomerLoginException();
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                _ = _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод находит последнего добавленного пользователя и берет его Id.
        /// </summary>
        /// <returns>Id последнего пользователя.</returns>
        public async Task<string> GetLastUserAsync()
        {
            try
            {
                List<UserEntity> users = await _postgre.Users.ToListAsync();

                if (users.Count <= 0)
                {
                    throw new EmptyTableUserException();
                }

                users.Reverse();
                string userId = users.Select(u => u.Id).FirstOrDefault();

                return userId;

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger _logger = new Logger(_db, e.GetType().FullName, e.Message.ToString(), e.StackTrace);
                _ = _logger.LogCritical();
                throw;
            }
        }
    }
}
