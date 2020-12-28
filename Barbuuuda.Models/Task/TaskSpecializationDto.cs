using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.Task {
    /// <summary>
    /// Модель сопоставляется с таблицей специализаций заданий.
    /// </summary>
    [Table("TaskSpecializations", Schema = "dbo")]
    public sealed class TaskSpecializationDto {
        [Key, Column("spec_id")]
        public int SpecId { get; set; }

        [Column("spec_name", TypeName = "varchar(200)")]
        public string SpecName { get; set; }    // Название специализации.
    }
}
