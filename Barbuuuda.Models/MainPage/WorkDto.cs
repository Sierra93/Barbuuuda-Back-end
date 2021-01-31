using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.MainPage
{
    /// <summary>
    /// Модель описывает таблицу "Как это работает".
    /// </summary>
    [Table("Works", Schema = "dbo")]
    public sealed class WorkDto
    {
        [Key, Column("work_id")]
        public int WorkId { get; set; }

        [Column("main_title", TypeName = "nvarchar(150)")]
        public string MainTitle { get; set; }   // Основной заголовок блока.

        [Column("second_title", TypeName = "nvarchar(150)")]
        public string SecondTitle { get; set; }     // Дополнительный заголовок.

        [Column("block_title", TypeName = "nvarchar(150)")]
        public string BlockTitle { get; set; }  // Заголовок блока.

        [Column("block_text", TypeName = ("nvarchar(500)"))]
        public string BlockText { get; set; }   // Описание блока.
    }
}
