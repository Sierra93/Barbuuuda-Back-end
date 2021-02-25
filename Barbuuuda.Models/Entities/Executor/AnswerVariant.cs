using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Executor
{
    /// <summary>
    /// Класс модели вариантов ответов к вопросам теста для исполнителей.
    /// </summary>
    [Table("AnswerVariants", Schema = "dbo")]
    public sealed class AnswerVariant
    {
        [Key, Column("AnswerVariantId")]
        public int AnswerVariantId { get; set; }

        /// <summary>
        /// Описание варианта ответа.
        /// </summary>
        [Column("AnswerVariantText", TypeName = "nvarchar(max)")]
        public string AnswerVariantText { get; set; }

        /// <summary>
        /// Внешний ключ к QuestionId вопроса таблицы Questions.
        /// </summary>
        [Column("QuestionId"), ForeignKey("QuestionId")]
        public int QuestionId { get; set; }

        /// <summary>
        /// Верный ответ или нет. true/false.
        /// </summary>
        [Column("IsTrue", TypeName = "bit")]
        public bool IsTrue { get; set; }
    }
}
