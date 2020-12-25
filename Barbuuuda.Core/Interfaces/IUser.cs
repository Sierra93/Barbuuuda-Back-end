using Barbuuuda.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces {
    /// <summary>
    /// Интерфейс определяет методы работы с пользователями.
    /// </summary>
    public interface IUser {
        /// <summary>
        /// Метод создает нового пользователя.
        /// </summary>
        /// <param name="user">Объект с данными регистрации пользователя.</param>
        Task<UserDto> Create(UserDto user);

        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        /// <param name="user">Объект данных юзера.</param>
        /// <returns>Статус true/false</returns>
        Task<object> Login(UserDto user);

        /// <summary>
        /// Метод проверяет, авторизован ли юзер, если нет, то вернет false, иначе true.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <returns>true/false</returns>
        Task<bool> Authorize(string login);

        /// <summary>
        /// Метод получает хидер в зависимости от роли.
        /// </summary>
        /// <param name="role">Роль юзера.</param>
        /// <returns></returns>
        Task<IList<HeaderTypeDto>> GetHeader(string role);
    }
}
