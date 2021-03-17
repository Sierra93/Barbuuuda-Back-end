using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Knowlege
{
    /// <summary>
    /// Класс сопоставляется с таблицей статей в БЗ.
    /// </summary>
    [Table("KnowlegeArticles")]
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
        [Column("ArticleTitle", TypeName = "varchar(100)")]
        public string ArticleTitle { get; set; }

        /// <summary>
        /// Детальное описание статьи.
        /// </summary>
        [Column("ArticleDetails", TypeName = "nvarchar(max)")]
        public string ArticleDetails { get; set; }

        /// <summary>
        /// Кол-во людей, которые сочли эту статью полезной.
        /// </summary>
        [Column("HelpfulCount", TypeName = "int")]
        public int HelpfulCount { get; set; }

        /// <summary>
        /// Кол-во людей, которые не сочли эту статью полезной.
        /// </summary>
        [Column("NotHelpfulCount", TypeName = "int")]
        public int NotHelpfulCount { get; set; }

        /// <summary>
        /// Вторичный ключ на PK CategoryId таблицы KnowlegeCategories.
        /// </summary>
        public int CategoryId { get; set; }
        public KnowlegeCategoryEntity Id { get; set; }

        /// <summary>
        /// Имеет ли статья скриншоты.
        /// </summary>
        [Column("HasImage", TypeName = "bit")]
        public bool HasImage { get; set; }

        /// <summary>
        /// Url скрина.
        /// </summary>
        [Column("ArticleDetails", TypeName = "nvarchar(max)")]
        public string ImageUrl { get; set; }
    }
}
