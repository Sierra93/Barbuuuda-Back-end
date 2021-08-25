namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели для списков значений селекта сортировки заданий.
    /// </summary>
    public class ControlSortOutput
    {
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
