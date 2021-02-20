using Barbuuuda.Models.User;
using System.Collections;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция описывает методы по работе с исполнителями сервиса.
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// Метод выгружает список исполнителей сервиса.
        /// </summary>
        /// <returns>Список исполнителей.</returns>
        Task<IEnumerable> GetExecutorListAsync();


        /// <summary>
        /// Метод добавляет специализации исполнителя.
        /// </summary>
        /// <param name="specializations">Массив специализаций.</param>
        Task AddExecutorSpecializations(ExecutorSpecialization[] specializations, string executorName);
    }
}
