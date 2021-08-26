using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.User;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Barbuuuda.Models.Respond.Input;
using Barbuuuda.Models.Respond.Output;
using Barbuuuda.Models.Task.Output;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция описывает методы по работе с исполнителями сервиса.
    /// </summary>
    public interface IExecutorService
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

        /// <summary>
        /// Метод получает вопрос для теста исполнителя в зависимости от номера вопроса, переданного с фронта.
        /// </summary>
        /// <param name="numberQuestion">Номер вопроса.</param>
        /// <returns>Вопрос с вариантами ответов.</returns>
        Task<object> GetQuestionAsync(int numberQuestion);

        /// <summary>
        /// Метод получает кол-во вопросов для теста исполнителя.
        /// </summary>
        /// <returns>Кол-во вопросов.</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Метод проверяет результаты ответов на тест исполнителем.
        /// </summary>
        /// <param name="answers">Массив с ответами на тест.</param>
        /// <param name="userName">Логин юзера.</param>
        /// <returns>Статус прохождения теста true/false.</returns>
        Task<bool> CheckAnswersTestAsync(List<AnswerVariant> answers, string userName);

        /// <summary>
        /// Метод выгружает задания, которые находятся в работе у исполнителя. Т.е у которых статус "В работе".
        /// </summary>
        /// <returns>Список заданий.</returns>
        Task<IEnumerable> GetTasksWorkAsync(string userName);

        /// <summary>
        /// Метод оставляет ставку к заданию.
        /// </summary>
        /// <param name="taskId">Id задания, к которому оставляют ставку.</param>
        /// <param name="price">Цена ставки (без комиссии 22%).</param>
        /// <param name="comment">Комментарий к ставке.</param>
        /// <param name="isTemplate">Флаг сохранения как шаблон.</param>
        /// <param name="template">Данные шаблона.</param>
        /// <param name="userName">Имя юзера.</param>
        Task<bool> RespondAsync(long? taskId, decimal? price, bool isTemplate, RespondInput respondInput, string comment, string userName);

        /// <summary>
        /// Метод изменит ставку к заданию.
        /// </summary>
        /// <param name="taskId">Id задания, к которому оставляют ставку.</param>
        /// <param name="price">Цена ставки (без комиссии 22%).</param>
        /// <param name="comment">Комментарий к ставке.</param>
        /// <param name="respondId">Id ставки.</param>
        /// <param name="userName">Имя юзера.</param>
        Task<bool> ChangeRespondAsync(long taskId, decimal price, string comment, long respondId, string userName);

        /// <summary>
        /// Метод проверит, была ли сделана ставка к заданию текущим исполнителем.
        /// </summary>
        /// <param name="taskId">Id задания</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Статус проверки true/false.</returns>
        Task<bool> CheckRespondAsync(long? taskId, string account);

        /// <summary>
        /// Метод выгрузит список заданий, в которых был выбран исполнитель.
        /// </summary>
        /// <param name="account">Логин исполнителя.</param>
        /// <returns>Список приглашений с данными заданий.</returns>
        Task<GetResultTask> InviteAsync(string account);

        /// <summary>
        /// Метод выгрузит список заданий, в которых был выбран текущий исполнитель.
        /// </summary>
        /// <param name="account">Логин исполнителя.</param>
        /// <returns>Список заданий.</returns>
        //Task<GetResultTask> MyTasksAsync(string account);

        /// <summary>
        /// Метод проставит согласие на выполнение задания.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="account">Логин исполнителя.</param>
        /// <returns>Флаг результата.</returns>
        Task<bool> AcceptTaskAsync(long taskId, string account);

        /// <summary>
        /// Метод проставит отказ на выполнение задания.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="account">Логин исполнителя.</param>
        /// <returns>Флаг результата.</returns>
        Task<bool> CancelTaskAsync(long taskId, string account);


        /// <summary>
        /// Метод получит список заданий для вкладки "Мои задания". Т.е задания, работа над которыми начата текущим исполнителем.
        /// <param name="account">Логин исполнителя.</param>
        /// </summary>
        /// <returns>Список заданий.</returns>
        Task<GetResultTask> GetWorkTasksAsync(string account);

        /// <summary>
        /// Метод получит ставку исполнителя для ее изменения.
        /// </summary>
        /// <param name="taskId">Id задания.</param>
        /// <param name="respondId">Id ставки.</param>
        /// <param name="account">Логин пользователя.</param>
        /// <returns>Данные ставки исполнителя.</returns>
        Task<ChangeRespondOutput> GetChangedRespondAsync(long taskId, long respondId, string account);
    }
}
