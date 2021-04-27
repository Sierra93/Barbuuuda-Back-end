using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Chat
{
    /// <summary>
    /// Класс сопоставляется с таблицей сообщений dbo.DialogMessages.
    /// </summary>
    [Table("DialogMessages", Schema = "dbo")]
    public sealed class DialogMessageEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key, Column("MessageId", TypeName = "serial")]
        public int MessageId { get; set; }

        /// <summary>
        /// Id диалога.
        /// </summary>
        public long DialogId { get; set; }

        /// <summary>
        /// Сообщение.
        /// </summary>
        [Column("Message", TypeName = "text")]
        public string Message { get; set; }

        /// <summary>
        /// Дата и время сообщения.
        /// </summary>
        [Column("Created", TypeName = "timestamp")]
        public DateTime Created { get; set; }

        [ForeignKey("DialogId")]
        public MainInfoDialogEntity Dialog { get; set; }
    }
}
