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
        [Key, Column("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Главный заголовок блока.
        /// </summary>
        [Column("Title", TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("Text", TypeName = "nvarchar(max)")]
        public string Text { get; set; }    
    }
}
