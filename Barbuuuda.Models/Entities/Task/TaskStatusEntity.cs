using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Task
{
    /// <summary>
    /// Модель сопоставляется с таблицей типов статусов.
    /// </summary>
    [Table("TaskStatuses", Schema = "dbo")]
    public sealed class TaskStatusEntity
    {
        [Key, Column("status_id")]
        public int StatusId { get; set; }

        /// <summary>
        /// Код статуса.
        /// </summary>
        [Column("status_code", TypeName = "varchar(100)")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Название статуса.
        /// </summary>
        [Column("status_name", TypeName = "varchar(100)")]
        public string StatusName { get; set; }
    }
}
