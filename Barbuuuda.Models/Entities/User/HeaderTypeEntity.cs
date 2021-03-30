using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.User
{
    /// <summary>
    /// Модель сопоставляется с таблицей наборов полей для хидера.
    /// </summary>
    [Table("Headers", Schema = "dbo")]
    public sealed class HeaderTypeEntity
    {
        [Key, Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Иконка поля хидера (если есть).
        /// </summary>
        [Column("header_icon", TypeName = "nvarchar(max)")]
        public string HeaderIcon { get; set; }

        /// <summary>
        /// Название поля хидера.
        /// </summary>
        [Column("header_field", TypeName = "nvarchar(200)")]
        public string HeaderField { get; set; }

        /// <summary>
        /// Тип хидера.
        /// </summary>
        [Column("header_type", TypeName = "nvarchar(50)")]
        public string HeaderType { get; set; }

        /// <summary>
        /// Поле меню профиля.
        /// </summary>
        [Column("profile_field", TypeName = "nvarchar(100)")]
        public string ProfileField { get; set; }

        /// <summary>
        /// Пункт профиля или нет.
        /// </summary>
        [Column("is_profile", TypeName = "bit")]
        public bool IsProfile { get; set; }     
    }
}
