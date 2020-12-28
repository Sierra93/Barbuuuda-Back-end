using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Barbuuuda.Core.Enums {
    /// <summary>
    /// Перечислитель статусов заданий.
    /// </summary>
    public enum TaskStatusEnum : int {
        [Description("В аукционе")]
        Auction = 1,

        [Description("В работе")]
        Work = 2
    }
}
