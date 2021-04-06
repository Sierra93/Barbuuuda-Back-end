namespace Barbuuuda.Models.Respond.Outpoot
{
    /// <summary>
    /// Класс выходной модели для списка ставок задания.
    /// </summary>
    public class RespondOutpoot
    {
        /// <summary>
        /// Логин исполнителя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Комментарий к ставке.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Цена (без НДС).
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Кол-во положительных отзывов.
        /// </summary>
        public long CountPositive { get; set; }

        /// <summary>
        /// Кол-во отрицательных отзывов.
        /// </summary>
        public long CountNegative { get; set; }

        /// <summary>
        /// Кол-во всего выполненных заданий исполнителем.
        /// </summary>
        public long CountTotalCompletedTask { get; set; }

        /// <summary>
        /// Рейтинг исполнителя.
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// Иконка профиля исполнителя.
        /// </summary>
        public string UserIcon { get; set; }
    }
}
