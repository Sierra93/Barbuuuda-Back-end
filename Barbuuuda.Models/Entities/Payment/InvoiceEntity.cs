using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbuuuda.Models.User;

namespace Barbuuuda.Models.Entities.Payment
{
    /// <summary>
    /// Класс сопоставляется с таблицей счетов пользователей dbo.Invoices.
    /// </summary>
    [Table("Invoices", Schema = "dbo")]
    public class InvoiceEntity
    {
        /// <summary>
        /// Id счета.
        /// </summary>
        [Key, Column("ScoreId", TypeName = "bigint")]
        public long ScoreId { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        [Required, ForeignKey("Id"), Column("UserId", TypeName = "text"), DefaultValue("")]
        public string UserId { get; set; }

        /// <summary>
        /// Сумма счета.
        /// </summary>
        [Required, Column("InvoiceAmount", TypeName = "numeric(12,2)"), DefaultValue(0)]
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        [Required, Column("Currency", TypeName = "varchar(10)"), DefaultValue("")]
        public string Currency { get; set; }

        /// <summary>
        /// Номер счета, на который производятся выплаты и возвраты.
        /// </summary>
        [Required, Column("ScoreNumber", TypeName = "int"), MinLength(20)]
        public int? ScoreNumber { get; set; }

        public UserEntity User { get; set; }
    }
}
