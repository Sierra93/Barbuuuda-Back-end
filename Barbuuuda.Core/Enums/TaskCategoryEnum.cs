using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Barbuuuda.Core.Enums {
    /// <summary>
    /// Перечислитель статусов заданий.
    /// </summary>
    public enum TaskCategoryEnum : int {
        [Description("Программирование")]
        Program = 1,

        [Description("Дизайн")]
        Design = 2
    }
}
