using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.User {
    /// <summary>
    /// Модель сопоставляется с таблицей наборов полей для хидера.
    /// </summary>
    [Table("Headers", Schema = "dbo")]
    public sealed class HeaderTypeDto {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("header_icon", TypeName = "nvarchar(max)")]
        public string HeaderIcon { get; set; }  // Иконка поля хидера (если есть).

        [Column("header_field", TypeName = "nvarchar(200)")]
        public string HeaderField { get; set; }     // Название поля хидера.

        [Column("header_type", TypeName = "nvarchar(50)")]
        public string HeaderType { get; set; }  // Тип хидера.

        [Column("profile_field", TypeName = "nvarchar(100)")]
        public string ProfileField { get; set; }    // Поле меню профиля.

        [Column("is_profile", TypeName = "bit")]
        public bool IsProfile { get; set; }     // Пункт профиля или нет.
    }
}
