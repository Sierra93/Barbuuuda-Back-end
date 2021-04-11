using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Chat
{
    /// <summary>
    /// Класс сопоставляется с таблицей информации о диалогах dbo.MainInfoDialogs.
    /// </summary>
    [Table("MainInfoDialogs", Schema = "dbo")]
    public sealed class MainInfoDialogEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key, Column("DialogId", TypeName = "serial")]
        public int DialogId { get; set; }

        /// <summary>
        /// Название диалога.
        /// </summary>
        [Column("DialogName", TypeName = "varchar(300)")]
        public string DialogName { get; set; }

        /// <summary>
        /// Дата и время создания диалога.
        /// </summary>
        [Column("Created", TypeName = "timestamp")]
        public DateTime Created { get; set; }
    }
}
