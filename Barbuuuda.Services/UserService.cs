using Barbuuuda.Core.Data;
using Barbuuuda.Core.Extensions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Services {
    /// <summary>
    /// Сервис реализует методы пользователя.
    /// </summary>
    public class UserService : IUser {
        ApplicationDbContext _db;
        PostgreDbContext _postgre;

        public UserService(ApplicationDbContext db, PostgreDbContext postgre) {
            _db = db;
            _postgre = postgre;
        }

        /// <summary>
        /// Метод создает нового пользователя.
        /// </summary>
        /// <param name="user">Объект с данными регистрации пользователя.</param>
        public async Task<UserDto> Create(UserDto user) {
            try {
                bool bFields = CheckUserFields(user);

                if (!bFields) {
                    throw new ArgumentNullException();
                }

                // Проверяет существование пользователя.
                bool isUser = await IdentityUser(user.UserLogin, user.UserEmail, user.UserPhone);

                if (!isUser) {
                    // Хэширует пароль в MD5.
                    string hashPass = HashMD5.HashPassword(user.UserPassword);
                    user.UserPassword = hashPass;

                    await _postgre.Users.AddAsync(user);
                    await _postgre.SaveChangesAsync();

                    return user;
                }

                throw new ArgumentException();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Не все поля заполнены", ex.Message.ToString());
            }

            catch (ArgumentException ex) {
                throw new ArgumentException("Такой пользователь уже существует", ex.Message.ToString());
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод ищет пользователя в БД. Если существует, то не дает создать.
        /// </summary>
        /// <returns>Статус true/false</returns>
        async Task<bool> IdentityUser(string login, string email, string phone) {
            // Ищет по логину.
            UserDto isUserLogin =  await _postgre.Users.Where(u => u.UserLogin.Equals(login)).FirstOrDefaultAsync();

            // Ищет по email.
            UserDto isUserEmail = await _postgre.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefaultAsync();

            // Ищет по телефону.
            UserDto isUserPhone = await _postgre.Users.Where(u => u.UserPhone.Equals(phone)).FirstOrDefaultAsync();

            if (isUserLogin != null || isUserEmail != null || isUserPhone != null) {
                return true;
            }

            return false;
        }

        async Task<bool> IdentityUser(string email) {
            // Ищет по email.
            UserDto isUserEmail = await _postgre.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefaultAsync();

            if (isUserEmail != null) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        /// <param name="user">Объект данных юзера.</param>
        /// <returns>Статус true/false</returns>
        public async Task<object> Login(UserDto user) {
            try {
                ErrorExtension errorExtension = new ErrorExtension();

                if (string.IsNullOrEmpty(user.UserEmail) || string.IsNullOrEmpty(user.UserPassword)) {
                    throw new ArgumentException();
                }

                bool bUser = await IdentityUser(user.UserEmail);

                if (bUser) {
                    // Выбирает юзера из БД.
                    var oUser = GetUserDB(user.UserEmail);

                    // Хэширует пароль для сравнения.
                    string hashPassword = HashMD5.HashPassword(user.UserPassword);

                    // Выбирает пароль пользователя из БД.
                    bool getIdentityPassword = await GetUserPassword(hashPassword);

                    if (!getIdentityPassword) {
                        return errorExtension.ThrowErrorLogin();
                    }

                    if (oUser != null) {
                        var now = DateTime.UtcNow;
                        var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                            notBefore: now,
                            claims: oUser.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                        // Записывает токен юзеру.
                        UserDto updateUser = _postgre.Users.Where(u => u.UserEmail.Equals(user.UserEmail)).FirstOrDefault();
                        updateUser.Token = encodedJwt;

                        var response = new {
                            access_token = encodedJwt,
                            username = oUser.Name,
                            role = updateUser.UserType
                        };

                        _postgre.Users.Update(updateUser);
                        await _db.SaveChangesAsync();

                        return response;
                    }
                }

                return errorExtension.ThrowErrorLogin();
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Такого пользователя не существует", ex.Message.ToString());
            }

            catch (ArgumentException ex) {
                throw new ArgumentException("Параметры не могут быть пустыми", ex.Message.ToString());
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод проверяет заполненность полей.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool CheckUserFields(UserDto user) {
            if (string.IsNullOrEmpty(user.UserLogin) ||
                   string.IsNullOrEmpty(user.UserPassword) ||
                   string.IsNullOrEmpty(user.UserEmail) ||
                   string.IsNullOrEmpty(user.UserType) ||
                   string.IsNullOrEmpty(user.UserPhone)) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод выбирает пароль пользователя из БД.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<bool> GetUserPassword(string password) {
            UserDto oUser = await _postgre.Users.Where(p => p.UserPassword.Equals(password)).FirstOrDefaultAsync();

            if (oUser == null) {
                return false;
            }

            return true;
        }

        ClaimsIdentity GetUserDB(string email) {
            UserDto oUser =  _postgre.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefault();

            if (oUser != null) {
                var claims = new List<Claim> {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, email)
                    };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

        /// <summary>
        /// Метод проверяет, авторизован ли юзер, если нет, то вернет false, иначе true.
        /// </summary>
        /// <param name="login">Логин юзера.</param>
        /// <returns>true/false</returns>
        public bool Authorize(string email, ref int userId) {
            try {
                if (string.IsNullOrEmpty(email)) {
                    throw new ArgumentNullException();
                }

                UserDto oUser = _postgre.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefault();
                
                if (oUser != null) {
                    userId = oUser.UserId;
                }

                return oUser.Token != null ? true : false;
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Логин пользователя не передан", ex.Message.ToString());
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает хидер в зависимости от роли.
        /// </summary>
        /// <param name="role">Роль юзера.</param>
        /// <returns></returns>
        public IList<HeaderTypeDto> GetHeader(string role) {
            try {
                if (string.IsNullOrEmpty(role)) {
                    throw new ArgumentNullException();
                }

                return _db.Headers.Where(h => h.HeaderType.Equals(role)).ToList();
            }

            catch (IndexOutOfRangeException ex) {
                throw new IndexOutOfRangeException($"Поля этой роли не сформированы {ex.Message}");
            }

            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Роль пользователя не передана {ex.Message}");
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
