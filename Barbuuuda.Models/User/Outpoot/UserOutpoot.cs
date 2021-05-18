namespace Barbuuuda.Models.User.Outpoot
{
    /// <summary>
    /// Класс выходной модели пользователя.
    /// </summary>
    public class UserOutpoot
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Фото профиля.
        /// </summary>
        public string UserIcon { get; set; }

        /// <summary>
        /// Вычисляемое время для диалогов.
        /// </summary>
        public string CalcTime { get; set; }

        /// <summary>
        /// Вычисляемая дата для диалогов.
        /// </summary>
        public string CalcShortDate { get; set; }

        /// <summary>
        /// Полная дата.
        /// </summary>
        public string Created { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string UserRole { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
