using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;

namespace Barbuuuda.Models.Entities.Task
{
    [Table("CanceledInvities", Schema = "dbo")]
    public class CanceledInviteEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key]
        public long CanceledInviteId { get; set; }

        /// <summary>
        /// Id задания.
        /// </summary>
        [ForeignKey("TaskId")]
        public long TaskId { get; set; }

        /// <summary>
        /// Id исполнителя.
        /// </summary>
        [ForeignKey("Id")]
        public string ExecutorId { get; set; }

        public TaskEntity Task { get; set; }

        public UserEntity User { get; set; }
    }
}
