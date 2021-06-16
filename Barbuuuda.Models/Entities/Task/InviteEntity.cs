using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;

namespace Barbuuuda.Models.Entities.Task
{
    [Table("Invities", Schema = "dbo")]
    public class InviteEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key]
        public long InviteId { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        [ForeignKey("TaskId")]
        public int TaskId { get; set; }

        /// <summary>
        /// Id исполнителя.
        /// </summary>
        [ForeignKey("Id")]
        public string ExecutorId { get; set; }

        /// <summary>
        /// Флаг принятия задания в работу исполнителем.
        /// </summary>
        public bool IsAccept { get; set; }

        /// <summary>
        /// Флаг отказа от взятия в работу задания исполнителем.
        /// </summary>
        public bool IsCancel { get; set; }

        [ForeignKey("OwnerId")]
        public string OwnerId { get; set; }

        public TaskEntity Task { get; set; }

        public UserEntity Id { get; set; }
    }
}
