using System.Collections.Generic;

namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Список для значений селекта сортировки заданий.
    /// </summary>
    public class ControlSortResult
    {
        public List<ControlSortOutput> ControlSorts { get; set; } = new List<ControlSortOutput>();

        public int Count => ControlSorts.Count;
    }
}
