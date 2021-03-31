using Barbuuuda.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Executor
{
    /// <summary>
    /// Класс сопоставляется с таблицей статистики исполнителей.
    /// </summary>
    [Table("ExecutorStatistic")]
    public sealed class StatisticEntity
    {
        [Key]
        public int StatId { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public string Id { get; set; }

        [ForeignKey("Id")]
        public UserEntity UserId { get; set; }

        /// <summary>
        /// Общее кол-во выполненных заданий.
        /// </summary>
        [Column("CountTotalCompletedTask", TypeName = "bigint")]
        public long? CountTotalCompletedTask { get; set; }

        /// <summary>
        /// Кол-во положительных отзывов.
        /// </summary>
        [Column("CountPositive", TypeName = "bigint")]
        public long? CountPositive { get; set; }

        /// <summary>
        /// Кол-во отрицательных отзывов.
        /// </summary>
        [Column("CountNegative", TypeName = "bigint")]
        public long? CountNegative { get; set; }

        /// <summary>
        /// Рейтинг.
        /// </summary>
        [Column("Rating", TypeName = "numeric(12,2)")]
        public decimal Rating { get; set; }

        /// <summary>
        /// Счет.
        /// </summary>
        [Column("Score", TypeName = "numeric(12,2)")]
        public decimal Score { get; set; }

        /// <summary>
        /// Код категории.
        /// </summary>
        [Column("CategoryCode", TypeName = "varchar(100)")]
        public string CategoryCode { get; set; }

        /// <summary>
        /// Кол-во выполненных заданий категории.
        /// </summary>
        [Column("CountTaskCategory", TypeName = "bigint")]
        public long? CountTaskCategory { get; set; }
    }
}
