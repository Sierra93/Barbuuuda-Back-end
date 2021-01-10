using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.Task {
    /// <summary>
    /// Модель сопоставляется с таблицей статусов заданий.
    /// </summary>
    [Table("TaskTypes", Schema = "dbo")]
    public sealed class TaskTypeDto {
        [Key, Column("type_id")]
        public int TypeId { get; set; }

        [Column("type_code", TypeName = "varchar(100)")]
        public string TypeCode { get; set; }  // Код типа.

        [Column("type_name", TypeName = "varchar(100)")]
        public string TypeName { get; set; }    // Название типа задания.
    }
}
