using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Executor.Input;
using Barbuuuda.Models.User;
using System.Collections;
using System.Collections.Generic;
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
        Task RespondAsync(int taskId, decimal? price, bool isTemplate, RespondInput respondInput, string comment, string userName);
    }
}
