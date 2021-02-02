using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.Logger {
    /// <summary>
    /// Класс сопоставляется с таблицей логов.
    /// </summary>
    [Table("Logs", Schema = "dbo")]
    public class LoggerEntity {
        [Key, Column("log_id", TypeName = "int")]
        public int LogId { get; set; }

        [Column("log_lvl", TypeName = "nvarchar(100)")]
        public string LogLvl { get; set; }  // Уровень логгирования.

        [Column("type_exception", TypeName = "nvarchar(100)")]
        public string TypeException { get; set; }   // Тип исключения.

        [Column("exception", TypeName = "nvarchar(max)")]
        public string Exception { get; set; }   // Сообщение исключения.

        [Column("stack_trace", TypeName = "nvarchar(max)")]
        public string StackTrace { get; set; }  // Путь, где возникло исключение.

        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }  // Дата создания лога.
    }
}