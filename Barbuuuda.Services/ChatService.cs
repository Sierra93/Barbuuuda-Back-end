using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Chat.Outpoot;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы работы с чатом.
    /// </summary>
    public sealed class ChatService : IChat
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;

        /// <summary>
        /// Абстракция хаба.
        /// </summary>
        IHubContext<ChatHub> _hubContext;

        /// <summary>
        /// Абстракция пользователя.
        /// </summary>
        private readonly IUser _user;

        public ChatService(IHubContext<ChatHub> hubContext, ApplicationDbContext db, PostgreDbContext postgre, IUser user)
        {
            _hubContext = hubContext;
            _db = db;
            _postgre = postgre;
            _user = user;
        }        

        /// <summary>
        /// Метод пишет сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns></returns>
        public Task SendAsync(string message, string lastName, string firstName, string account)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="userId">Id пользователя, для которого нужно подтянуть диалог.</param>
        /// <returns></returns>
        public Task GetDialogAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Список диалогов.</returns>
        public async Task<GetResultDialogOutpoot> GetDialogsAsync(string account)
        {
            try
            {
                GetResultDialogOutpoot dialogsList = new GetResultDialogOutpoot();

                // Находит Id пользователя, для которого подтянуть список диалогов.
                string userId = await _user.GetUserIdByLogin(account);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new NotFoundUserException(account);
                }

                // Выберет список диалогов.
                var dialogs = await _postgre.DialogMembers
                        .Join(_postgre.MainInfoDialogs, member => member.DialogId, info => info.DialogId, (member, info) => new { member, info })
                        .Where(d => d.member.Id.Equals(userId))
                        .Select(res => new
                        {
                            res.info.DialogId,
                            res.info.DialogName,
                            res.member.Id
                        })
                        .ToListAsync();

                // Если диалоги не найдены.
                if (!dialogs.Any())
                {
                    return dialogsList;
                }

                // Добавит в результирующую коллекцию GetResultDialogOutpoot.
                dialogsList.Dialogs.AddRange(from dialog in dialogs
                                             let jsonString = JsonSerializer.Serialize(dialog)
                                             let resultDialog = JsonSerializer.Deserialize<DialogOutpoot>(jsonString)
                                             select resultDialog);

                return dialogsList;
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
