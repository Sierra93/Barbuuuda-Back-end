using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Task
{
    /// <summary>
    /// Модель сопоставляется с таблицей категорий задач.
    /// </summary>
    [Table("TaskCategories", Schema = "dbo")]
    public sealed class TaskCategoryEntity
    {
        [Key, Column("category_id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Код категории.
        /// </summary>
        [Column("category_code", TypeName = "varchar(100)")]
        public string CategoryCode { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        [Column("category_name", TypeName = "varchar(100)")]
        public string CategoryName { get; set; }    

        [Column("specializations", TypeName = "jsonb")]
        public Specialization[] Specializations { get; set; }

        /// <summary>
        /// Url иконки категории.
        /// </summary>
        [Column("url", TypeName = "text")]
        public string Url { get; set; }     
    }

    /// <summary>
    /// Класс сопоставляется с json столбцом специализаций.
    /// </summary>
    [NotMapped]
    public sealed class Specialization
    {
        /// <summary>
        /// Наименование специализации.
        /// </summary>
        public string SpecName { get; set; }

        /// <summary>
        /// Код специализации.
        /// </summary>
        public string SpecCode { get; set; }
    }
}
