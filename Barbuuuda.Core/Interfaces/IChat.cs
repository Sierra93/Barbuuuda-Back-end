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
        /// <param name="dialogId">Id диалога, для которого нужно подтянуть сообщения.</param>
        /// <param name="account">Логин текущего пользователя.</param>
        /// <returns>Список сообщений.</returns>
        Task<GetResultMessageOutpoot> GetDialogAsync(long? dialogId, string account);

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Список диалогов.</returns>
        Task<GetResultDialogOutpoot> GetDialogsAsync(string account);
    }
}
