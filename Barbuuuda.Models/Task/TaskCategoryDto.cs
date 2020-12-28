﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.Task {
    /// <summary>
    /// Модель сопоставляется с таблицей категорий задач.
    /// </summary>
    [Table("TaskCategories", Schema = "dbo")]
    public sealed class TaskCategoryDto {
        [Key, Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category_code", TypeName = "varchar(100)")]
        public string CategoryCode { get; set; }  // Код категории.

        [Column("category_name", TypeName = "varchar(100)")]
        public string CategoryName { get; set; }    // Название категории.
    }
}
