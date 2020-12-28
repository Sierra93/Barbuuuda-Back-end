using Barbuuuda.Models.Task;
using System;
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
        Task<TaskDto> CreateTask(TaskDto task);
    }
}
