using Barbuuuda.Models.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Chat
{
    /// <summary>
    /// Класс сопоставляется с таблицей участников диалога dbo.DialogMembers.
    /// </summary>
    [Table("DialogMembers", Schema = "dbo")]
    public sealed class DialogMemberEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key, Column("MemberId", TypeName = "serial")]
        public int MemberId { get; set; }

        /// <summary>
        /// Id участника диалога.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Дата и время присоединения к диалогу.
        /// </summary>
        [Column("Joined", TypeName = "timestamp")]
        public DateTime Joined { get; set; }

        /// <summary>
        /// Id диалога.
        /// </summary>
        public int DialogId { get; set; }

        [ForeignKey("Id")]
        public UserEntity User { get; set; }

        [ForeignKey("DialogId")]
        public MainInfoDialogEntity Dialog { get; set; }
    }
}
