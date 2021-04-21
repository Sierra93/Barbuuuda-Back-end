﻿using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Input;
using Barbuuuda.Models.User.Outpoot;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Интерфейс определяет методы работы с пользователями.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        /// <param name="user">Объект данных юзера.</param>
        /// <returns>Статус true/false</returns>
        Task<object> LoginAsync(UserInput user);

        /// <summary>
        /// Метод проверяет, авторизован ли юзер.
        /// </summary>
        /// <param name="username">login юзера.</param>
        /// <returns>Объект с данными авторизованного юзера.</returns>
        Task<object> GetUserAuthorize(string username);
        /// <summary>
        /// Метод получает информацию о пользователе для профиля.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <returns>Объект с данными о профиле пользователя.</returns>
        Task<object> GetProfileInfo(string userId);

        /// <summary>
        /// Метод сохраняет личные данные юзера.
        /// </summary>
        /// <param name="user">Объект с данными юзера.</param>
        Task SaveProfileData(UserEntity user, string userName);

        /// <summary>
        /// Метод выдаст токен юзеру.
        /// </summary>
        /// <param name="claimsIdentity">Объект полномочий.</param>
        /// <returns>Строку токена.</returns>
        Task<string> GenerateToken(ClaimsIdentity claimsIdentity);

        /// <summary>
        /// Метод обновит токен юзеру.
        /// </summary>
        /// <param name="claimsIdentity">Объект полномочий.</param>
        /// <returns>Строку токена.</returns>
        Task<string> GenerateToken(string userName);

        /// <summary>
        /// Метод находит юзера по его логину.
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Объект с данными пользователя.</returns>
        Task<UserEntity> GetUserByLogin(string userName);

        /// <summary>
        /// Метод находит Id пользователя по его логину.
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Id пользователя.</returns>
        Task<string> GetUserIdByLogin(string userName);

        /// <summary>
        /// Метод находит логин пользователя по его Id.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Логин пользователя.</returns>
        Task<string> FindUserIdByLogin(string userId);

        /// <summary>
        /// Метод находит фамилию и имя пользователя по его Id.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Фамилия и имя пользователя.</returns>
        Task<UserOutpoot> GetUserInitialsByIdAsync(string userId);
    }
}
