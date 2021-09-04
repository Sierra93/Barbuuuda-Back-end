namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели списка заданий аукциона.
    /// </summary>
    public class TaskOutput {
        /// <summary>
        /// Id задания.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Id заказчика, который создал задание.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Id исполнителя, который выполняет задание.
        /// </summary>
        public string ExecutorId { get; set; }

        /// <summary>
        /// Дата создания задачи.
        /// </summary>
        public string TaskBegda { get; set; }

        /// <summary>
        /// Дата завершения задачи.
        /// </summary>
        public string TaskEndda { get; set; }

        /// <summary>
        /// Кол-во ставок к заданию.
        /// </summary>
        public int CountOffers { get; set; }

        /// <summary>
        /// Кол-во просмотров задания.
        /// </summary>
        public int CountViews { get; set; }

        /// <summary>
        /// Код типа заданий (для всех, для про).
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Код статуса задания.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Код категории задания (программирование и тд).
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// Бюджет задания в цифрах либо по дефолту "По договоренности".
        /// </summary>
        public string TaskPrice { get; set; }

        /// <summary>
        /// Заголовок задания.
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// Описание задания.
        /// </summary>
        public string TaskDetail { get; set; }

        /// <summary>
        /// Код специализации.   
        /// </summary>
        public string SpecCode { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Название статуса.
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Кол-во заданий всего.
        /// </summary>
        public long CountTotalTasks { get; set; }

        /// <summary>
        /// Флаг просроченно ли задание.
        /// </summary>
        public bool IsOverdue { get; set; }
    }
}
