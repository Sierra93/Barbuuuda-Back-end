using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.MainPage {
    /// <summary>
    /// Класс сопоставляется с таблицей ЧТО ВЫ ПОЛУЧАЕТЕ.
    /// </summary>
    [Table("Privileges", Schema = "dbo")]
    public sealed class PrivilegeDto {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("title", TypeName = "nvarchar(200)")]
        public string Title { get; set; }   // Главный заголовок блока.

        [Column("text", TypeName = "nvarchar(500)")]
        public string Text { get; set; }    // Описание.
    }
}
