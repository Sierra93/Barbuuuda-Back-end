using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;

namespace Barbuuuda.Models.Entities.Executor
{
    /// <summary>
    /// Класс сопоставляется с таблицей переходов dbo.Transitions.
    /// </summary>
    [Table("Transitions", Schema = "dbo")]
    public class TransitionEntity
    {
        /// <summary>
        /// Id перехода.
        /// </summary>
        [Key, Column("TransitionId")]
        public long TransitionId { get; set; }

        /// <summary>
        /// Id пользователя совершившего переход.
        /// </summary>
        [Column("UserId")]
        public string Id { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        [Column("TaskId")]
        public int TaskId { get; set; }

        /// <summary>
        /// Тип переода (просмотр - View или редактирование - Edit).
        /// </summary>
        [Column("TransitionType")]
        public string TransitionType { get; set; }

        [ForeignKey("Id")]
        public UserEntity User { get; set; }

        [ForeignKey("TaskId")]
        public TaskEntity Task { get; set; }
    }
}
