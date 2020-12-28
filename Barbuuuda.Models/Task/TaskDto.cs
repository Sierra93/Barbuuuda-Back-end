using Barbuuuda.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.Task {
    /// <summary>
    /// Модель сопоставляется с таблизей заданий.
    /// </summary>
    [Table("Tasks", Schema = "dbo")]
    public sealed class TaskDto {
        [Key, Column("task_id")]
        public int TaskId { get; set; }

        [Required, Column("owner_id", TypeName = "integer")]
        public int OwnerId { get; set; }    // Id заказчика, который создал задание.

        [ForeignKey("OwnerId")]
        public UserDto Owner { get; set; }

        [Column("executor_id", TypeName = "integer")]
        public int? ExecutorId { get; set; }

        [ForeignKey("ExecutorId")]
        public UserDto Executor { get; set; }   // Id исполнителя, который выполняет задание.

        [Column("date_create_task", TypeName = "timestamp")]
        public DateTime DateCreateTask { get; set; }    // Дата создания задачи.

        [Column("count_offers", TypeName = "integer")]
        public int CountOffers { get; set; }    // Кол-во ставок к заданию.

        [Column("count_views", TypeName = "integer")]
        public int CountViews { get; set; }     // Кол-во просмотров задания.

        [Column("type_code", TypeName = "varchar(100)")]
        public string TypeCode { get; set; } // Id типа заданий (для всех, для про).

        [Column("status_code", TypeName = "varchar(100)")]
        public string StatusCode { get; set; }   // Id статуса задания.

        [Column("category_code", TypeName = "varchar(100)")]
        public string CategoryCode { get; set; }     // Id категории задания (программирование и тд).

        [Column("task_price", TypeName = "money")]
        public double? TaskPrice { get; set; }   // Бюджет задания в цифрах либо по дефолту "По договоренности".

        [Column("task_title", TypeName = "text")]
        public string TaskTitle { get; set; }   // Заголовок задания.

        [Column("task_detail", TypeName = "text")]
        public string TaskDetail { get; set; }  // Описание задания.

        [Column("spec_code", TypeName = "varchar(100)")]
        public string SpecCode { get; set; }
    }
}
