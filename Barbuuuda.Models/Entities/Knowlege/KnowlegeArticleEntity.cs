using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Knowlege
{
    /// <summary>
    /// Класс сопоставляется с таблицей статей в БЗ.
    /// </summary>
    [Table("KnowlegeArticles", Schema = "dbo")]
    public sealed class KnowlegeArticleEntity
    {
        /// <summary>
        /// Id статьи.
        /// </summary>
        [Key]
        public int ArticleId { get; set; }

        /// <summary>
        /// Заголовок статьи.
        /// </summary>
        [Column("ArticleTitle", TypeName = "varchar(200)")]
        public string ArticleTitle { get; set; }

        /// <summary>
        /// Детальное описание статьи.
        /// </summary>
        [Column("ArticleDetails", TypeName = "text")]
        public string ArticleDetails { get; set; }

        /// <summary>
        /// Кол-во людей, которые сочли эту статью полезной.
        /// </summary>
        [Column("HelpfulCount", TypeName = "integer")]
        public int HelpfulCount { get; set; }

        /// <summary>
        /// Кол-во людей, которые не сочли эту статью полезной.
        /// </summary>
        [Column("NotHelpfulCount", TypeName = "integer")]
        public int NotHelpfulCount { get; set; }

        /// <summary>
        /// Вторичный ключ на PK CategoryId таблицы KnowlegeCategories.
        /// </summary>
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public KnowlegeCategoryEntity Category { get; set; }

        /// <summary>
        /// Имеет ли статья скриншоты.
        /// </summary>
        [Column("HasImage", TypeName = "bool")]
        public bool HasImage { get; set; }

        /// <summary>
        /// Url скрина.
        /// </summary>
        [Column("ArticleDetails", TypeName = "text")]
        public string ImageUrl { get; set; }
    }
}
