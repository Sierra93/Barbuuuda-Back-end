using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbuuuda.Models.User;

namespace Barbuuuda.Models.Entities.Customer
{
    /// <summary>
    /// Класс сопоставляется с таблицей заказов. Заказ представляет собой оплату заданий.
    /// </summary>
    [Table("Orders", Schema = "dbo")]
    public class OrderEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key]
        public long OrderId { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Сумма.
        /// </summary>
        [Column("Amount", TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        [Column("TaskId", TypeName = "bigint)")]
        public long? TaskId { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        [Column("Currency", TypeName = "varchar(50)")]
        public string Currency { get; set; }

        [ForeignKey("Id")]
        public UserEntity User { get; set; }

        //[ForeignKey("TaskId")]
        //public TaskEntity Task { get; set; }
    }
}
