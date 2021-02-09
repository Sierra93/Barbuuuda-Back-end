using Barbuuuda.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        [Required, Column("OwnerId", TypeName = "varchar(150)")]
        public string OwnerId { get; set; }    // Id заказчика, который создал задание.

        [Column("ExecutorId", TypeName = "varchar(150)")]
        public string ExecutorId { get; set; }  // Id исполнителя, который выполняет задание.

        [Column("TaskBegda", TypeName = "timestamp")]
        public DateTime TaskBegda { get; set; }    // Дата создания задачи.

        [Column("TaskEndda", TypeName = "timestamp")]
        public DateTime TaskEndda { get; set; }    // Дата завершения задачи.

        [Column("CountOffers", TypeName = "integer")]
        public int CountOffers { get; set; }    // Кол-во ставок к заданию.

        [Column("CountViews", TypeName = "integer")]
        public int CountViews { get; set; }     // Кол-во просмотров задания.

        [Column("TypeCode", TypeName = "varchar(100)")]
        public string TypeCode { get; set; } // Код типа заданий (для всех, для про).

        [Column("StatusCode", TypeName = "varchar(100)")]
        public string StatusCode { get; set; }   // Код статуса задания.

        [Column("CategoryCode", TypeName = "varchar(100)")]
        public string CategoryCode { get; set; }     // Код категории задания (программирование и тд).

        [Column("TaskPrice", TypeName = "numeric(12,2)")]
        public decimal? TaskPrice { get; set; }   // Бюджет задания в цифрах либо по дефолту "По договоренности".

        [Column("TaskTitle", TypeName = "text")]
        public string TaskTitle { get; set; }   // Заголовок задания.

        [Column("TaskDetail", TypeName = "text")]
        public string TaskDetail { get; set; }  // Описание задания.

        [Column("SpecCode", TypeName = "varchar(100)")]
        public string SpecCode { get; set; }    // Код специализации.        
    }
}
