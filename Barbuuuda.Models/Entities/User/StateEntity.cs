using Barbuuuda.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.User
{
    /// <summary>
    /// Класс сопоставляется с таблицей состояния пользователей.
    /// </summary>
    [Table("State", Schema = "dbo")]
    public sealed class StateEntity
    {
        [Key]
        public int StateId { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public string UserId { get; set; }

        [ForeignKey("Id")]
        public UserEntity User { get; set; }

        /// <summary>
        /// Флаг нахождения на сервисе в данный момент.
        /// </summary>
        [Column("IsOnline", TypeName = "bool")]
        public bool IsOnline { get; set; }
    }
}
