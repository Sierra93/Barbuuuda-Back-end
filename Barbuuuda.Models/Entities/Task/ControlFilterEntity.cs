namespace Barbuuuda.Models.Entities.Task
{
    /// <summary>
    /// Класс сопоставляется с таблицей для контрола селекта фильтрации заданий.
    /// </summary>
    public class ControlFilterEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public long FilterId { get; set; }

        /// <summary>
        /// Ключ фильтрации.
        /// </summary>
        public string FilterKey { get; set; }

        /// <summary>
        /// Значение фильтрации.
        /// </summary>
        public string FilterValue { get; set; }
    }
}
