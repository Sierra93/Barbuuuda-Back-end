using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция чата.
    /// </summary>
    public interface IChat
    {
        Task SendAsync(string message);
    }
}
