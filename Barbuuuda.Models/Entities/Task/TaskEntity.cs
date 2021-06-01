using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Task
{
    /// <summary>
    /// Модель сопоставляется с таблицей заданий.
    /// </summary>
    [Table("Tasks", Schema = "dbo")]
    public sealed class TaskEntity
    {
        [Key, Column("TaskId")]
        public int TaskId { get; set; }

        /// <summary>
        /// Id заказчика, который создал задание.
        /// </summary>
        [Required, Column("OwnerId", TypeName = "varchar(150)")]
        public string OwnerId { get; set; }

        /// <summary>
        /// Id исполнителя, который выполняет задание.
        /// </summary>
        [Column("ExecutorId", TypeName = "varchar(150)")]
        public string ExecutorId { get; set; }

        /// <summary>
        /// Дата создания задачи.
        /// </summary>
        [Column("TaskBegda", TypeName = "timestamp")]
        public DateTime TaskBegda { get; set; }

        /// <summary>
        /// Дата завершения задачи.
        /// </summary>
        [Column("TaskEndda", TypeName = "timestamp")]
        public DateTime TaskEndda { get; set; }

        /// <summary>
        /// Кол-во ставок к заданию.
        /// </summary>
        [Column("CountOffers", TypeName = "integer")]
        public int CountOffers { get; set; }

        /// <summary>
        /// Кол-во просмотров задания.
        /// </summary>
        [Column("CountViews", TypeName = "integer")]
        public int CountViews { get; set; }

        /// <summary>
        /// Код типа заданий (для всех, для про).
        /// </summary>
        [Column("TypeCode", TypeName = "varchar(100)")]
        public string TypeCode { get; set; }

        /// <summary>
        /// Код статуса задания.
        /// </summary>
        [Column("StatusCode", TypeName = "varchar(100)")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Код категории задания (программирование и тд).
        /// </summary>
        [Column("CategoryCode", TypeName = "varchar(100)")]
        public string CategoryCode { get; set; }

        /// <summary>
        /// Бюджет задания в цифрах либо по дефолту "По договоренности".
        /// </summary>
        [Column("TaskPrice", TypeName = "numeric(12,2)")]
        public decimal? TaskPrice { get; set; }

        /// <summary>
        /// Заголовок задания.
        /// </summary>
        [Column("TaskTitle", TypeName = "text")]
        public string TaskTitle { get; set; }

        /// <summary>
        /// Описание задания.
        /// </summary>
        [Column("TaskDetail", TypeName = "text")]
        public string TaskDetail { get; set; }

        /// <summary>
        /// Код специализации.   
        /// </summary>
        [Column("SpecCode", TypeName = "varchar(100)")]
        public string SpecCode { get; set; }

        /// <summary>
        /// Флаг оплаты задания заказчиком.
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// Флаг подтверждения исполнителем взятия в работу задания.
        /// </summary>
        public bool IsWorkAccept { get; set; }
    }
}
