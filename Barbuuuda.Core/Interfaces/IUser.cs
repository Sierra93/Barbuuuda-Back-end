using Barbuuuda.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
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
        Task<object> LoginAsync(UserEntity user);

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
        Task SaveProfileData(UserEntity user);
    }
}
