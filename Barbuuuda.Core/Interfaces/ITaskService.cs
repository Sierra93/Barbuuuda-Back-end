using Barbuuuda.Models.Respond.Output;
using Barbuuuda.Models.Task.Output;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.Task.Input;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Интерфейс содержит логику работы с заданиями.
    /// </summary>
    public interface ITaskService
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
        Task<TaskEntity> EditTask(TaskInput oTask, string userName);

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
        Task<IList> GetTasksList(string userName, long? taskId, string type);

        /// <summary>
        /// Метод удаляет задание.
        /// </summary>
        /// <param name="taskId">Id задачи.</param>
        /// <returns>Статус удаления.</returns>
        Task<bool> DeleteTask(long taskId);

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
        Task<GetTaskResultOutput> GetStatusTasks(string status, string userName);

        /// <summary>
        /// Метод получает список заданий в аукционе. Выводит задания в статусе "В аукционе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        Task<GetTaskResultOutput> LoadAuctionTasks();

        /// <summary>
        /// Метод получает список ставок к заданию.
        /// </summary>
        /// <param name="taskId">Id задания, для которого нужно получить список ставок.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Список ставок.</returns>
        Task<GetRespondResultOutput> GetRespondsAsync(long taskId, string account);

        /// <summary>
        /// Метод выберет исполнителя задания.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="executorId">Id исполнителя, которого заказчик выбрал.</param>
        /// <returns>Флаг проверки оплаты.</returns>
        Task<bool> SelectAsync(long taskId, string executorId);

        /// <summary>
        /// Метод проверит оплату задания.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <returns>Флаг проверки.</returns>
        Task<bool> CheckSelectPayAsync(long taskId);

        /// <summary>
        /// Метод проверит, принял ли исполнитель в работу задание и не отказался ли от него.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Если все хорошо, то вернет список ставок к заданию, в котором будет только ставка исполнителя, которого выбрали и который принял в работу задание.</returns>
        Task<GetRespondResultOutput> CheckAcceptAndNotCancelInviteTaskAsync(long taskId, string account);

        /// <summary>
        /// Метод запишет переход к просмотру или изменению задания исполнителем.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="type">Тип перехода.</param>
        /// <param name="account">Логин пользователя</param>
        /// <returns>Id задания.</returns>
        Task<TransitionOutput> SetTransitionAsync(int taskId, string type, string account);

        /// <summary>
        /// Метод получит переход.
        /// </summary>
        /// <param name="account">Логин пользователя</param>
        /// <returns>Id задания.</returns>
        Task<TransitionOutput> GetTransitionAsync(string account);

        /// <summary>
        /// Метод получит список значений для селекта сортировки заданий.
        /// </summary>
        /// <returns>Список значений.</returns>
        Task<ControlSortResult> GetSortSelectAsync();

        /// <summary>
        /// Метод получит список значений для селекта фильтров заданий.
        /// </summary>
        /// <returns>Список значений.</returns>
        Task<ControlFilterResult> GetFilterSelectAsync();

        /// <summary>
        /// Метод найдет задание по его Id.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <returns>Найденное задание.</returns>
        Task<TaskOutput> GetTaskByIdAsync(int taskId);

        /// <summary>
        /// Метод получит код статуса по его названию.
        /// </summary>
        /// <param name="name">Название статуса.</param>
        /// <returns>Код статуса.</returns>
        Task<string> GetStatusCodeByNameAsync(string name);

        /// <summary>
        /// Метод получит список статусов для селекта фильтрации.
        /// </summary>
        /// <returns>Список статусов.</returns>
        Task<IEnumerable<TaskStatusOutput>> GetStatusesSelectAsync();

        /// <summary>
        /// Метод получит список типов заданий для селекта фильтрации.
        /// </summary>
        /// <returns>Список типов заданий.</returns>
        Task<IEnumerable<TaskTypeOutput>> GetTypesSelectAsync();

        /// <summary>
        /// Метод выдаст тип контрола.
        /// </summary>
        /// <param name="selectedValue">Выбранное значение.</param>
        /// <returns>Тип контрола.</returns>
        Task<ControlVisibleOutput> VisibleControlAsync(string selectedValue);
    }
}
