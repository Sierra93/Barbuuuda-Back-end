using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Respond
{
    /// <summary>
    /// Класс сопоставляется с таблицей ставок к заданиям.
    /// </summary>
    [Table("Responds", Schema = "dbo")]
    public sealed class RespondEntity
    {
        /// <summary>
        /// Первичный ключ.
        /// </summary>
        [Key, Column("RespondId")]
        public int RespondId { get; set; }

        /// <summary>
        /// Цена ставки (без комиссии).
        /// </summary>
        [Column("Price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        [Column("TaskId")]
        public long? TaskId { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Column("Comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Id исполнителя, который сделал ставку к заданию.
        /// </summary>
        [Column("ExecutorId")]
        public string ExecutorId { get; set; }
    }
}
