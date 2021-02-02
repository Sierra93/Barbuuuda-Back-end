using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.MainPage
{
    /// <summary>
    /// Модель описывает таблицу главного фона на главной странице.
    /// </summary>
    [Table("Fons", Schema = "dbo")]
    public sealed class FonEntity
    {
        [Key, Column("fon_id")]
        public int FonId { get; set; }

        [Column("main_title", TypeName = "nvarchar(200)")]
        public string MainTitle { get; set; }   // Главный заголовок фона.

        [Column("second_title", TypeName = "nvarchar(200)")]
        public string SecondTitle { get; set; }     // Подзаголовок фона.

        [Column("btn-text", TypeName = "nvarchar(100)")]
        public string BtnText { get; set; }     // Текст кнопки фона.
    }
}
