using Barbuuuda.Models.Chat.Input;
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

        public ChatController() : base(Module)
        {
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
    }
}
