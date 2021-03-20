using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Knowlege
{
    /// <summary>
    /// Класс сопоставляется с таблицей категорий БЗ.
    /// </summary>
    [Table("KnowlegeCategories", Schema = "knowlege")]
    public sealed class KnowlegeCategoryEntity
    {
        /// <summary>
        /// Id категории.
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// Заголовок категории.
        /// </summary>
        [Column("CategoryTitle", TypeName = "varchar(200)")]
        public string CategoryTitle { get; set; }

        /// <summary>
        /// Краткое описание категории.
        /// </summary>
        [Column("CategoryTooltip", TypeName = "varchar(400)")]
        public string CategoryTooltip { get; set; }

        /// <summary>
        /// Кол-во статей в категории.
        /// </summary>
        [Column("ArticlesCount", TypeName = "int")]
        public int ArticlesCount { get; set; }

        /// <summary>
        /// Главный заголовок, который классифицирует всю категорию.
        /// </summary>
        [Column("CategoryMainTitle", TypeName = "varchar(200)")]
        public string CategoryMainTitle { get; set; }
    }
}
