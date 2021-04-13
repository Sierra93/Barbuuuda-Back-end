﻿using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Chat.Input;
using Barbuuuda.Models.Chat.Outpoot;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер чата.
    /// </summary>
    [ApiController, Route("chat")]
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
        /// Метод отправляет сообщение.
        /// </summary>
        /// <param name="chatInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("send")]
        public async Task<IActionResult> SendAsync([FromBody] ChatInput chatInput)
        {
            //await _hubContext.Clients.All.SendAsync("Notify", chatInput.Message);

            return Ok();
        }

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="dialogInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("dialog")]
        public async Task<IActionResult> GetDialogAsync([FromBody] DialogInput dialogInput)
        {
            return Ok();
        }

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <returns>Список диалогов.</returns>
        [HttpPost, Route("dialogs")]
        [ProducesResponseType(200, Type = typeof(GetResultDialogOutpoot))]
        public async Task<IActionResult> GetDialogsAsync()
        {
            var dialogs = await _chat.GetDialogsAsync(GetUserName());

            return Ok(dialogs);
        }
    }
}
