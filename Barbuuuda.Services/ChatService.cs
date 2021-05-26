using AutoMapper;
using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Enums;
using Barbuuuda.Core.Exceptions;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Core.Logger;
using Barbuuuda.Models.Chat.Output;
using Barbuuuda.Models.Entities.Chat;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Output;
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
    public sealed class ChatService : IChatService
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;

        /// <summary>
        /// Абстракция пользователя.
        /// </summary>
        private readonly IUserService _user;

        public ChatService(ApplicationDbContext db, PostgreDbContext postgre, IUserService user)
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
        public async Task<GetResultMessageOutput> SendAsync(string message, string account, long dialogId)
        {
            try
            {
                GetResultMessageOutput messagesList = new GetResultMessageOutput();

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

                // Приведет к типу MessageOutput.
                foreach (object messageText in messages)
                {
                    string jsonString = JsonSerializer.Serialize(messageText);
                    MessageOutput messageOutput = JsonSerializer.Deserialize<MessageOutput>(jsonString);

                    // Проставит флаг принадлежности сообщения.
                    messageOutput.IsMyMessage = messageOutput.UserId.Equals(userId);

                    // Затирает Id пользователя, чтобы фронт не видел.
                    messageOutput.UserId = null;

                    messagesList.Messages.Add(messageOutput);
                }
                messagesList.DialogState = DialogStateEnum.Open.ToString();                

                return messagesList;
            }

            catch (Exception ex)
            {
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message, ex.StackTrace);
                await logger.LogCritical();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="dialogId">Id диалога, для которого нужно подтянуть сообщения.</param>
        /// <param name="account">Логин текущего пользователя.</param>
        /// <param name="isWriteBtn">Флаг кнопки "Написать".</param>
        /// <param name="executorId">Id исполнителя, на ставку которого нажали.</param>
        /// <returns>Список сообщений диалога.</returns>
        public async Task<GetResultMessageOutput> GetDialogAsync(long? dialogId, string account, string executorId, bool isWriteBtn = false)
        {
            try
            {
                GetResultMessageOutput messagesList = new GetResultMessageOutput();

                // Если dialogId не передан и не передан флаг кнопки, значит нужно открыть пустой чат.
                if (dialogId == null && !isWriteBtn)
                {
                    messagesList.DialogState = DialogStateEnum.None.ToString();

                    return messagesList;
                }

                // Найдет Id текущего пользователя.
                string userId = await _user.GetUserIdByLogin(account);

                // Если передан флаг кнопки "Ответить", значит нужно поискать существующий диалог с исполнителем или создать новый.
                if (isWriteBtn)
                {
                    // Есть ли роль заказчика.
                    UserOutput user = await _user.GetUserInitialsByIdAsync(userId);

                    // Пытается найти существующий диалог заказчика с исполнителем.
                    if (user.UserRole.Equals(UserRole.CUSTOMER) && !string.IsNullOrEmpty(executorId))
                    {
                        // Ищет Id диалога с текущим заказчиком и исполнителем, на ставку которого нажали. Затем сравнит их DialogId, если он совпадает, значит заказчик с исполнителем общаются.
                        // Выберет DialogId заказчика.
                        long customerDialogId = await _postgre.DialogMembers
                            .Where(d => d.Id
                            .Equals(userId))
                            .Select(res => res.DialogId)
                            .FirstOrDefaultAsync();

                        // Выберет DialogId исполнителя.
                        long executorDialogId = await _postgre.DialogMembers
                            .Where(d => d.Id
                            .Equals(executorId))
                            .Select(res => res.DialogId)
                            .FirstOrDefaultAsync();

                        // Сравнит DialogId заказчика и исполнителя. Если они равны, значит заказчик и исполнитель общаются в одном чате и возьмет DialogId этого чата.
                        if (customerDialogId != executorDialogId)
                        {
                            // Создаст новый диалог.
                            await _postgre.MainInfoDialogs.AddAsync(new MainInfoDialogEntity()
                            {
                                DialogName = string.Empty,
                                Created = DateTime.Now
                            });

                            await _postgre.SaveChangesAsync();

                            // Выберет DialogId созданного диалога.
                            long newDialogId = await _postgre.MainInfoDialogs
                                .OrderBy(d => d.DialogId)
                                .Select(d => d.DialogId)
                                .LastOrDefaultAsync();

                            // Добавит заказчика и исполнителя к новому диалогу.
                            await _postgre.DialogMembers.AddRangeAsync(
                                new DialogMemberEntity()
                                {
                                    DialogId = newDialogId,
                                    Id = userId,
                                    Joined = DateTime.Now
                                },
                                new DialogMemberEntity()
                                {
                                    DialogId = newDialogId,
                                    Id = executorId,
                                    Joined = DateTime.Now
                                });

                            await _postgre.SaveChangesAsync();

                            messagesList.DialogState = DialogStateEnum.Open.ToString();

                            return messagesList;
                        }

                        dialogId = executorDialogId;
                    }
                }               

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

                // Приведет к типу MessageOutput.
                foreach (object message in messages)
                {
                    string jsonString = JsonSerializer.Serialize(message);
                    MessageOutput messageOutput = JsonSerializer.Deserialize<MessageOutput>(jsonString);

                    // Проставит флаг принадлежности сообщения.
                    messageOutput.IsMyMessage = messageOutput.UserId.Equals(userId);

                    // Затирает Id пользователя, чтобы фронт его не видел.
                    messageOutput.UserId = null;
                    messagesList.Messages.Add(messageOutput);
                }
                messagesList.DialogState = DialogStateEnum.Open.ToString();

                // Находит Id участников диалога по DialogId.
                IEnumerable<string> membersIds = await GetDialogMembers(dialogId);

                if (membersIds == null)
                {
                    throw new NotDialogMembersException(dialogId);
                }

                string id = membersIds.FirstOrDefault(i => !i.Equals(userId));
                UserOutput otherUser = await _user.GetUserInitialsByIdAsync(id);

                // Запишет имя и фамилию пользователя, диалог с которым открыт.
                if (!string.IsNullOrEmpty(otherUser.FirstName) && !string.IsNullOrEmpty(otherUser.LastName))
                {
                    messagesList.FirstName = otherUser.FirstName;
                    messagesList.LastName = CommonMethodsService.SubstringLastName(otherUser.LastName); 
                }

                // Если не заполнено имя и фамилия, значит записать логин.
                else
                {
                    messagesList.UserName = otherUser.UserName;
                }

                return messagesList;
            }

            catch (Exception ex)
            {
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message, ex.StackTrace);
                await logger.LogCritical();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Список диалогов.</returns>
        public async Task<GetResultDialogOutput> GetDialogsAsync(string account)
        {
            try
            {
                GetResultDialogOutput dialogsList = new GetResultDialogOutput();
                List<DialogOutput> distinctDialogs = new List<DialogOutput>();

                // Находит Id пользователя, для которого подтянуть список диалогов и мапит к типу UserOutput.
                MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<UserEntity, UserOutput>());
                Mapper mapper = new Mapper(config);
                UserOutput user = mapper.Map<UserOutput>(await _user.GetUserByLogin(account));

                if (string.IsNullOrEmpty(user.Id))
                {
                    throw new NotFoundUserException(account);
                }

                // Выберет список диалогов.
                var dialogs = await _postgre.DialogMembers
                    .Join(_postgre.MainInfoDialogs, member => member.DialogId, info => info.DialogId, (member, info) => new { member, info })
                    .Select(res => new
                    {
                        res.info.DialogId,
                        res.info.DialogName,
                        res.member.Id,
                        res.info.Created,
                        res.member.User.UserName,
                        res.member.User.UserRole,
                        UserIcon = res.member.User.UserIcon ?? NoPhotoUrl.NO_PHOTO
                    })
                    .ToListAsync();

                // Если диалоги не найдены, то вернет пустой массив.
                if (!dialogs.Any())
                {
                    return dialogsList;
                }

                // Находит и убирает дубли.
                int i = 0;
                foreach (object dialog in dialogs)
                {
                    string jsonString = JsonSerializer.Serialize(dialog);
                    DialogOutput resultDialog = JsonSerializer.Deserialize<DialogOutput>(jsonString);
                    resultDialog.UserId ??= string.Empty;

                    if (distinctDialogs.Count == 0 && !resultDialog.UserName.Equals(account))
                    {
                        distinctDialogs.Add(resultDialog);
                    }

                    List<DialogOutput> dublicate = distinctDialogs
                        .Where(u => u.UserId
                        .Equals(dialogs[i].Id))
                        .ToList();

                    if (dublicate.Count != 0 || resultDialog.UserName.Equals(account))
                    {
                        i++;
                        continue;
                    }

                    distinctDialogs.Add(resultDialog);
                    i++;
                }

                switch (user.UserRole)
                {
                    // Диалог просматривает исполнитель, значит нужно удалить из массива других исполнителей.
                    case UserRole.EXECUTOR:
                    {
                        DialogOutput itemToRemove = distinctDialogs
                            .FirstOrDefault(r => r.UserRole
                            .Equals(UserRole.EXECUTOR));

                        if (itemToRemove != null)
                        {
                            distinctDialogs.Remove(itemToRemove);
                        }

                        break;
                    }

                    // Диалог просматривает заказчик, значит нужно удалить из массива других заказчиков.
                    case UserRole.CUSTOMER:
                    {
                        DialogOutput itemToRemove = distinctDialogs
                            .FirstOrDefault(r => r.UserRole
                            .Equals(UserRole.CUSTOMER));

                        if (itemToRemove != null)
                        {
                            distinctDialogs.Remove(itemToRemove);
                        }

                        break;
                    }
                }

                foreach (object dialog in distinctDialogs)
                {
                    string jsonString = JsonSerializer.Serialize(dialog);
                    DialogOutput resultDialog = JsonSerializer.Deserialize<DialogOutput>(jsonString);
                    
                    // Подтянет последнее сообщение диалога для отображения в свернутом виде взяв первые 40 символов и далее ставит ...
                    resultDialog.LastMessage = await _postgre.DialogMessages
                        .Where(d => d.DialogId == resultDialog.DialogId)
                        .OrderBy(o => o.Created)
                        .Select(m => m.Message.Length > 40 ? string.Concat(m.Message.Substring(0, 40), "...") : m.Message)
                        .LastOrDefaultAsync();

                    // Находит Id участников диалога по DialogId.
                    IEnumerable<string> membersIds = await GetDialogMembers(resultDialog.DialogId);
                    string executorId = string.Empty;

                    if (membersIds == null)
                    {
                        throw new NotDialogMembersException(resultDialog.DialogId);
                    }

                    // Запишет логин собеседника.
                    foreach (string id in membersIds.Where(id => !id.Equals(user.Id)))
                    {
                        resultDialog.UserName = await _user.FindUserIdByLogin(id);

                        // Запишет имя и фамилию, если они заполнены, иначе фронт будет использовать логин собеседника.
                        UserOutput userInitial = await _user.GetUserInitialsByIdAsync(id);

                        if (string.IsNullOrEmpty(userInitial.FirstName) || string.IsNullOrEmpty(userInitial.LastName))
                        {
                            continue;
                        }

                        resultDialog.FirstName = userInitial.FirstName;

                        // Возьмет первую букву фамилии и поставит после нее точку.
                        resultDialog.LastName = CommonMethodsService.SubstringLastName(userInitial.LastName);

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
                Logger logger = new Logger(_db, ex.GetType().FullName, ex.Message, ex.StackTrace);
                await logger.LogCritical();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод найдет список Id участников диалога по DialogId.
        /// </summary>
        /// <param name="dialogId">Id диалога, участников которого нужно найти.</param>
        /// <returns>Список Id участников диалога.</returns>
        private async Task<IEnumerable<string>> GetDialogMembers(long? dialogId)
        {
            return await _postgre.DialogMembers
                .Where(d => d.DialogId == dialogId)
                .Select(res => res.Id)
                .ToListAsync();
        }
    }
}
