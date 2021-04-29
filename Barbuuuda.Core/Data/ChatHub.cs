using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Barbuuuda.Core.Data
{
    /// <summary>
    /// Класс хаба для работы с SignalR.
    /// </summary>
    [Authorize]
    public class ChatHub : Hub { }
}
