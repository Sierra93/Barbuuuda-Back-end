using AutoMapper;
using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Chat.Outpoot;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Outpoot;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
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

                // Находит Id пользователя, для которого подтянуть список диалогов и мапит к типу UserOutpoot.
                MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<UserEntity, UserOutpoot>());
                Mapper mapper = new Mapper(config);
                UserOutpoot user = mapper.Map<UserOutpoot>(await _user.GetUserByLogin(account));

                if (string.IsNullOrEmpty(user.Id))
                {
                    throw new NotFoundUserException(account);
                }

                // Выберет список диалогов.
                var dialogs = await _postgre.DialogMembers
                        .Join(_postgre.MainInfoDialogs, member => member.DialogId, info => info.DialogId, (member, info) => new { member, info })
                        .Where(d => d.member.Id.Equals(user.Id))
                        .Select(res => new
                        {
                            res.info.DialogId,
                            res.info.DialogName,
                            res.member.Id
                        })
                        .ToListAsync();

                // Если диалоги не найдены, то вернет пустой массив.
                if (!dialogs.Any())
                {
                    return dialogsList;
                }

                foreach (object dialog in dialogs)
                {
                    string jsonString = JsonSerializer.Serialize(dialog);
                    DialogOutpoot resultDialog = JsonSerializer.Deserialize<DialogOutpoot>(jsonString);

                    // Подтянет последнее сообщение диалога для отображения в свернутом виде взяв первые 40 символов и далее ставит ...
                    resultDialog.LastMessage = await _postgre.DialogMessages
                        .Where(d => d.DialogId == resultDialog.DialogId)
                        .OrderBy(o => o.DialogId)
                        .Select(m => m.Message.Length > 40 ? string.Concat(m.Message.Substring(0, 40), "...") : m.Message)
                        .LastOrDefaultAsync();

                    // Находит Id участников диалога по DialogId.
                    IEnumerable<string> membersIds = await _postgre.DialogMembers
                        .Where(d => d.DialogId == resultDialog.DialogId)
                        .Select(res => res.Id)
                        .ToListAsync();
                    string executorId = string.Empty;

                    // Запишет логин собеседника.
                    foreach (string id in membersIds.Where(id => !id.Equals(user.Id)))
                    {
                        resultDialog.UserName = await _user.FindUserIdByLogin(id);

                        // Запишет имя и фамилию, если они заполнены, иначе фронт будет использовать логин собеседника.
                        UserOutpoot userInitial = await _user.GetUserInitialsByIdAsync(id);

                        if (string.IsNullOrEmpty(userInitial.FirstName) || string.IsNullOrEmpty(userInitial.LastName))
                        {
                            continue;
                        }

                        resultDialog.FirstName = userInitial.FirstName;

                        // Возьмет первую букву фамилии и поставит после нее точку.
                        resultDialog.LastName = string.Concat(userInitial.LastName.Substring(0, 1), ".");

                        // Проставит фото профиля или фото по дефолту.
                        resultDialog.UserIcon = userInitial.UserIcon ?? NoPhotoUrl.NO_PHOTO;
                    }

                    // Находит исполнителя в диалоге.
                    foreach (string id in membersIds)
                    {
                        var dialogUser = await _postgre.Users
                            .Where(u => u.Id.Equals(id))
                            .Select(u => new 
                            {
                                u.UserRole,
                                u.Id
                            })
                            .FirstOrDefaultAsync();
                        
                        // Пропустит, если не исполнитель.
                        if (!dialogUser.UserRole.Equals(UserRole.EXECUTOR))
                        {                            
                            continue;
                        }

                        executorId = dialogUser.Id;
                    }

                    // Исполнитель диалога найден, теперь подтянуть его ставку.
                    resultDialog.Price = await _postgre.Responds
                        .Where(r => r.ExecutorId.Equals(executorId))
                        .Select(res => string.Format("{0:0,0}", res.Price))
                        .FirstOrDefaultAsync();                    
                    dialogsList.Dialogs.Add(resultDialog);
                }

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
