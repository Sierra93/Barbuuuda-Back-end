using System.ComponentModel.DataAnnotations.Schema;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;

namespace Barbuuuda.Models.Entities.Executor
{
    /// <summary>
    /// Класс сопоставляется с таблицей переходов dbo.Transitions.
    /// </summary>
    [Table("Transitions")]
    public class TransitionEntity
    {
        /// <summary>
        /// Id перехода.
        /// </summary>
        public long TransitionId { get; set; }

        /// <summary>
        /// Id пользователя совершившего переход.
        /// </summary>
        [ForeignKey("Id")]
        public string UserId { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        [ForeignKey("TaskId")]
        public long TaskId { get; set; }

        /// <summary>
        /// Тип переода (просмотр - View или редактирование - Edit).
        /// </summary>
        public string TransitionType { get; set; }

        public UserEntity User { get; set; }

        public TaskEntity Task { get; set; }
    }
}
