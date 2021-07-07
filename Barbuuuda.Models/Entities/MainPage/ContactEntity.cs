using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.MainPage
{
    /// <summary>
    /// Класс сопоставляется с таблицей dbo.Contacts.
    /// </summary>
    [Table("Contacts", Schema = "dbo")]
    public class ContactEntity
    {
        [Key]
        public long ContactId { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        [Required, Column("Name", TypeName = "nvarchar(100)"), MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Почта.
        /// </summary>
        [Required, Column("Email", TypeName = "nvarchar(100)"), MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// График работы.
        /// </summary>
        [Required, Column("Work", TypeName = "nvarchar(200)"), MaxLength(200)]
        public string Work { get; set; }

        /// <summary>
        /// Создатель.
        /// </summary>
        [Required, Column("Creator", TypeName = "nvarchar(100)"), MaxLength(200)]
        public string Creator { get; set; }

        /// <summary>
        /// ИНН.
        /// </summary>
        [Required, Column("INN", TypeName = "bigint)")]
        public long INN { get; set; }
    }
}
