using Barbuuuda.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.User
{
    /// <summary>
    /// Класс сопоставляется с таблицей статистики пользователей.
    /// </summary>
    [Table("State")]
    public sealed class StateEntity
    {
        [Key]
        public int StateId { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public string Id { get; set; }

        [ForeignKey("Id")]
        public UserEntity UserId { get; set; }

        /// <summary>
        /// Флаг нахождения на сервисе в данный момент.
        /// </summary>
        [Column("IsOnline", TypeName = "bool")]
        public bool IsOnline { get; set; }
    }
}
