namespace Barbuuuda.Models.Entities.Task
{
    /// <summary>
    /// Класс сопоставляется с таблицей для контрола селекта сортировки заданий.
    /// </summary>
    public class ControlSortEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public long SortId { get; set; }

        /// <summary>
        /// Ключ сортировки.
        /// </summary>
        public string SortKey { get; set; }

        /// <summary>
        /// Значение сортировки.
        /// </summary>
        public string SortValue { get; set; }
    }
}
