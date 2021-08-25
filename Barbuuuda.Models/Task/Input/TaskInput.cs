using System;

namespace Barbuuuda.Models.Task.Input
{
    /// <summary>
    /// Класс входной модели задания.
    /// </summary>
    public class TaskInput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        public long? TaskId { get; set; }

        /// <summary>
        /// Параметр получения заданий либо все либо одно.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Статус задания, задания которого нужно получить.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Заголовок задания.
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// Описание задания.
        /// </summary>
        public string TaskDetail { get; set; }

        /// <summary>
        /// Цена задания.
        /// </summary>
        public decimal TaskPrice { get; set; }

        /// <summary>
        /// Дата сдачи задания.
        /// </summary>
        public DateTime TaskEndda { get; set; }

        /// <summary>
        /// Код специализации задания.
        /// </summary>
        public string SpecCode { get; set; }

        /// <summary>
        /// Категория задания.
        /// </summary>
        public string CategoryCode { get; set; }
    }
}
