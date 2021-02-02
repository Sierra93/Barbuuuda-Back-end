using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.MainPage
{
    /// <summary>
    /// Модель сопоставляется с таблицей Преимущества.
    /// </summary>
    [Table("Advantages", Schema = "dbo")]
    public sealed class AdvantageEntity
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("main_title", TypeName = "nvarchar(200)")]
        public string MainTitle { get; set; }

        [Column("second_title", TypeName = "nvarchar(200)")]
        public string SecondTitle { get; set; }

        [Column("text", TypeName = "nvarchar(500)")]
        public string Text { get; set; }
    }
}
