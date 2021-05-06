using System.Collections.Generic;

namespace Barbuuuda.Models.Task.Outpoot
{
    /// <summary>
    /// Класс выходной модели со списком моделей заданий аукциона.
    /// </summary>
    public class GetTaskResultOutpoot
    {
        /// <summary>
        /// Список заданий.
        /// </summary>
        public List<TaskOutpoot> Tasks { get; set; } = new List<TaskOutpoot>();

        /// <summary>
        /// Общее кол-во заданий аукциона.
        /// </summary>
        public int TotalCount { get { return Tasks.Count; } set { } }
    }
}
