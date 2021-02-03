using Barbuuuda.Models.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Models.Outpoot
{
    /// <summary>
    /// Класс с информацией о пагинации.
    /// </summary>
    public class ModelIndexOutpoot
    {
        public IEnumerable<TaskEntity> Tasks { get; set; }

        public ModelPaginationOutpoot PageData { get; set; }
    }
}
