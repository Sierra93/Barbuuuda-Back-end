using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.Task
{
    /// <summary>
    /// Модель сопоставляется с таблицей типов статусов.
    /// </summary>
    [Table("TaskStatuses", Schema = "dbo")]
    public sealed class TaskStatusDto
    {
        [Key, Column("status_id")]
        public int StatusId { get; set; }

        [Column("status_code", TypeName = "varchar(100)")]
        public string StatusCode { get; set; }  // Код статуса.

        [Column("status_name", TypeName = "varchar(100)")]
        public string StatusName { get; set; }
    }
}
