using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Executor
{
    /// <summary>
    /// Класс модели вариантов ответов к вопросам теста для исполнителей.
    /// </summary>
    [Table("AnswerVariants", Schema = "dbo")]
    public sealed class AnswerVariantEntity
    {
        [Key, Column("AnswerVariantId")]
        public int AnswerVariantId { get; set; }

        /// <summary>
        /// Массив с вариантами ответов.
        /// </summary>
        [Column("AnswerVariantText", TypeName = "jsonb")]
        public AnswerVariant[] AnswerVariantText { get; set; }

        /// <summary>
        /// Внешний ключ к QuestionId вопроса таблицы Questions.
        /// </summary>
        [Column("QuestionId"), ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
    }

    /// <summary>
    /// Нужно для столбца вариантов ответов.
    /// </summary>
    [NotMapped]
    public sealed class AnswerVariant
    {
        /// <summary>
        /// Вариант ответа.
        /// </summary>
        public string AnswerVariantText { get; set; }

        /// <summary>
        /// Был ли выбран ответ.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Является ли ответ верным.
        /// </summary>
        public bool? IsRight { get; set; }
    }
}
