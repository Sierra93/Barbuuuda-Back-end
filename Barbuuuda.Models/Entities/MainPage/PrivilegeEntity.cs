using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.MainPage
{
    /// <summary>
    /// Класс сопоставляется с таблицей ЧТО ВЫ ПОЛУЧАЕТЕ.
    /// </summary>
    [Table("Privileges", Schema = "dbo")]
    public sealed class PrivilegeEntity
    {
        [Key, Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Главный заголовок блока.
        /// </summary>
        [Column("title", TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("text", TypeName = "nvarchar(500)")]
        public string Text { get; set; }    
    }
}
