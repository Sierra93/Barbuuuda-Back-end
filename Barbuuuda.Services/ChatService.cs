using AutoMapper;
using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Enums;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Chat.Outpoot;
using Barbuuuda.Models.Entities.Chat;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Outpoot;
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
        /// Абстракция пользователя.
        /// </summary>
        private readonly IUser _user;

        public ChatService(ApplicationDbContext db, PostgreDbContext postgre, IUser user)
        {
            _db = db;
            _postgre = postgre;
            _user = user;
        }


        /// <summary>
        /// Метод пишет сообщение.
        /// </summary>
        /// <param name="account">Логин пользователя.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="dialogId">Id диалога.</param>
        /// <returns>Список сообщений.</returns>
        public async Task<GetResultMessageOutpoot> SendAsync(string message, string account, long dialogId)
        {
            try
            {
                GetResultMessageOutpoot messagesList = new GetResultMessageOutpoot();

                // Если сообщения не передано, то ничего не делать.
                if (string.IsNullOrEmpty(message))
                {
                    return null;
                }

                // Найдет Id пользователя.
                string userId = await _user.GetUserIdByLogin(account);

                // Проверит существование диалога.
                bool isDialog = await _postgre.MainInfoDialogs
                    .Where(d => d.DialogId == dialogId)
                    .FirstOrDefaultAsync() != null;

                if (!isDialog)
                {
                    throw new NotFoundDialogIdException(dialogId);
                }

                // Запишет сообщение в диалог.
                await _postgre.DialogMessages.AddAsync(new DialogMessageEntity()
                {
                    Message = message,
                    DialogId = dialogId,
                    Created = DateTime.Now,
                    UserId = userId,
                    IsMyMessage = true
                });

                await _postgre.SaveChangesAsync();

                // Получит сообщения диалога.
                var messages = await (_postgre.DialogMessages
                        .Where(d => d.DialogId == dialogId)
                        .OrderBy(m => m.Created)
                        .Select(res => new
                        {
                            dialogId = res.DialogId,
                            message = res.Message,
                            created = string.Format("{0:f}", res.Created),
                            userId = res.UserId,
                            isMyMessage = res.IsMyMessage
                        })
                        .ToListAsync());

                // Приведет к типу MessageOutpoot.
                foreach (object messageText in messages)
                {
                    string jsonString = JsonSerializer.Serialize(messageText);
                    MessageOutpoot messageOutpoot = JsonSerializer.Deserialize<MessageOutpoot>(jsonString);

                    // Проставит флаг принадлежности сообщения.
                    messageOutpoot.IsMyMessage = messageOutpoot.UserId.Equals(userId) ? true : false;

                    // Затирает Id пользователя, чтобы фронт не видел.
                    messageOutpoot.UserId = null;

                    messagesList.Messages.Add(messageOutpoot);
                }
                messagesList.DialogState = DialogStateEnum.Open.ToString();                

                return messagesList;
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="dialogId">Id диалога, для которого нужно подтянуть сообщения.</param>
        /// <param name="account">Логин текущего пользователя.</param>
        /// <returns>Список сообщений.</returns>
        public async Task<GetResultMessageOutpoot> GetDialogAsync(long? dialogId, string account)
        {
            try
            {
                GetResultMessageOutpoot messagesList = new GetResultMessageOutpoot();

                // Если dialogId не передан, значит нужно открыть пустой чат.
                if (dialogId == null)
                {
                    messagesList.DialogState = DialogStateEnum.None.ToString();

                    //TODO: доработать создание нового диалога.

                    return messagesList;
                }

                // Найдет Id пользователя.
                string userId = await _user.GetUserIdByLogin(account);

                // Проверит существование диалога.
                bool isDialog = await _postgre.MainInfoDialogs
                    .Where(d => d.DialogId == dialogId)
                    .FirstOrDefaultAsync() != null;

                if (!isDialog)
                {
                    throw new NotFoundDialogIdException(dialogId);
                }

                // Получит сообщения диалога.
                var messages = await (_postgre.DialogMessages
                        .Where(d => d.DialogId == dialogId)
                        .OrderBy(m => m.Created)
                        .Select(res => new
                        {
                            dialogId = res.DialogId,
                            message = res.Message,
                            created = string.Format("{0:f}", res.Created),
                            userId = res.UserId,
                            isMyMessage = res.IsMyMessage
                        })
                        .ToListAsync());

                // Если у диалога нет сообщений, значит вернуть пустой диалог, который будет открыт.
                if (!messages.Any())
                {
                    messagesList.DialogState = DialogStateEnum.Empty.ToString();

                    return messagesList;
                }

                // Приведет к типу MessageOutpoot.
                foreach (object message in messages)
                {
                    string jsonString = JsonSerializer.Serialize(message);
                    MessageOutpoot messageOutpoot = JsonSerializer.Deserialize<MessageOutpoot>(jsonString);

                    // Проставит флаг принадлежности сообщения.
                    messageOutpoot.IsMyMessage = messageOutpoot.UserId.Equals(userId) ? true : false;

                    // Затирает Id пользователя, чтобы фронт не видел.
                    messageOutpoot.UserId = null;

                    messagesList.Messages.Add(messageOutpoot);
                }
                messagesList.DialogState = DialogStateEnum.Open.ToString();

                return messagesList;
            }

            catch (Exception ex)
            {
                Logger _logger = new Logger(_db, ex.GetType().FullName, ex.Message.ToString(), ex.StackTrace);
                await _logger.LogCritical();
                throw new Exception(ex.Message.ToString());
            }
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
                        .Join(_postgre.MainInfoDialogs, parentMember => parentMember.member.DialogId, mainInfoDialog => mainInfoDialog.DialogId, (parentMember, mainInfoDialog) => new { parentMember, mainInfoDialog })
                        .Where(d => d.parentMember.member.Id.Equals(user.Id))
                        .Select(res => new
                        {
                            res.parentMember.info.DialogId,
                            res.parentMember.info.DialogName,
                            res.parentMember.member.Id,
                            res.mainInfoDialog.Created
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
                        .OrderBy(o => o.Created)
                        .Select(m => m.Message.Length > 40 ? string.Concat(m.Message.Substring(0, 40), "...") : m.Message)
                        .LastOrDefaultAsync();

                    // Находит Id участников диалога по DialogId.
                    IEnumerable<string> membersIds = await _postgre.DialogMembers
                        .Where(d => d.DialogId == resultDialog.DialogId)
                        .Select(res => res.Id)
                        .ToListAsync();
                    string executorId = string.Empty;

                    //// Запишет логин собеседника.
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

                    // Если дата диалога совпадает с сегодняшней, то заполнит часы и минуты, иначе оставит их null.
                    if (DateTime.Now.ToString("d").Equals(Convert.ToDateTime(resultDialog.Created).ToString("d")))
                    {
                        // Запишет только часы и минуты.
                        resultDialog.CalcTime = Convert.ToDateTime(resultDialog.Created).ToString("t");
                    }

                    // Если дата диалога не совпадает с сегодняшней.
                    else if (!DateTime.Now.ToString("d").Equals(Convert.ToDateTime(resultDialog.Created).ToString("d")))
                    {
                        // Запишет только дату.
                        resultDialog.CalcShortDate = Convert.ToDateTime(resultDialog.Created).ToString("d");
                    }

                    // Форматирует дату убрав секунды.
                    resultDialog.Created = Convert.ToDateTime(resultDialog.Created).ToString("g");
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
