namespace Barbuuuda.Models.Respond.Output
{
    /// <summary>
    /// Класс выходной модели для списка ставок задания.
    /// </summary>
    public class RespondOutput
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

        /// <summary>
        /// Флаг видимости кнопки "ИЗМЕНИТЬ СТАВКУ".
        /// </summary>
        public bool IsVisibleButton { get; set; }

        /// <summary>
        /// Id исполнителя.
        /// </summary>
        public string ExecutorId { get; set; }

        /// <summary>
        /// Вычисляемый флаг, отправленно ли приглашение исполнителю.
        /// </summary>
        public bool IsSendInvite { get; set; }

        /// <summary>
        /// Вычисляемый флаг, принял ли предложение исполнитель.
        /// </summary>
        public bool IsAcceptInvite { get; set; }

        /// <summary>
        /// Вычисляемый флаг, отказался ли исполнитель от выполнения задания.
        /// </summary>
        public bool IsCancelInvite { get; set; }
    }
}
