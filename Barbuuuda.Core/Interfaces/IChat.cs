using Barbuuuda.Models.Chat.Output;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция чата.
    /// </summary>
    public interface IChat
    {
        /// <summary>
        /// Метод отправит сообщение.
        /// </summary>
        /// <param name="account">Логин пользователя.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="dialogId">Id диалога.</param>
        /// <returns>Список сообщений.</returns>
        Task<GetResultMessageOutput> SendAsync(string message, string account, long dialogId);

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="dialogId">Id диалога, для которого нужно подтянуть сообщения.</param>
        /// <param name="account">Логин текущего пользователя.</param>
        /// <param name="isWriteBtn">Флаг кнопки "Написать".</param>
        /// <param name="executorId">Id исполнителя, на которого нажали при нажатии на кнопку "Написать."</param>
        /// <returns>Список сообщений.</returns>
        Task<GetResultMessageOutput> GetDialogAsync(long? dialogId, string account, string executorId, bool isWriteBtn = false);

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Список диалогов.</returns>
        Task<GetResultDialogOutput> GetDialogsAsync(string account);
    }
}
