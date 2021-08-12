namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс выходной модели для списков значений селекта фильтров заданий.
    /// </summary>
    public class ControlFilterOutput
    {
        /// <summary>
        /// Ключ сортировки.
        /// </summary>
        public string FilterKey { get; set; }

        /// <summary>
        /// Значение сортировки.
        /// </summary>
        public string FilterValue { get; set; }
    }
}
