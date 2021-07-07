using System.ComponentModel.DataAnnotations;

namespace Barbuuuda.Models.Task.Input
{
    /// <summary>
    /// Класс входной модели для получения списка ставок задания/
    /// </summary>
    public class GetRespondInput
    {
        [Required(ErrorMessage = "TaskId задания не может быть пустым")]
        public long TaskId { get; set; }
    }
}
