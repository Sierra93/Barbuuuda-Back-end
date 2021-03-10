using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Executor
{
    /// <summary>
    /// Класс модели вопросов для теста исполнителей.
    /// </summary>
    [Table("Questions", Schema = "dbo")]
    public sealed class QuestionEntity
    {
        [Key, Column("QuestionId")]
        public int QuestionId { get; set; }

        /// <summary>
        /// Описание вопроса теста.
        /// </summary>
        [Column("QuestionText", TypeName = "nvarchar(max)")]
        public string QuestionText { get; set; }

        /// <summary>
        /// Номер вопроса, который нужно получить.
        /// </summary>
        [Column("NumberQuestion", TypeName = "integer")]
        public int NumberQuestion { get; set; }
    }
}
