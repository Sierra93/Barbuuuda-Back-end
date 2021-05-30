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
        [Column("ScoreNumber", TypeName = "int4"), MinLength(20)]
        public int? ScoreNumber { get; set; }

        public UserEntity User { get; set; }

        /// <summary>
        /// Адрес выставления счета.
        /// </summary>
        [Column("ScoreEmail", TypeName = "varchar(500)"), DefaultValue("")]
        [RegularExpression(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))){2,63}\.?$", ErrorMessage = "Некорректный адрес")]
        public string ScoreEmail { get; set; }
    }
}
