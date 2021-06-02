using System.ComponentModel.DataAnnotations;

namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели приглашений исполнителя.
    /// </summary>
    public class InviteOutput
    {
        /// <summary>
        /// Id задания.
        /// </summary>
        [Required]
        public long TaskId { get; set; }

        /// <summary>
        /// Заголовок задания.
        /// </summary>
        [Required, MaxLength(100)]
        public string TaskTitle { get; set; }

        /// <summary>
        /// Описание задания.
        /// </summary>
        [Required, MaxLength(200)]
        public string TaskDetail { get; set; }

        /// <summary>
        /// Дата сдачи задания.
        /// </summary>
        [Required]
        public string TaskEndda { get; set; }

        /// <summary>
        /// Логин заказчика.
        /// </summary>
        [Required] 
        public string UserName { get; set; }

        /// <summary>
        /// Цена задания.
        /// </summary>
        [Required]
        public string TaskPrice { get; set; } = "По договоренности";

        /// <summary>
        /// Кол-во ставок к заданию.
        /// </summary>
        public int CountOffers { get; set; }

        /// <summary>
        /// Кол-во просмотров задания.
        /// </summary>
        public int CountViews { get; set; }

        /// <summary>
        /// Для всех или для PRO.
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Название статуса.
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Название категории задания.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
