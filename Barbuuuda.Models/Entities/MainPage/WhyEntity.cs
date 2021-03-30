using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.MainPage
{
    /// <summary>
    /// Класс сопоставляется с таблицей Почему Барбуда.
    /// </summary>
    [Table("Whies", Schema = "dbo")]
    public sealed class WhyEntity
    {
        [Key, Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Главный заголовок блока.
        /// </summary>
        [Column("main_titie", TypeName = "nvarchar(200)")]
        public string MainTitle { get; set; }

        /// <summary>
        /// Доп.заголовок.
        /// </summary>
        [Column("second_title", TypeName = "nvarchar(200)")]
        public string SecondTitle { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("text", TypeName = "nvarchar(500)")]
        public string Text { get; set; }    
    }
}
