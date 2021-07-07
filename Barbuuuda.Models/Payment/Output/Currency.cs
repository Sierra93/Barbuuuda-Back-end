namespace Barbuuuda.Models.Payment.Output
{
    /// <summary>
    /// Класс секции виджета currency.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Тип поля.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Пометка к полю.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Подсказка при наведении.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Доступ к полю.
        /// </summary>
        public string Access { get; set; }
    }
}
