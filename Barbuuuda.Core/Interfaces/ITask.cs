using Barbuuuda.Models.Task;
using System.Collections;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Интерфейс содержит логику работы с заданиями.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <param name="userName">Login юзера.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        Task<TaskEntity> CreateTask(TaskEntity oTask, string userName);

        /// <summary>
        /// Метод редактирует задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        Task<TaskEntity> EditTask(TaskEntity oTask, string userName);

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        Task<IList> GetTaskCategories();

        /// <summary>
        /// Метод получает список заданий заказчика.
        /// </summary>
        /// <param name="userName">Логин заказчика.</param>
        /// <param name="taskId">Id задания.</param>
        /// <param name="type">Параметр получения заданий либо все либо одно.</param>
        /// <returns>Коллекция заданий.</returns>
        Task<IList> GetTasksList(string userName, int? taskId, string type);

        /// <summary>
        /// Метод удаляет задание.
        /// </summary>
        /// <param name="taskId">Id задачи.</param>
        Task DeleteTask(int taskId);

        /// <summary>
        /// Метод фильтрует задания заказчика по параметру.
        /// </summary>
        /// <param name="query">Параметр фильтрации.</param>
        /// <returns>Отфильтрованные данные.</returns>
        Task<IList> FilterTask(string query);

        /// <summary>
        /// Метод ищет задание по Id или названию.
        /// </summary>
        /// <param name="param">Поисковый параметр.</param>
        /// <returns>Результат поиска.</returns>
        Task<IList> SearchTask(string param);

        /// <summary>
        /// Метод ищет задания указанной даты.
        /// </summary>
        /// <param name="date">Параметр даты.</param>
        /// <returns>Найденные задания.</returns>
        Task<IList> GetSearchTaskDate(string date);

        /// <summary>
        /// Метод выгружает активные задания заказчика.
        /// </summary>
        /// <returns>Список активных заданий.</returns>
        Task<IList> LoadActiveTasks(string userName);

        /// <summary>
        /// Метод получает кол-во задач определенного статуса.
        /// </summary>
        /// <param name="status">Имя статуса, кол-во задач которых нужно получить.</param>
        /// <returns>Число кол-ва задач.</returns>
        Task<object> GetCountTaskStatuses();

        /// <summary>
        /// Метод получает задания определенного статуса.
        /// </summary>
        /// <param name="status">Название статуса.</param>
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Список заданий с определенным статусом.</returns>
        Task<IList> GetStatusTasks(string status, string userName);

        /// <summary>
        /// Метод получает кол-во заданий всего.
        /// </summary>
        /// <param name="userName">Login пользователя.</param>
        /// <returns>Кол-во заданий.</returns>
        Task<int?> GetTotalCountTasks(string userName);

        /// <summary>
        /// Метод получает список заданий в аукционе. Выводит задания в статусе "В аукционе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        Task<object> LoadAuctionTasks();

        /// <summary>
        /// Метод получает логин юзера по его Id.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <returns>Логин юзера.</returns>
        Task<string> GetUserLoginById(string userId);


        /// <summary>
        /// Метод получает Id юзера по его Login.
        /// </summary>
        /// <param name="userId">Id юзера.</param>
        /// <returns>Id юзера.</returns>
        Task<string> GetUserByName(string userName);
    }
}
