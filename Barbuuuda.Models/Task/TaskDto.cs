﻿using Barbuuuda.Models.User;
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

        [Required, Column("owner_id", TypeName = "int")]
        public int OwnerId { get; set; }    // Id заказчика, который создал задание.

        [ForeignKey("OwnerId")]
        public UserDto Owner { get; set; }

        [Column("executor_id", TypeName = "int")]
        public int? ExecutorId { get; set; }

        [ForeignKey("ExecutorId")]
        public UserDto Executor { get; set; }   // Id исполнителя, который выполняет задание.

        [Column("date_create_task", TypeName = "datetime")]
        public DateTime DateCreateTask { get; set; }    // Дата создания задачи.

        [Column("count_offers", TypeName = "int")]
        public int CountOffers { get; set; }    // Кол-во ставок к заданию.

        [Column("count_views", TypeName = "int")]
        public int CountViews { get; set; }     // Кол-во просмотров задания.

        [Column("task_type_id", TypeName = "int")]
        public int TaskTypeId { get; set; } // Id типа заданий (для всех, для про).
        [ForeignKey("TaskTypeId")]
        public TaskTypeDto Type { get; set; }

        [Column("task_status_id", TypeName = "int")]
        public int TaskStatusId { get; set; }   // Id статуса задания.
        [ForeignKey("TaskStatusId")]
        public TaskStatusDto Status { get; set; }

        [Column("task_category_id", TypeName = "int")]
        public int TaskCategoryId { get; set; }     // Id категории задания (программирование и тд).
        [ForeignKey("TaskCategoryId")]
        public TaskCategoryDto Category { get; set; }

        [Column("task_price", TypeName = "money")]
        public double? TaskPrice { get; set; }   // Бюджет задания в цифрах либо по дефолту "По договоренности".

        [Column("task_title", TypeName = "nvarchar(200)")]
        public string TaskTitle { get; set; }   // Заголовок задания.

        [Column("task_detail", TypeName = "nvarchar(max)")]
        public string TaskDetail { get; set; }  // Описание задания.

    }
}
