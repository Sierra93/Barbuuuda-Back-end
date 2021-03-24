using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Knowlege
{
    /// <summary>
    /// Класс сопоставляется с таблицей популярных статей.
    /// </summary>
    [Table("PopularArticles")]
    public sealed class PopularArticleEntity
    {
        [Key]
        public int PopularId { get; set; }

        /// <summary>
        /// Заголовок популярной статьи.
        /// </summary>
        [DefaultValue(""), Column("ArticleTitle", TypeName = "varchar(400)")]
        public string ArticleTitle { get; set; }

        /// <summary>
        /// Кол-во людей, которым помогла статья.
        /// </summary>
        [DefaultValue(0), Column("HelpfulCount", TypeName = "integer")]
        public int HelpfulCount { get; set; }
    }
}
