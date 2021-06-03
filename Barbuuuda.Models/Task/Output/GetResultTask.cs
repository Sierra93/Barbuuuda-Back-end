using System.Collections.Generic;

namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс со списком выходных моделей заданий.
    /// </summary>
    public class GetResultTask
    {
        /// <summary>
        /// Список с заданиями.
        /// </summary>
        public List<ResultTaskOutput> Invities { get; set; } = new List<ResultTaskOutput>();

        /// <summary>
        /// Кол-во заданий.
        /// </summary>
        public int Count => Invities.Count;
    }
}
