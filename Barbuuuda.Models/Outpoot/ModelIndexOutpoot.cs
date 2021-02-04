using Barbuuuda.Models.Task;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Models.Outpoot
{
    /// <summary>
    /// Класс с информацией о пагинации.
    /// </summary>
    public class ModelIndexOutpoot
    {
        public IEnumerable Tasks { get; set; }

        public ModelPaginationOutpoot PageData { get; set; }
    }
}
