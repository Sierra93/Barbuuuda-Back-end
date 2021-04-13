using Barbuuuda.Models.Chat.Outpoot;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция чата.
    /// </summary>
    public interface IChat
    {
        /// <summary>
        /// Метод пишет сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns></returns>
        Task SendAsync(string message, string lastName, string firstName, string account);

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="userId">Id пользователя, для которого нужно подтянуть диалог.</param>
        /// <returns></returns>
        Task GetDialogAsync(string userId);

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Список диалогов.</returns>
        Task<GetResultDialogOutpoot> GetDialogsAsync(string account);
    }
}
