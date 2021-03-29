using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Task
{
    /// <summary>
    /// Модель сопоставляется с таблицей статусов заданий.
    /// </summary>
    [Table("TaskTypes", Schema = "dbo")]
    public sealed class TaskTypeEntity
    {
        [Key, Column("type_id")]
        public int TypeId { get; set; }

        /// <summary>
        /// Код типа.
        /// </summary>
        [Column("type_code", TypeName = "varchar(100)")]
        public string TypeCode { get; set; }

        /// <summary>
        /// Название типа задания.
        /// </summary>
        [Column("type_name", TypeName = "varchar(100)")]
        public string TypeName { get; set; }    
    }
}
