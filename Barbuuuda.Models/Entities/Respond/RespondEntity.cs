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
        [Key]
        public int RespondId { get; set; }

        /// <summary>
        /// Цена ставки (без комиссии).
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        public int? TaskId { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Id исполнителя, который сделал ставку к заданию.
        /// </summary>
        public string ExecutorId { get; set; }
    }
}
