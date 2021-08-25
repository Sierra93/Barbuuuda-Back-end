using System.Collections.Generic;

namespace Barbuuuda.Models.Task.Output
{
    public class ControlFilterResult
    {
        public List<ControlFilterOutput> ControlFilters { get; set; } = new List<ControlFilterOutput>();

        public long Count => ControlFilters.Count;
    }
}
