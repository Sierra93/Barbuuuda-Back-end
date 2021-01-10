using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.MainPage {
    /// <summary>
    /// Класс сопоставляется с таблицей Почему Барбуда.
    /// </summary>
    [Table("Whies", Schema = "dbo")]
    public sealed class WhyDto {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("main_titie", TypeName = "nvarchar(200)")]
        public string MainTitle { get; set; }   // Главный заголовок блока.

        [Column("second_title", TypeName = "nvarchar(200)")]
        public string SecondTitle { get; set; }     // Доп.заголовок.

        [Column("text", TypeName = "nvarchar(500)")]
        public string Text { get; set; }    // Описание.
    }
}
