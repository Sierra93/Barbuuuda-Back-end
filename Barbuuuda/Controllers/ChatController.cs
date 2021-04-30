﻿using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Chat.Input;
using Barbuuuda.Models.Chat.Outpoot;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер чата.
    /// </summary>
    [ApiController, Route("chat")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatController : BaseController
    {
        public static string Module => "Barbuuuda.Chat";

        /// <summary>
        /// Абстракция чата.
        /// </summary>
        private readonly IChat _chat;

        public ChatController(IChat chat) : base(Module)
        {
            _chat = chat;
        }

        /// <summary>
        /// Метод отправит сообщение.
        /// </summary>
        /// <param name="chatInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("send")]
        [ProducesResponseType(200, Type = typeof(GetResultMessageOutpoot))]
        public async Task<IActionResult> SendAsync([FromBody] ChatInput chatInput)
        {
            GetResultMessageOutpoot messages = await _chat.SendAsync(chatInput.Message, GetUserName(), chatInput.DialogId);

            return Ok(messages);
        }

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="dialogInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("dialog")]
        [ProducesResponseType(200, Type = typeof(GetResultMessageOutpoot))]
        public async Task<IActionResult> GetDialogAsync([FromBody] DialogInput dialogInput)
        {
            GetResultMessageOutpoot messages = await _chat.GetDialogAsync(dialogInput.DialogId, GetUserName());

            return Ok(messages);
        }

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <returns>Список диалогов.</returns>
        [HttpPost, Route("dialogs")]
        [ProducesResponseType(200, Type = typeof(GetResultDialogOutpoot))]
        public async Task<IActionResult> GetDialogsAsync()
        {
            GetResultDialogOutpoot dialogs = await _chat.GetDialogsAsync(GetUserName());

            return Ok(dialogs);
        }
    }
}