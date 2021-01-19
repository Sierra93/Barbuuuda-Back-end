using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.MainPage {
    /// <summary>
    /// Класс сопоставляется с таблицей "Надеемся на долгое сотрудничество".
    /// </summary>
    [Table("Hopes", Schema = "dbo")]
    public sealed class HopeDto {
        [Key, Column("hope_id")]
        public int HopeId { get; set; }

        [Column("hope_title", TypeName = "nvarchar(150)")]
        public string HopeTitle { get; set; }   // Заголовок блока.

        [Column("hope_details", TypeName = "nvarchar(200)")]
        public string HopeDetails { get; set; }     // Описание блока.
    }
}
