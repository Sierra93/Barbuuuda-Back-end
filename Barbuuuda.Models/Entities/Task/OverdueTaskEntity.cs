using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbuuuda.Models.Task;

namespace Barbuuuda.Models.Entities.Task
{
    /// <summary>
    /// Класс сопоставляется с таблицей просроченных заданий dbo.OverdueTasks.
    /// </summary>
    [Table("OverdueTasks", Schema = "dbo")]
    public class OverdueTaskEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key]
        public long OverdueId { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public TaskEntity Task { get; set; }
    }
}
