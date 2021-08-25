using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Chat.Input;
using Barbuuuda.Models.Chat.Output;
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
        /// <summary>
        /// Абстракция чата.
        /// </summary>
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Метод отправит сообщение.
        /// </summary>
        /// <param name="chatInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("send")]
        [ProducesResponseType(200, Type = typeof(GetResultMessageOutput))]
        public async Task<IActionResult> SendAsync([FromBody] ChatInput chatInput)
        {
            GetResultMessageOutput messages = await _chatService.SendAsync(chatInput.Message, GetUserName(), chatInput.DialogId);

            return Ok(messages);
        }

        /// <summary>
        /// Метод получает диалог, либо создает новый.
        /// </summary>
        /// <param name="dialogInput">Входная модель.</param>
        /// <returns></returns>
        [HttpPost, Route("dialog")]
        [ProducesResponseType(200, Type = typeof(GetResultMessageOutput))]
        public async Task<IActionResult> GetDialogAsync([FromBody] DialogInput dialogInput)
        {
            GetResultMessageOutput messages = await _chatService.GetDialogAsync(dialogInput.DialogId, GetUserName(), dialogInput.ExecutorId, dialogInput.IsWriteBtn);

            return Ok(messages);
        }

        /// <summary>
        /// Метод получает список диалогов с текущим пользователем.
        /// </summary>
        /// <returns>Список диалогов.</returns>
        [HttpPost, Route("dialogs")]
        [ProducesResponseType(200, Type = typeof(GetResultDialogOutput))]
        public async Task<IActionResult> GetDialogsAsync()
        {
            GetResultDialogOutput dialogs = await _chatService.GetDialogsAsync(GetUserName());

            return Ok(dialogs);
        }
    }
}
