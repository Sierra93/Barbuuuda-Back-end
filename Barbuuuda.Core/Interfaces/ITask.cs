using Barbuuuda.Models.Task;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces {
    /// <summary>
    /// Интерфейс содержит логику работы с заданиями.
    /// </summary>
    public interface ITask {
        /// <summary>
        /// Метод создает новое задание.
        /// </summary>
        /// <param name="task">Объект с данными задания.</param>
        /// <returns>Вернет данные созданного задания.</returns>
        Task<TaskDto> CreateTask(TaskDto oTask);

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns>Коллекцию категорий.</returns>
        Task<IList> GetTaskCategories();

        /// <summary>
        /// Метод выгружает список специализаций заданий.
        /// </summary>
        /// <returns>Коллекцию специализаций.</returns>
        Task<IList> GetTaskSpecializations();

        /// <summary>
        /// Метод получает список заданий заказчика.
        /// </summary>
        /// <param name="userId">Id заказчика.</param>
        /// <returns>Коллекция заданий.</returns>
        Task<IList> GetTasksList(int userId);
    }
}
