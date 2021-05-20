using System.Collections.Generic;

namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели со списком моделей заданий аукциона.
    /// </summary>
    public class GetTaskResultOutput
    {
        /// <summary>
        /// Список заданий.
        /// </summary>
        public List<TaskOutput> Tasks { get; set; } = new List<TaskOutput>();

        /// <summary>
        /// Общее кол-во заданий аукциона.
        /// </summary>
        public int TotalCount { get { return Tasks.Count; } set { } }
    }
}
