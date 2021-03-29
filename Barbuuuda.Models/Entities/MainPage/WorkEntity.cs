using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.MainPage
{
    /// <summary>
    /// Модель описывает таблицу "Как это работает".
    /// </summary>
    [Table("Works", Schema = "dbo")]
    public sealed class WorkEntity
    {
        [Key, Column("work_id")]
        public int WorkId { get; set; }

        /// <summary>
        /// Основной заголовок блока.
        /// </summary>
        [Column("main_title", TypeName = "nvarchar(150)")]
        public string MainTitle { get; set; }

        /// <summary>
        /// Дополнительный заголовок.
        /// </summary>
        [Column("second_title", TypeName = "nvarchar(150)")]
        public string SecondTitle { get; set; }

        /// <summary>
        /// Заголовок блока.
        /// </summary>
        [Column("block_title", TypeName = "nvarchar(150)")]
        public string BlockTitle { get; set; }

        /// <summary>
        /// Описание блока.
        /// </summary>
        [Column("block_text", TypeName = ("nvarchar(500)"))]
        public string BlockText { get; set; }   
    }
}
